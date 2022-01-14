using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using SphereStudio.Base;
using SphereStudio.Core;
using SphereStudio.DockPanes;
using SphereStudio.DocumentViews;

namespace SphereStudio.Forms
{
    /// <summary>
    /// Represents an instance of the Sphere Studio IDE.
    /// </summary>
    partial class IdeWindow : Form, IStyleAware, ICore
    {
        private DocumentTab currentTab;
        private string defaultActiveDocumentName;
        private DockManager dockManager = null;
        private bool isFirstDebugStop;
        private bool loadingPresets = false;
        private FileListPane fileListPane;
        private StartPageView startPageView = null;
        private DocumentTab startPageTab = null;
        private List<DocumentTab> tabs = new List<DocumentTab>();

        public IdeWindow()
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            PluginManager.Core = this;

            InitializeDocking();
            PluginManager.Register(null, fileListPane, "File List");

            Text = Versioning.IsWiP ? $"{Versioning.Name} WiP" : Versioning.Name;
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

        protected bool StartVisible
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
                if (value && !StartVisible)
                {
                    startPageView = new StartPageView(this);
                    startPageView.RepopulateProjects();
                    startPageTab = AddDocument(startPageView, "Start Page");
                    startPageTab.Restyle();
                }
                else if (!value && StartVisible)
                {
                    startPageTab.Close(true);
                    startPageTab.Dispose();
                    startPageTab = null;
                }
            }
        }

        /// <summary>
        /// Adds a new top-level menu to the IDE menu bar.
        /// </summary>
        /// <param name="item">The menu item to add.</param>
        /// <param name="before">The name of the menu before which this one will be inserted.</param>
        public void AddMenuItem(ToolStripMenuItem item, string before = "")
        {
            if (string.IsNullOrEmpty(before))
                mainMenuStrip.Items.Add(item);
            int insertion = -1;
            foreach (ToolStripItem menuitem in mainMenuStrip.Items)
            {
                if (menuitem.Text.Replace("&", "") == before)
                    insertion = mainMenuStrip.Items.IndexOf(menuitem);
            }
            CreateRootMenuItem(item);
            mainMenuStrip.Items.Insert(insertion, item);
        }

        /// <summary>
        /// Adds a subitem to an existing menu.
        /// </summary>
        /// <param name="location">The menu to add the item to. Use dots to drill down, e.g. "File.New"</param>
        /// <param name="newItem">The ToolStripItem of the menu item to add.</param>
        public void AddMenuItem(string location, ToolStripItem newItem)
        {
            string[] items = location.Split('.');
            ToolStripMenuItem item = GetMenuItem(mainMenuStrip.Items, items[0]);
            if (item == null)
            {
                item = new ToolStripMenuItem(items[0]);
                CreateRootMenuItem(item);
                mainMenuStrip.Items.Add(item);
            }

            for (int i = 1; i < items.Length; ++i)
            {
                ToolStripMenuItem menuitem = GetMenuItem(item.DropDownItems, items[i]);
                if (menuitem == null)
                {
                    menuitem = new ToolStripMenuItem(items[i]);
                    item.DropDownItems.Add(menuitem);
                }
                item = menuitem;
            }

            item.DropDownItems.Add(newItem);
        }

        public void RemoveMenuItem(ToolStripItem item)
        {
            ToolStripMenuItem menuItem = item.OwnerItem as ToolStripMenuItem;
            if (menuItem != null) menuItem.DropDownItems.Remove(item);
        }

        public void RemoveMenuItem(string name)
        {
            ToolStripMenuItem item = GetMenuItem(mainMenuStrip.Items, name);
            if (item != null) item.Dispose();
        }

        public DocumentView OpenFile(string filePath)
        {
            return OpenFile(filePath, false);
        }

        public DocumentView OpenFile(string filePath, bool restoreView)
        {
            string extension = Path.GetExtension(filePath);

            // is it a project?
            if (extension.ToUpper() == ".SSPROJ")
            {
                OpenProject(filePath);
                return null;
            }

            // if the file is already open, just switch to it
            DocumentTab tab = GetDocument(filePath);
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
                string fileExtension = Path.GetExtension(filePath);
                if (fileExtension.StartsWith("."))  // remove dot from extension
                    fileExtension = fileExtension.Substring(1);
                var plugins = from name in PluginManager.GetNames<IFileOpener>()
                              let plugin = PluginManager.Get<IFileOpener>(name)
                              where plugin.FileExtensions.Any(it => it.ToUpperInvariant() == fileExtension.ToUpperInvariant())
                              select plugin;
                IFileOpener defaultOpener = PluginManager.Get<IFileOpener>(Session.Settings.FileOpener);
                IFileOpener opener = plugins.FirstOrDefault() ?? defaultOpener;
                if (opener != null)
                {
                    view = opener.Open(filePath);
                }
                else
                {
                    MessageBox.Show(
                        $"Sphere Studio doesn't know how to open that type of file and no default file opener is available.  Tip: Go to Preferences -> Plugins and check your plugins.\n\nFile Type: {fileExtension.ToLower()}\n\nPath to File:\n{filePath}",
                        "Unable to Open File",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (IOException)
            {
                return null;
            }

            if (view != null)
                AddDocument(view, filePath, restoreView);
            return view;
        }

        /// <summary>
        /// Loads a Sphere Studio project into the IDE.
        /// </summary>
        /// <param name="fileName">The filename of the project.</param>
        /// <param name="usePluginWarning">Whether to show a warning if required plugins are missing.</param>
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

            if (!CloseCurrentProject())
                return;
            Session.Project = pj;

            RefreshProject();

            LoadProject?.Invoke(null, EventArgs.Empty);

            helpStatusLabel.Text = "Project was loaded successfully.";

            StartVisible = true;

            string[] docs = Session.Project.User.Documents;
            foreach (string s in docs)
            {
                if (string.IsNullOrWhiteSpace(s))
                    continue;
                try { OpenFile(s, true); }
                catch (Exception) { }
            }

            // if the form isn't visible, don't try to mess with the panels.
            // it will be done in OnLoad.
            if (Visible)
            {
                if (Session.Project.User.StartPageHidden)
                    StartVisible = false;

                var tab = GetDocument(Session.Project.User.ActiveDocument);
                tab?.Activate();
            }

            UpdateEngineList();
            UpdateControls();
        }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(mainDockPanel);
            style.AsHeading(MainMenuStrip);
            style.AsUIElement(mainToolStrip);
            style.AsHeading(mainStatusStrip);
            UpdateMenuItems();
        }

        public override void Refresh()
        {
            base.Refresh();

            foreach (DocumentTab tab in tabs)
                tab.Restyle();
            dockManager.Refresh();
            UpdateEngineList();
            UpdateControls();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.Cancel)
                return;

            Rectangle savedBounds = WindowState != FormWindowState.Normal ? RestoreBounds : Bounds;
            Properties.Settings.Default.WindowX = savedBounds.X;
            Properties.Settings.Default.WindowY = savedBounds.Y;
            Properties.Settings.Default.WindowWidth = savedBounds.Width;
            Properties.Settings.Default.WindowHeight = savedBounds.Height;
            Properties.Settings.Default.WindowMaxed = WindowState == FormWindowState.Maximized;
            Properties.Settings.Default.Save();

            Size = new Size(Properties.Settings.Default.WindowWidth, Properties.Settings.Default.WindowHeight);
            WindowState = Properties.Settings.Default.WindowMaxed ? FormWindowState.Maximized : FormWindowState.Normal;

            if (!CloseCurrentProject(true))
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
                StartVisible = !Session.Project.User.StartPageHidden;
                DocumentTab tab = GetDocument(Session.Project.User.ActiveDocument);
                if (tab != null)
                    tab.Activate();
            }
            else
            {
                StartVisible = Session.Settings.UseStartPage;
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

            if (!string.IsNullOrWhiteSpace(defaultActiveDocumentName))
            {
                var tab = GetDocument(defaultActiveDocumentName);
                tab?.Activate();
            }

            base.OnShown(e);
        }

        void topMenu_DropDownOpening(object sender, EventArgs e)
        {
            Color c = ((ToolStripMenuItem) sender).DropDown.BackColor;
            if (c.R + c.G + c.B > 380)
                ((ToolStripMenuItem) sender).ForeColor = Color.Black;
            else
                ((ToolStripMenuItem) sender).ForeColor = Color.White;
        }

        void topMenu_DropDownClosed(object sender, EventArgs e)
        {
            Color c = MainMenuStrip.BackColor;
            if (c.R + c.G + c.B > 380) // find contrast level.
                ((ToolStripMenuItem) sender).ForeColor = Color.Black;
            else
                ((ToolStripMenuItem) sender).ForeColor = Color.White;
        }

        private void mainDockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (mainDockPanel.ActiveDocument == null)
                return;
            DockContent content = mainDockPanel.ActiveDocument as DockContent;
            if (content.Tag is DocumentTab)
            {
                if (currentTab != null) currentTab.Deactivate();
                currentTab = content.Tag as DocumentTab;
                currentTab.Activate();
            }
            UpdateControls();
        }

        #region File menu Click handlers
        private void menuFile_DropDownOpening(object sender, EventArgs e)
        {
            saveAsMenuItem.Enabled = saveMenuItem.Enabled = (currentTab != null);
            closeProjectMenuItem.Enabled = IsProjectOpen;
            openLastProjectMenuItem.Enabled = (!IsProjectOpen ||
                Session.Settings.LastProject != Session.Project.RootPath);
            topMenu_DropDownOpening(sender, e);
        }

        internal void newMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripDropDown dropDown = ((ToolStripDropDownItem)sender).DropDown;

            string[] pluginNames = PluginManager.GetNames<INewFileOpener>();
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
                        AddDocument(view);
                };
                dropDown.Items.Add(item);
            }
        }

        internal void newMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            ToolStripDropDown dropdown = ((ToolStripDropDownItem) sender).DropDown;

            while (dropdown.Items.ContainsKey("8:12"))
            {
                dropdown.Items.RemoveByKey("8:12");
            }
        }

        private void closeProjectMenuItem_Click(object sender, EventArgs e)
        {
            CloseCurrentProject();
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
            NewProjectDialog npf = new NewProjectDialog(rootPath);

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
                if (!CloseCurrentProject()) return;
                if (BuildEngine.Prep(npf.NewProject))
                {
                    npf.NewProject.Save();
                    OpenProject(npf.NewProject.FileName, false);
                    startPageView.RepopulateProjects();
                }
                else
                {
                    Directory.Delete(npf.NewProject.RootPath, true);
                }
            }
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            string[] fileNames = GetFilesToOpen(false);
            if (fileNames == null) return;
            OpenFile(fileNames[0]);
        }

        private void openLastProjectMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Session.Settings.LastProject))
                OpenProject(Session.Settings.LastProject, false);
            else
                UpdateControls();
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
            string savePath = IsProjectOpen ? Session.Project.RootPath : null;
            if (currentTab != null)
                currentTab.Save(savePath);
        }

        private void saveAllMenuItem_Click(object sender, EventArgs e)
        {
            SaveAllDocuments();
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            string savePath = IsProjectOpen ? Session.Project.RootPath : null;
            if (currentTab != null)
                currentTab.SaveAs(savePath);
        }
        #endregion

        #region Edit menu Click handlers
        private void editMenu_DropDownOpening(object sender, EventArgs e)
        {
            cutMenuItem.Enabled = selectAllMenuItem.Enabled = currentTab != null;
            copyToolButton.Enabled = copyMenuItem.Enabled = redoMenuItem.Enabled = undoMenuItem.Enabled = currentTab != null;
            pasteMenuItem.Enabled = pasteToolButton.Enabled = true;
            zoomInMenuItem.Enabled = zoomOutMenuItem.Enabled = currentTab != null;
            topMenu_DropDownOpening(sender, e);
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

            startPageMenuItem.Checked = currentTab == startPageTab;
            var tabList = from tab in tabs where tab != startPageTab
                          select tab;
            if (tabList.Count() > 0)
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

            topMenu_DropDownOpening(sender, e);
        }

        private void viewMenu_DropDownClosed(object sender, EventArgs e)
        {
            for (int i = 0; i < viewMenu.DropDownItems.Count; ++i)
            {
                if (viewMenu.DropDownItems[i].Name == "zz_v")
                {
                    viewMenu.DropDownItems.RemoveAt(i);
                    i--;
                }
            }
            topMenu_DropDownClosed(sender, e);
        }

        private void closeDocumentMenuItem_Click(object sender, EventArgs e)
        {
            if (mainDockPanel.ActiveDocument == null) return;

            if (mainDockPanel.ActiveDocument is DockContent &&
                ((DockContent)mainDockPanel.ActiveDocument).Controls[0] is StartPageView)
            {
                startPageMenuItem.PerformClick();
            }
            else mainDockPanel.ActiveDocument.DockHandler.Close();
        }

        void documentMenuItem_Click(object sender, EventArgs e)
        {
            DocumentTab tab = GetDocument(((ToolStripItem)sender).Tag as string);
            if (tab != null)
                tab.Activate();
            else
                SelectDockPane((string)((ToolStripMenuItem)sender).Tag);
        }

        private void startPageMenuItem_Click(object sender, EventArgs e)
        {
            StartVisible = true;
            startPageTab.Activate();
        }
        #endregion

        #region Project menu Click handlers
        private void exploreProjectMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", $@"/select,""{Session.Project.FileName}""")
                .Dispose();
        }

        private void projectPropertiesMenuItem_Click(object sender, EventArgs e)
        {
            showProjectProperties();
        }

        private void refreshProjectMenuItem_Click(object sender, EventArgs e)
        {
            RefreshProject();
        }
        #endregion

        #region Build menu Click handlers
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
        private void breakNowMenuItem_Click(object sender, EventArgs e)
        {
            Debugger.Pause();
        }

        private async void buildRunMenuItem_Click(object sender, EventArgs e)
        {
            if (Debugger != null)
                await Debugger.Resume();
            else
                await StartEngine(true);
        }

        private async void rebuildRunMenuItem_Click(object sender, EventArgs e)
        {
            await StartEngine(true, true);
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
            await StartEngine(false, true);
        }
        #endregion

        #region Settings menu Click handlers
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
            using (var about = new AboutDialog())
                about.ShowDialog();
        }
        #endregion

        #region Configuration Selector handlers
        private void engineToolComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingPresets) return;

            // user selected Configure (always at bottom)
            if (engineToolComboBox.SelectedIndex == engineToolComboBox.Items.Count - 1)
            {
                showPluginManager();
                return;
            }

            Session.Project.User.Engine = engineToolComboBox.Text;
            UpdateControls();
            UpdateEngineList();
        }
        #endregion

        #region Private IDE routines
        private void InitializeDocking()
        {
            fileListPane = new FileListPane(this);
            dockManager = new DockManager(mainDockPanel);
        }

        /// <summary>
        /// Searches open document tabs for one with a specified filename.
        /// </summary>
        /// <param name="filepath">The name of the file to search for.</param>
        /// <returns>The DocumentTab of the document, or null if none was found.</returns>
        internal DocumentTab GetDocument(string filepath)
        {
            foreach (DocumentTab tab in tabs)
            {
                if (tab.FileName == filepath) return tab;
            }
            return null;
        }

        internal string[] GetFilesToOpen(bool multiSelect)
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

        /// <summary>
        /// Sets the default active document when the editor first starts up.
        /// Used internally when dragging a file onto the executable.
        /// </summary>
        /// <param name="name">File path of the document to set.</param>
        internal void SetDefaultActive(string name)
        {
            defaultActiveDocumentName = name;
        }

        private bool IsProjectOpen
        {
            get { return Session.Project != null; }
        }

        internal DocumentTab AddDocument(DocumentView view, string filepath = null, bool restoreView = false)
        {
            DocumentTab tab = new DocumentTab(this, view, filepath, restoreView);
            tab.Closed += (sender, e) => tabs.Remove(tab);
            tab.Activate();
            tabs.Add(tab);
            return tab;
        }

        /// <summary>
        /// Refreshes the GUI.
        /// </summary>
        private void ApplyRefresh(bool ignore_presets = false)
        {
            if (!ignore_presets)
                UpdateEngineList();

            UpdateControls();
            SuspendLayout();
            startPageView.RepopulateProjects();
            UpdateMenuItems();
            Invalidate(true);
            ResumeLayout();
        }

        /// <summary>
        /// Closes all opened documents; optionally saving them as well.
        /// </summary>
        /// <param name="forceClose">If true, closes unsaved documents without prompting.</param>
        /// <returns>true if all documents were closed, false if a save prompt was canceled.</returns>
        private bool CloseAllDocuments(bool forceClose = false)
        {
            DocumentTab[] toClose = (from tab in tabs select tab).ToArray();
            if (!forceClose)
            {
                foreach (DocumentTab tab in toClose)
                    if (!tab.PromptSave()) return false;
            }
            foreach (DocumentTab tab in toClose)
                tab.Close(true);

            StartVisible = false;
            return true;
        }

        /// <summary>
        /// Closes the current project and all open documents.
        /// </summary>
        /// <param name="forceClose">If true, closes unsaved documents without prompting.</param>
        /// <returns>'true' if the project was closed; 'false' on cancel.</returns>
        private bool CloseCurrentProject(bool forceClose = false)
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
            Session.Project.User.StartPageHidden = !StartVisible;
            Session.Project.User.Documents = tabs
                .Where(it => it.FileName != null)
                .Select(it => it.FileName)
                .ToArray();
            Session.Project.User.ActiveDocument = currentTab != null
                ? currentTab.FileName : "";

            // close all open document tabs
            if (!CloseAllDocuments(forceClose))
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
            openLastProjectMenuItem.Enabled = (Session.Settings.LastProject.Length > 0);

            // all clear!
            Text = Versioning.Name;
            UpdateEngineList();
            UpdateControls();
            return true;
        }

        // Needed to make sure menu items are visible on a variety of
        // color themes. Eg, White text on a white theme = unreadable.
        private void CreateRootMenuItem(ToolStripMenuItem item)
        {
            item.DropDownOpening += topMenu_DropDownOpening;
            item.DropDownClosed += topMenu_DropDownClosed;
        }

        private ToolStripMenuItem GetMenuItem(ToolStripItemCollection collection, string name)
        {
            return collection.OfType<ToolStripMenuItem>().FirstOrDefault(item => item.Text.Replace("&", "") == name);
        }

        private void showPluginManager()
        {
            showPreferencesDialog("Plugins");
        }
        
        private void showPreferencesDialog(string pageName = null)
        {
            var form = new PreferencesDialog(pageName);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Session.Settings.Apply();
                ApplyRefresh();
            }
        }

        private void showProjectProperties()
        {
            var form = new ProjectPropertiesDialog(Session.Project);
            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateEngineList();
                UpdateControls();
                RefreshProject();
            }
        }

        private void RefreshProject()
        {
            var ideName = Versioning.IsWiP ? $"{Versioning.Name} WiP" : Versioning.Name;
            Text = Session.Project.GameOnly
                ? $"{Project.Name} (Sphere Game) - {ideName}"
                : $"{Project.Name} - {ideName}";
            fileListPane.Open();
            fileListPane.Refresh();
            if (Session.Project != null)
                Session.Settings.LastProject = Session.Project.FileName;
            UpdateControls();
        }

        private void SaveAllDocuments()
        {
            foreach (DocumentTab tab in tabs)
            {
                tab.Save();
            }
        }

        /// <summary>
        /// Selects a dock pane by tab name, this is not ideal for documents but useful for
        /// persistent panes like the project tree and plugins.
        /// </summary>
        /// <param name="name">The registered name of the dock pane to select.</param>
        private void SelectDockPane(string name)
        {
            foreach (IDockContent content in mainDockPanel.Contents)
                if (content.DockHandler.TabText == name)
                    content.DockHandler.Activate();
        }

        private async Task StartEngine(bool wantDebugger, bool rebuilding = false)
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
                        var breaks = Session.Project.GetAllBreakpoints();
                        foreach (string filename in breaks.Keys)
                            foreach (int lineNumber in breaks[filename])
                                await Debugger.SetBreakpoint(filename, lineNumber);
                        await Debugger.Resume();
                    }
                    else
                    {
                        SystemSounds.Hand.Play();
                        helpStatusLabel.Text = "An error occurred launching a debugging session.";
                        Debugger = null;
                        UpdateControls();
                    }
                }
            }
            else
            {
                await BuildEngine.Test(Session.Project, rebuilding);
            }

            UpdateControls();
        }

        private void UpdateControls()
        {
            var starter = IsProjectOpen
                ? PluginManager.Get<IStarter>(Session.Project.User.Engine)
                : null;
            bool haveConfig = starter != null && starter.CanConfigure;
            bool haveLastProject = !string.IsNullOrEmpty(Session.Settings.LastProject);

            configureEngineToolButton.Enabled = configureEngineMenuItem.Enabled = haveConfig;

            packageGameMenuItem.Enabled = Session.Project != null
                && BuildEngine.CanPackage(Session.Project);

            testGameMenuItem.Enabled = testGameToolButton.Enabled = Session.Project != null
                && BuildEngine.CanTest(Session.Project) && Debugger == null;
            buildRunMenuItem.Enabled = Session.Project != null && (Debugger == null || !Debugger.Running);
            rebuildRunMenuItem.Enabled = Session.Project != null && Debugger == null;
            runGameToolButton.Enabled = Session.Project != null && (Debugger == null || !Debugger.Running);
            rebuildRunToolMenuItem.Enabled = Session.Project != null && Debugger == null;
            breakNowMenuItem.Enabled = pauseToolButton.Enabled = Debugger != null && Debugger.Running;
            stopDebuggingMenuItem.Enabled = stopToolButton.Enabled = Debugger != null;
            stepIntoMenuItem.Enabled = Debugger != null && !Debugger.Running;
            stepOutMenuItem.Enabled = Debugger != null && !Debugger.Running;
            stepOverMenuItem.Enabled = Debugger != null && !Debugger.Running;

            openLastProjectMenuItem.Enabled = haveLastProject;

            projectPropertiesMenuItem.Enabled = projectPropertiesToolButton.Enabled = IsProjectOpen;
            exploreProjectMenuItem.Enabled = refreshProjectMenuItem.Enabled = IsProjectOpen;

            saveToolButton.Enabled = currentTab != null && currentTab.View.CanSave;
            cutToolButton.Enabled = currentTab != null;
            copyToolButton.Enabled = currentTab != null;

            if (dockManager != null) dockManager.Refresh();
        }

        private void UpdateMenuItems()
        {
            foreach (ToolStripMenuItem item in MainMenuStrip.Items)
                topMenu_DropDownClosed(item, null);
        }

        private void UpdateEngineList()
        {
            bool wasLoadingPresets = loadingPresets;
            loadingPresets = true;

            engineToolComboBox.Items.Clear();
            string[] engines = PluginManager.GetNames<IStarter>();
            if (IsProjectOpen && engines.Length > 0)
            {
                foreach (string name in engines)
                    engineToolComboBox.Items.Add(name);
                engineToolComboBox.Items.Add("Plugin Manager...");
                engineToolComboBox.Text = Session.Project.User.Engine;
                engineToolComboBox.Enabled = true;
            }
            else
            {
                engineToolComboBox.Enabled = false;
            }

            loadingPresets = wasLoadingPresets;
        }
        #endregion

        private void debugger_Detached(object sender, EventArgs e)
        {
            var scriptViews = from tab in tabs
                              where tab.View is TextView
                              select tab.View;
            foreach (TextView view in scriptViews)
            {
                view.ActiveLine = 0;
                view.ErrorLine = 0;
            }
            Debugger = null;
            buildRunMenuItem.Text = "Build && &Run";
            buildRunToolMenuItem.Text = "Build && &Run (Default)";
            runGameToolButton.Text = "&Run Game";
            UpdateControls();
        }

        private void debugger_Paused(object sender, PausedEventArgs e)
        {
            if (isFirstDebugStop)
            {
                // ignore first pause
                isFirstDebugStop = false;
                return;
            }

            TextView view = null;
            view = OpenFile(Debugger.FileName) as TextView;
            if (view != null)
            {
                view.ActiveLine = Debugger.LineNumber;
                if (e.Reason == PauseReason.Exception)
                    view.ErrorLine = Debugger.LineNumber;
            }
            if (!Debugger.Running)
                Activate();
            UpdateControls();
        }

        private void debugger_Resumed(object sender, EventArgs e)
        {
            var scriptViews = from tab in tabs
                              where tab.View is TextView
                              select tab.View;
            foreach (TextView view in scriptViews)
            {
                view.ActiveLine = 0;
                view.ErrorLine = 0;
            }
            UpdateControls();
        }
    }
}