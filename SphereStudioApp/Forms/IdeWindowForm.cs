using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using SphereStudio.Base;
using SphereStudio.Core;
using SphereStudio.DockPanes;
using SphereStudio.DocumentViews;

namespace SphereStudio.Forms
{
    partial class IdeWindowForm : Form, IStyleAware, ICore
    {
        const string debuggerErrorStatusText = "An error occurred launching a debugging session.";
        const string closedProjectStatusText = "No project is currently loaded.";
        const string loadedProjectStatusText = "Project was loaded successfully.";

        private DocumentTab currentTab;
        private string defaultActiveFileName;
        private DockManager dockManager = null;
        private bool isFirstDebugStop;
        private FileListPane fileListPane;
        private bool refreshingEngines = false;
        private StartPageView startPageView = null;
        private DocumentTab startPageTab = null;
        private List<DocumentTab> tabs = new List<DocumentTab>();

        public IdeWindowForm()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            PluginManager.Core = this;

            dockManager = new DockManager(mainDockPanel);
            fileListPane = new FileListPane(this);
            PluginManager.Register(null, fileListPane, "File List");

            newToolButton.DropDown = newMenuItem.DropDown;

            BuildEngine.Initialize();
            Session.Settings.Apply();

            Docking.Show(fileListPane);
            Docking.Activate(fileListPane);
            Refresh();

            if (Session.Settings.AutoOpenLastProject)
                openLastProjectMenuItem.PerformClick();
        }

        public event EventHandler LoadProject;
        public event EventHandler TestGame;
        public event EventHandler UnloadProject;

        public DocumentView ActiveDocument => currentTab.View;
        public IDebugger Debugger { get; private set; }
        public IDock Docking => dockManager;
        public IProject Project => Session.Project;
        public ICoreSettings Settings => Session.Settings;
        public UIStyle Style => StyleManager.Style;

        protected bool StartPageVisible
        {
            // This is kind of hacky, but it beats working with the Weifen Luo controls
            // directly.
            get
            {
                if (startPageTab != null && !tabs.Contains(startPageTab))
                {
                    startPageTab.Dispose();
                    startPageTab = null;
                }
                return startPageTab != null;
            }
            set
            {
                if (value && !StartPageVisible)
                {
                    startPageView = new StartPageView(this);
                    startPageView.Refresh();
                    startPageTab = addDocument(startPageView, "Start Page");
                    startPageTab.Restyle();
                }
                else if (!value && StartPageVisible)
                {
                    startPageTab.Close(true);
                    startPageTab.Dispose();
                    startPageTab = null;
                }
            }
        }

        public void AddMenuItem(ToolStripMenuItem menuItem, string before = "")
        {
            if (string.IsNullOrEmpty(before))
                mainMenuStrip.Items.Add(menuItem);
            int position = -1;
            foreach (ToolStripItem item in mainMenuStrip.Items)
            {
                if (item.Text.Replace("&", "") == before)
                    position = mainMenuStrip.Items.IndexOf(item);
            }
            mainMenuStrip.Items.Insert(position, menuItem);
        }

        public void AddMenuItem(string location, ToolStripMenuItem menuItem)
        {
            var hops = location.Split('.');
            var item = getMenuItem(mainMenuStrip.Items, hops[0]);
            if (item == null)
            {
                item = new ToolStripMenuItem(hops[0]);
                mainMenuStrip.Items.Add(item);
            }
            for (int i = 1; i < hops.Length; ++i)
            {
                ToolStripMenuItem subItem = getMenuItem(item.DropDownItems, hops[i]);
                if (subItem == null)
                {
                    subItem = new ToolStripMenuItem(hops[i]);
                    item.DropDownItems.Add(subItem);
                }
                item = subItem;
            }
            item.DropDownItems.Add(menuItem);
        }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsUIElement(mainDockPanel);
            style.AsHeading(mainMenuStrip);
            style.AsUIElement(mainToolStrip);
            style.AsHeading(mainStatusStrip);
        }

        public DocumentView OpenFile(string filePath)
        {
            return openFile(filePath, false);
        }

        public void OpenProject(string fileName, bool usePluginWarning = true)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            Project pj = Core.Project.Open(fileName);
            IStarter starter = PluginManager.Get<IStarter>(pj.User.Engine);
            ICompiler compiler = PluginManager.Get<ICompiler>(pj.Compiler);
            if (usePluginWarning && (starter == null || compiler == null))
            {
                var answer = MessageBox.Show(
                    $"One or more plugins required to work on '{pj.Name}' are either disabled or not installed.  Please open Configuration Manager and check your plugins.\n\nCompiler required:\n{pj.Compiler}\n\nIf you continue, data may be lost.  Open this project anyway?",
                    "Proceed with Caution",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.No)
                    return;
            }

            if (!closeCurrentProject())
                return;
            Session.Project = pj;

            refreshProject();

            LoadProject?.Invoke(null, EventArgs.Empty);

            statusLabel.Text = loadedProjectStatusText;

            StartPageVisible = true;

            string[] docs = Session.Project.User.Documents;
            foreach (string s in docs)
            {
                if (string.IsNullOrWhiteSpace(s))
                    continue;
                try { openFile(s, true); }
                catch (Exception) { }
            }

            // if the form isn't visible, don't try to mess with the panels.
            // it will be done in OnLoad.
            if (Visible)
            {
                if (Session.Project.User.StartPageHidden)
                    StartPageVisible = false;

                var tab = findDocumentTab(Session.Project.User.ActiveDocument);
                tab?.Activate();
            }

            refreshEngineList();
            refreshUI();
        }

        public override void Refresh()
        {
            base.Refresh();

            foreach (DocumentTab tab in tabs)
                tab.Restyle();
            dockManager.Refresh();
            refreshProject();
            refreshEngineList();
            refreshUI();
        }

        public void RemoveMenuItem(ToolStripMenuItem item)
        {
            var menuItem = item.OwnerItem as ToolStripMenuItem;
            menuItem?.DropDownItems.Remove(item);
        }

        public void RemoveMenuItem(string name)
        {
            var item = getMenuItem(mainMenuStrip.Items, name);
            item?.Dispose();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Rectangle savedBounds = WindowState != FormWindowState.Normal ? RestoreBounds : Bounds;
            Properties.Settings.Default.WindowX = savedBounds.X;
            Properties.Settings.Default.WindowY = savedBounds.Y;
            Properties.Settings.Default.WindowWidth = savedBounds.Width;
            Properties.Settings.Default.WindowHeight = savedBounds.Height;
            Properties.Settings.Default.WindowMaxed = WindowState == FormWindowState.Maximized;
            Properties.Settings.Default.Save();

            Size = new Size(Properties.Settings.Default.WindowWidth, Properties.Settings.Default.WindowHeight);
            WindowState = Properties.Settings.Default.WindowMaxed ? FormWindowState.Maximized : FormWindowState.Normal;

            if (!closeCurrentProject(true))
                e.Cancel = true;
            else
                dockManager.Persist();

            base.OnFormClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            Location = new Point(Properties.Settings.Default.WindowX, Properties.Settings.Default.WindowY);
            Size = new Size(Properties.Settings.Default.WindowWidth, Properties.Settings.Default.WindowHeight);
            WindowState = Properties.Settings.Default.WindowMaxed
                ? FormWindowState.Maximized
                : FormWindowState.Normal;

            // this works around glitchy WeifenLuo behavior when messing with panel
            // visibility before the form loads.
            if (Session.Settings.AutoOpenLastProject && Session.Project != null)
            {
                StartPageVisible = !Session.Project.User.StartPageHidden;
                var tab = findDocumentTab(Session.Project.User.ActiveDocument);
                if (tab != null)
                    tab.Activate();
            }
            else
            {
                StartPageVisible = Session.Settings.UseStartPage;
            }

            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            bool isFirstRun = !Session.Settings.GetBoolean("setupComplete", false);
            if (isFirstRun)
            {
                var selection = MessageBox.Show(
                    "Welcome to Sphere Studio, the fully integrated development environment for the Sphere game platform!\r\n\r\n" +
                        "Since it looks like this is your first time using the IDE, you'll need to set a few things up.  Do you want to do that now?",
                    "Welcome to Sphere Studio", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (selection == DialogResult.Yes)
                {
                    showPluginManager();
                    Session.Settings.SetValue("setupComplete", true);
                }
                else
                {
                    Close();
                }
            }

            if (!string.IsNullOrWhiteSpace(defaultActiveFileName))
            {
                var tab = findDocumentTab(defaultActiveFileName);
                tab?.Activate();
            }

            base.OnShown(e);
        }

        internal string[] getFilesToOpen(bool multiSelect)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                var plugins = from name in PluginManager.GetNames<IFileOpener>()
                              let plugin = PluginManager.Get<IFileOpener>(name)
                              where plugin.FileExtensions != null
                              orderby plugin.FileTypeName ascending
                              select plugin;
                var filterString = string.Empty;
                foreach (IFileOpener plugin in plugins)
                {
                    var extensions = string.Empty;
                    foreach (string extension in plugin.FileExtensions)
                    {
                        if (extensions.Length > 0)
                            extensions += ";";
                        extensions += $"*.{extension}";
                    }
                    filterString += $"{plugin.FileTypeName}|{extensions}|";
                }
                filterString += "All Files|*.*";
                dialog.Filter = filterString;
                dialog.FilterIndex = plugins.Count() + 1;
                dialog.InitialDirectory = Session.Project.RootPath;
                dialog.Multiselect = multiSelect;
                return dialog.ShowDialog() == DialogResult.OK ? dialog.FileNames : null;
            }
        }

        internal void setDefaultActiveFile(string fileName)
        {
            defaultActiveFileName = fileName;
        }

        private DocumentTab addDocument(DocumentView view, string fileName = null, bool restoreView = false)
        {
            DocumentTab tab = new DocumentTab(this, view, fileName, restoreView);
            tab.Closed += (sender, e) => tabs.Remove(tab);
            tab.Activate();
            tabs.Add(tab);
            return tab;
        }

        private bool closeAllDocuments(bool forceClose = false)
        {
            var tabsToClose = tabs.ToArray();
            if (!forceClose && !tabsToClose.All(tab => tab.PromptSave()))
                return false;
            foreach (var tab in tabsToClose)
                tab.Close(true);
            StartPageVisible = false;
            return true;
        }

        private bool closeCurrentProject(bool forceClose = false)
        {
            if (Session.Project == null)
                return true;

            // if the debugger is active, prevent closing the project.
            if (Debugger != null)
            {
                MessageBox.Show(this,
                    "There is an active debugging session.  Please stop the debugger before closing the project or IDE.",
                    "Debugging in Progress", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // user values will be lost if we don't record them now.
            Session.Project.User.StartPageHidden = !StartPageVisible;
            Session.Project.User.Documents = tabs
                .Where(it => it.FileName != null)
                .Select(it => it.FileName)
                .ToArray();
            Session.Project.User.ActiveDocument = currentTab != null
                ? currentTab.FileName : "";

            // close all open document tabs
            if (!closeAllDocuments(forceClose))
                return false;

            // save and unload the project
            if (Session.Project != null)
            {
                UnloadProject?.Invoke(null, EventArgs.Empty);
                Session.Project.Save();
                Session.Project = null;
            }

            // clear the project tree
            fileListPane.Close();
            openLastProjectMenuItem.Enabled = (Session.Settings.LastProjectFileName.Length > 0);

            // all clear!
            refreshProject();
            refreshEngineList();
            refreshUI();
            statusLabel.Text = closedProjectStatusText;
            return true;
        }

        private DocumentTab findDocumentTab(string fileName)
        {
            return tabs.Find(it => it.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase));
        }

        private ToolStripMenuItem getMenuItem(ToolStripItemCollection collection, string name)
        {
            return collection
                .OfType<ToolStripMenuItem>()
                .FirstOrDefault(it => name == it.Text.Replace("&", ""));
        }

        private bool isProjectLoaded()
        {
            return Session.Project != null;
        }

        private DocumentView openFile(string fileName, bool restoreView)
        {
            var extension = Path.GetExtension(fileName);

            // for Sphere Studio project files, load it into the IDE as a project
            if (extension.Equals(".ssproj", StringComparison.OrdinalIgnoreCase))
            {
                OpenProject(fileName);
                return null;
            }

            // if the file is already open, just switch to it
            var tab = findDocumentTab(fileName);
            if (tab != null)
            {
                tab.Activate();
                return tab.View;
            }

            // the IDE will look for a file opener explicitly declaring the file extension.
            // if that fails, then use the default opener (if any).
            DocumentView view = null;
            try
            {
                if (extension.StartsWith("."))  // remove dot from extension
                    extension = extension.Substring(1);
                var plugins = from name in PluginManager.GetNames<IFileOpener>()
                              let plugin = PluginManager.Get<IFileOpener>(name)
                              where plugin.FileExtensions.Any(it => extension.Equals(it, StringComparison.OrdinalIgnoreCase))
                              select plugin;
                IFileOpener defaultOpener = PluginManager.Get<IFileOpener>(Session.Settings.FileOpener);
                IFileOpener opener = plugins.FirstOrDefault() ?? defaultOpener;
                if (opener != null)
                {
                    view = opener.Open(fileName);
                }
                else
                {
                    MessageBox.Show(
                        $"Sphere Studio doesn't know how to open that type of file and no default file opener is available.  Tip: Go to Preferences -> Plugins and check your plugins.\n\nFile Type: .{extension}\n\nPath to File:\n{fileName}",
                        "Unable to Open File",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (IOException)
            {
                view = null;
            }

            if (view != null)
                addDocument(view, fileName, restoreView);
            return view;
        }

        private void refreshEngineList()
        {
            bool wasRefreshingEngines = refreshingEngines;
            refreshingEngines = true;

            engineToolComboBox.Items.Clear();
            var engineNames = PluginManager.GetNames<IStarter>();
            if (isProjectLoaded() && engineNames.Length > 0)
            {
                engineToolComboBox.Items.AddRange(engineNames);
                engineToolComboBox.Items.Add("Plugin Manager...");
                engineToolComboBox.Text = Session.Project.User.Engine;
                engineToolComboBox.Enabled = true;
            }
            else
            {
                engineToolComboBox.Enabled = false;
            }

            refreshingEngines = wasRefreshingEngines;
        }

        private void refreshProject()
        {
            var ideName = Versioning.IsWiP ? $"{Versioning.Name} WiP" : Versioning.Name;
            if (isProjectLoaded())
            {
                Session.Settings.LastProjectFileName = Session.Project.FileName;
                Text = Session.Project.GameOnly
                    ? $"{Project.Name} (Sphere Game) - {ideName}"
                    : $"{Project.Name} - {ideName}";
                projectStatusLabel.Text = Path.GetFileName(Session.Project.FileName);
                fileListPane.Open();
                fileListPane.Refresh();
            }
            else
            {
                Text = ideName;
                projectStatusLabel.Text = "no project";
            }
        }

        private void refreshUI()
        {
            var starter = isProjectLoaded()
                ? PluginManager.Get<IStarter>(Session.Project.User.Engine)
                : null;

            var canBreak = Debugger?.Running ?? false;
            var canConfigureEngine = starter?.CanConfigure ?? false;
            var canCopyPaste = currentTab != null && currentTab != startPageTab;
            var canLaunch = isProjectLoaded() && Debugger == null;
            var canSave = currentTab?.View.CanSave ?? false;
            var canStep = Debugger != null && !Debugger.Running;
            var canTestGame = BuildEngine.CanTest(Session.Project) && Debugger == null;

            configureEngineToolButton.Enabled = canConfigureEngine;
            copyToolButton.Enabled = canCopyPaste;
            cutToolButton.Enabled = canCopyPaste;
            pasteToolButton.Enabled = canCopyPaste;
            pauseToolButton.Enabled = canBreak;
            projectPropertiesToolButton.Enabled = isProjectLoaded();
            runGameToolButton.Enabled = canLaunch || canStep;
            saveToolButton.Enabled = canSave;
            stopToolButton.Enabled = canBreak || canStep;
            testGameToolButton.Enabled = canTestGame;

            buildRunToolMenuItem.Enabled = canLaunch || canStep;
            rebuildRunToolMenuItem.Enabled = canLaunch;

            dockManager?.Refresh();
        }

        private void selectDockPane(string name)
        {
            foreach (var content in mainDockPanel.Contents)
            {
                if (content.DockHandler.TabText == name)
                    content.DockHandler.Activate();
            }
        }

        private void showPluginManager()
        {
            showPreferencesDialog("Plugins");
        }

        private void showPreferencesDialog(string pageName = null)
        {
            var result = new PreferencesForm(pageName).ShowDialog();
            if (result == DialogResult.OK)
            {
                Session.Settings.Apply();
                refreshProject();
                refreshEngineList();
                refreshUI();
                startPageView.Refresh();
            }
        }

        private async Task startEngine(bool wantDebugger, bool rebuilding = false)
        {
            foreach (DocumentTab tab in
                from tab in tabs
                where tab.FileName != null
                select tab)
            {
                tab.SaveIfDirty();
            }
            testGameMenuItem.Enabled = testGameToolButton.Enabled = false;
            buildRunMenuItem.Enabled = runGameToolButton.Enabled = false;

            if (TestGame != null)
                TestGame(null, EventArgs.Empty);

            if (wantDebugger && BuildEngine.CanDebug(Session.Project))
            {
                Debugger = await BuildEngine.Debug(Session.Project, rebuilding);
                if (Debugger != null)
                {
                    Debugger.Detached += debugger_Detached;
                    Debugger.Paused += debugger_Paused;
                    Debugger.Resumed += debugger_Resumed;
                    testGameMenuItem.Enabled = false;
                    testGameToolButton.Enabled = false;
                    isFirstDebugStop = true;
                    if (await Debugger.Attach())
                    {
                        buildRunMenuItem.Text = buildRunToolMenuItem.Text = "&Resume Running";
                        runGameToolButton.Text = "Resume";
                        var breakpoints = Session.Project.GetAllBreakpoints();
                        foreach (var fileName in breakpoints.Keys)
                            foreach (var lineNumber in breakpoints[fileName])
                                await Debugger.SetBreakpoint(fileName, lineNumber);
                        await Debugger.Resume();
                    }
                    else
                    {
                        SystemSounds.Hand.Play();
                        statusLabel.Text = debuggerErrorStatusText;
                        Debugger = null;
                        refreshUI();
                    }
                }
            }
            else
            {
                await BuildEngine.Test(Session.Project, rebuilding);
            }

            refreshUI();
        }

        private void debugger_Detached(object sender, EventArgs e)
        {
            var scriptViews = from tab in tabs
                              where tab.View is TextView
                              select (TextView)tab.View;
            foreach (var view in scriptViews)
            {
                view.ActiveLine = 0;
                view.ErrorLine = 0;
            }
            Debugger = null;
            buildRunMenuItem.Text = "Build && &Run";
            buildRunToolMenuItem.Text = "Build && &Run (Default)";
            runGameToolButton.Text = "&Run Game";
            refreshUI();
        }

        private void debugger_Paused(object sender, PausedEventArgs e)
        {
            if (isFirstDebugStop)
            {
                // ignore first pause
                isFirstDebugStop = false;
                return;
            }

            var view = OpenFile(Debugger.FileName) as TextView;
            if (view != null)
            {
                view.ActiveLine = Debugger.LineNumber;
                if (e.Reason == PauseReason.Exception)
                    view.ErrorLine = Debugger.LineNumber;
            }
            if (!Debugger.Running)
                Activate();
            refreshUI();
        }

        private void debugger_Resumed(object sender, EventArgs e)
        {
            var scriptViews = from tab in tabs
                              where tab.View is TextView
                              select (TextView)tab.View;
            foreach (var view in scriptViews)
            {
                view.ActiveLine = 0;
                view.ErrorLine = 0;
            }
            refreshUI();
        }

        private void mainDockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (mainDockPanel.ActiveDocument != null)
            {
                var content = (DockContent)mainDockPanel.ActiveDocument;
                if (content.Tag is DocumentTab)
                {
                    currentTab?.Deactivate();
                    currentTab = (DocumentTab)content.Tag;
                    currentTab.Activate();
                }
            }
            else
            {
                currentTab?.Deactivate();
                currentTab = null;
            }
            refreshUI();
        }

        #region toolbar control event handlers
        private void engineToolComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (refreshingEngines)
                return;

            // user selected Configure (always at bottom)
            if (engineToolComboBox.SelectedIndex == engineToolComboBox.Items.Count - 1)
            {
                showPluginManager();
                refreshEngineList();
                return;
            }

            Session.Project.User.Engine = engineToolComboBox.Text;
            refreshUI();
            refreshEngineList();
        }
        #endregion

        #region File menu Click handlers
        private void fileMenu_DropDownOpening(object sender, EventArgs e)
        {
            var canClose = currentTab != null;
            var canSave = currentTab?.View.CanSave ?? false;
            var haveLastProject = !string.IsNullOrEmpty(Session.Settings.LastProjectFileName);

            closeMenuItem.Enabled = canClose;
            closeProjectMenuItem.Enabled = isProjectLoaded();
            openLastProjectMenuItem.Enabled = haveLastProject
                && (!isProjectLoaded() || Session.Settings.LastProjectFileName != Session.Project.FileName);
            saveMenuItem.Enabled = canSave;
            saveAsMenuItem.Enabled = canSave;

            closeMenuItem.Text = canClose ? $"&Close {currentTab.Title}" : "&Close";
            saveMenuItem.Text = canSave ? $"&Save {currentTab.Title}" : "&Save";
        }

        internal void newMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripDropDown dropDown = ((ToolStripDropDownItem)sender).DropDown;

            var pluginNames = PluginManager.GetNames<INewFileOpener>();
            if (pluginNames.Length > 0)
                dropDown.Items.Add(new ToolStripSeparator() { Name = "8:12" });
            var plugins = from name in pluginNames
                          let plugin = PluginManager.Get<INewFileOpener>(name)
                          orderby plugin.FileTypeName ascending
                          select plugin;
            foreach (var plugin in plugins)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(plugin.FileTypeName) { Name = "8:12" };
                item.Image = plugin.FileIcon;
                item.Click += (s, ea) =>
                {
                    var view = plugin.New();
                    if (view != null)
                        addDocument(view);
                };
                dropDown.Items.Add(item);
            }
        }

        internal void newMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            var dropDown = ((ToolStripDropDownItem)sender).DropDown;
            while (dropDown.Items.ContainsKey("8:12"))
                dropDown.Items.RemoveByKey("8:12");
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            currentTab?.Close();
        }

        private void closeProjectMenuItem_Click(object sender, EventArgs e)
        {
            closeCurrentProject();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newProjectMenuItem_Click(object sender, EventArgs e)
        {
            string rootPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Sphere Projects");
            NewProjectForm npf = new NewProjectForm(rootPath);

            var starter = PluginManager.Get<IStarter>(Session.Settings.Engine);
            var compiler = PluginManager.Get<ICompiler>(Session.Settings.Compiler);
            if (starter == null || compiler == null)
            {
                MessageBox.Show(
                    "Unable to create a new Sphere Studio project.\n\nDefault engine and/or compiler plugins have not yet been selected.  Please go to Preferences -> Plugins and select a default engine and compiler plugin, then try again.",
                    "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (npf.ShowDialog() == DialogResult.OK)
            {
                if (!closeCurrentProject())
                    return;
                if (BuildEngine.Prep(npf.NewProject))
                {
                    npf.NewProject.Save();
                    OpenProject(npf.NewProject.FileName, false);
                    startPageView.Refresh();
                }
                else
                {
                    Directory.Delete(npf.NewProject.RootPath, true);
                }
            }
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            string[] fileNames = getFilesToOpen(false);
            if (fileNames == null) return;
            OpenFile(fileNames[0]);
        }

        private void openLastProjectMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Session.Settings.LastProjectFileName))
                OpenProject(Session.Settings.LastProjectFileName, false);
            else
                refreshUI();
        }

        private void openProjectMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog projDiag = new OpenFileDialog())
            {
                projDiag.Title = "Open Project";
                projDiag.Filter = "All Supported Projects|*.ssproj;game.sgm|Sphere Studio Projects|*.ssproj|Sphere 1.x Game Manifest|game.sgm";
                projDiag.InitialDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Sphere Projects");

                if (projDiag.ShowDialog() == DialogResult.OK)
                    OpenProject(projDiag.FileName);
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            string savePath = isProjectLoaded() ? Session.Project.RootPath : null;
            if (currentTab != null)
                currentTab.Save(savePath);
        }

        private void saveAllMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DocumentTab tab in tabs)
                tab.Save();
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            string savePath = isProjectLoaded() ? Session.Project.RootPath : null;
            if (currentTab != null)
                currentTab.SaveAs(savePath);
        }
        #endregion

        #region Edit menu Click handlers
        private void editMenu_DropDownOpening(object sender, EventArgs e)
        {
            var canCopyPaste = currentTab != null && currentTab != startPageTab;

            copyMenuItem.Enabled = canCopyPaste;
            cutMenuItem.Enabled = canCopyPaste;
            pasteMenuItem.Enabled = canCopyPaste;
            redoMenuItem.Enabled = canCopyPaste;
            selectAllMenuItem.Enabled = canCopyPaste;
            undoMenuItem.Enabled = canCopyPaste;
            zoomInMenuItem.Enabled = canCopyPaste;
            zoomOutMenuItem.Enabled = canCopyPaste;
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            currentTab?.Copy();
        }

        private void cutMenuItem_Click(object sender, EventArgs e)
        {
            currentTab?.Cut();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            currentTab?.Paste();
        }

        private void redoMenuItem_Click(object sender, EventArgs e)
        {
            currentTab?.Redo();
        }

        private void selectAllMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            currentTab?.Undo();
        }

        private void zoomInMenuItem_Click(object sender, EventArgs e)
        {
            currentTab?.ZoomIn();
        }

        private void zoomOutMenuItem_Click(object sender, EventArgs e)
        {
            currentTab?.ZoomOut();
        }
        #endregion

        #region View menu Click handlers
        private void viewMenu_DropDownOpening(object sender, EventArgs e)
        {
            startPageMenuItem.Checked = currentTab == startPageTab;

            // add dock panels to View menu
            var panelNames = from name in PluginManager.GetNames<IDockPane>()
                             let plugin = PluginManager.Get<IDockPane>(name)
                             where plugin.ShowInViewMenu
                             select name;
            if (panelNames.Any())
            {
                ToolStripSeparator ts = new ToolStripSeparator { Name = "zz_v" };
                viewMenu.DropDownItems.Add(ts);
                foreach (string title in panelNames)
                {
                    var plugin = PluginManager.Get<IDockPane>(title);
                    ToolStripMenuItem item = new ToolStripMenuItem(title) { Name = "zz_v" };
                    item.Image = plugin.DockIcon;
                    item.Checked = dockManager.IsVisible(plugin);
                    item.Click += (o, ev) => dockManager.Toggle(plugin);
                    viewMenu.DropDownItems.Add(item);
                }
            }

            // add open documents to View menu
            var tabList = from tab in tabs
                          where tab != startPageTab
                          select tab;
            if (tabList.Any())
            {
                ToolStripSeparator ts = new ToolStripSeparator { Name = "zz_v" };
                viewMenu.DropDownItems.Add(ts);
                foreach (DocumentTab tab in tabList)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(tab.Title) { Name = "zz_v" };
                    item.Click += documentMenuItem_Click;
                    item.Image = tab.View.Icon.ToBitmap();
                    item.Tag = tab.FileName;
                    item.Checked = tab == currentTab;
                    viewMenu.DropDownItems.Add(item);
                }
            }
        }

        private void viewMenu_DropDownClosed(object sender, EventArgs e)
        {
            // remove documents and dock panels from View menu
            for (int i = 0; i < viewMenu.DropDownItems.Count; ++i)
            {
                if (viewMenu.DropDownItems[i].Name == "zz_v")
                    viewMenu.DropDownItems.RemoveAt(i--);
            }
        }

        void documentMenuItem_Click(object sender, EventArgs e)
        {
            var tab = findDocumentTab((string)((ToolStripItem)sender).Tag);
            if (tab != null)
                tab.Activate();
            else
                selectDockPane((string)((ToolStripMenuItem)sender).Tag);
        }

        private void startPageMenuItem_Click(object sender, EventArgs e)
        {
            StartPageVisible = true;
            startPageTab.Activate();
        }
        #endregion

        #region Project menu Click handlers
        private void projectMenu_DropDownOpening(object sender, EventArgs e)
        {
            var haveProject = isProjectLoaded();

            exploreProjectMenuItem.Enabled = haveProject;
            projectPropertiesMenuItem.Enabled = haveProject;
            refreshProjectMenuItem.Enabled = haveProject;
        }

        private void exploreProjectMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", $@"/select,""{Session.Project.FileName}""");
        }

        private void projectPropertiesMenuItem_Click(object sender, EventArgs e)
        {
            var result = new ProjectPropertiesForm(Session.Project).ShowDialog();
            if (result == DialogResult.OK)
            {
                refreshEngineList();
                refreshUI();
                refreshProject();
            }
        }

        private void refreshProjectMenuItem_Click(object sender, EventArgs e)
        {
            refreshProject();
        }
        #endregion

        #region Build menu Click handlers
        private void buildMenu_DropDownOpening(object sender, EventArgs e)
        {
            var canBuild = isProjectLoaded() && Debugger == null;
            var canPackageGame = BuildEngine.CanPackage(Session.Project) && Debugger == null;
            buildMenuItem.Enabled = canBuild;
            packageGameMenuItem.Enabled = canPackageGame;
            rebuildMenuItem.Enabled = canBuild;
        }

        private async void buildMenuItem_Click(object sender, EventArgs e)
        {
            await BuildEngine.Build(Session.Project, true);
        }

        private async void packageGameMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Title = "Build Game Package",
                InitialDirectory = Session.Project.RootPath,
                Filter = BuildEngine.GetSaveFileFilters(Session.Project),
                DefaultExt = "spk",
                AddExtension = true,
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                await BuildEngine.Package(Session.Project, sfd.FileName, false);
            }
        }

        private async void rebuildMenuItem_Click(object sender, EventArgs e)
        {
            await BuildEngine.Build(Session.Project, true, true);
        }
        #endregion

        #region Run menu Click handlers
        private void runMenu_DropDownOpening(object sender, EventArgs e)
        {
            var canBreak = Debugger?.Running ?? false;
            var canLaunch = isProjectLoaded() && Debugger == null;
            var canStep = Debugger != null && !Debugger.Running;
            var canTestGame = BuildEngine.CanTest(Session.Project) && Debugger == null;

            breakNowMenuItem.Enabled = canBreak;
            buildRunMenuItem.Enabled = canLaunch || canStep;
            rebuildRunMenuItem.Enabled = canLaunch;
            stepIntoMenuItem.Enabled = canStep;
            stepOutMenuItem.Enabled = canStep;
            stepOverMenuItem.Enabled = canStep;
            stopDebuggingMenuItem.Enabled = canBreak || canStep;
            testGameMenuItem.Enabled = canTestGame;
        }

        private void breakNowMenuItem_Click(object sender, EventArgs e)
        {
            Debugger.Pause();
        }

        private async void buildRunMenuItem_Click(object sender, EventArgs e)
        {
            if (Debugger != null)
                await Debugger.Resume();
            else
                await startEngine(true);
        }

        private async void rebuildRunMenuItem_Click(object sender, EventArgs e)
        {
            await startEngine(true, true);
        }

        private void stepIntoMenuItem_Click(object sender, EventArgs e)
        {
            Debugger.StepInto();
        }

        private void stepOutMenuItem_Click(object sender, EventArgs e)
        {
            Debugger.StepOut();
        }

        private void stepOverMenuItem_Click(object sender, EventArgs e)
        {
            Debugger.StepOver();
        }

        private void stopDebuggingMenuItem_Click(object sender, EventArgs e)
        {
            Debugger.Detach();
        }

        private async void testGameMenuItem_Click(object sender, EventArgs e)
        {
            await startEngine(false, true);
        }
        #endregion

        #region Settings menu Click handlers
        private void settingsMenu_DropDownOpening(object sender, EventArgs e)
        {
            var starter = isProjectLoaded()
               ? PluginManager.Get<IStarter>(Session.Project.User.Engine)
               : null;

            var canConfigureEngine = starter?.CanConfigure ?? false;

            configureEngineMenuItem.Enabled = canConfigureEngine;
        }

        private void configureEngineMenuItem_Click(object sender, EventArgs e)
        {
            PluginManager.Get<IStarter>(Session.Project.User.Engine)
                .Configure();
        }

        private void preferencesMenuItem_Click(object sender, EventArgs e)
        {
            showPreferencesDialog();
        }
        #endregion

        #region Help menu Click handlers
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutBox = new AboutBoxForm())
                aboutBox.ShowDialog();
        }
        #endregion
    }
}
 