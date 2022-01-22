using System;
using System.IO;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace SphereStudio.Core
{
    /// <summary>
    /// Represents an open document in the IDE.
    /// </summary>
    class DocumentTab : IDisposable
    {
        private static uint untitledCounter = 1;
        
        private DockContent dockContent;
        private IdeWindowForm ideWindow;
        private string tabText;
        
        /// <summary>
        /// Creates a new Sphere Studio document tab.
        /// </summary>
        /// <param name="ideWindow">The IDE form that the tab will be created in.</param>
        /// <param name="view">The <c>DocumentView</c> that will be hosted in the new tab.</param>
        /// <param name="fileName">The fully-qualified filename of the document, or <c>null</c> if untitled.</param>
        /// <param name="restoreView">A boolean value specifying whether to restore the last saved view state. Has no effect on untitled tabs.</param>
        public DocumentTab(IdeWindowForm ideWindow, DocumentView view, string fileName = null, bool restoreView = false)
        {
            FileName = fileName;
            View = view;

            View.Dock = DockStyle.Fill;

            this.ideWindow = ideWindow;
            tabText = fileName != null ? Path.GetFileName(fileName)
                : $"Untitled {untitledCounter++}";
            dockContent = new DockContent();
            dockContent.FormClosing += dockContent_FormClosing;
            dockContent.FormClosed += dockContent_FormClosed;
            dockContent.Tag = this;
            dockContent.Icon = View.Icon;
            dockContent.TabText = tabText;
            dockContent.ToolTipText = FileName;
            dockContent.Controls.Add(View);
            dockContent.Show(ideWindow.mainDockPanel, DockState.Document);
            View.DirtyChanged += documentView_DirtyChanged;

            refreshTabText();

            if (View is TextView textView)
            {
                textView.Breakpoints = Session.Project.GetBreakpoints(FileName);
                textView.BreakpointChanged += textView_BreakpointSet;
            }

            if (restoreView && FileName != null)
            {
                try
                {
                    var settingID = $"viewState:{FileName.GetHashCode():X8}";
                    View.ViewState = Session.Project.UserSettings.GetString(settingID, string.Empty);
                }
                catch (Exception)
                {
                    // *MUNCH*
                }
            }
        }

        public void Dispose()
        {
            dockContent.Dispose();
        }

        public event EventHandler Closed;
        
        /// <summary>
        /// Gets the fully-qualified file path for this tab. This is <c>null</c> for newly created
        /// documents that haven't been saved yet.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the tab's title text, including the trailing asterisk if the
        /// document has been modified.
        /// </summary>
        public string Title => dockContent.TabText;
        
        /// <summary>
        /// Gets the <c>DocumentView</c> being hosted by the tab.
        /// </summary>
        public DocumentView View { get; private set; }

        /// <summary>
        /// Prompts the user to save a modified document. The document will
        /// remain open afterwards.
        /// </summary>
        /// <returns>'true' if the user saved, answered No, or if the file is clean; 'false' on cancel.</returns>
        public bool PromptSave()
        {
            if (View.Dirty)
            {
                Activate();
                DialogResult result = MessageBox.Show(
                    $"{tabText}\n\nThis document has been modified. Any unsaved changes will be lost if you continue. Do you want to save it now?",
                    "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return false;
                if (result == DialogResult.Yes)
                {
                    if (!Save()) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Saves the document in this tab.  For untitled documents which haven't been saved yet,
        /// the user will be asked to provide a filename.
        /// </summary>
        /// <param name="savePath">The default directory for the Save As dialog.</param>
        public bool Save(string savePath = null)
        {
            if (View.ReadOnly)
                return true;
            if (FileName == null)
                return SaveAs(savePath);

            View.Save(FileName);
            saveViewState();
            return true;
        }

        /// <summary>
        /// Saves the document in this tab if it's been modified. This has no effect on untitled
        /// documents.
        /// </summary>
        /// <returns>A boolean indicating whether the operation was successful.</returns>
        public bool SaveIfDirty()
        {
            if (FileName == null)
                return true;

            if (!View.Dirty)
            {
                saveViewState();
                return true;
            }
            else
            {
                return Save();
            }
        }

        /// <summary>
        /// Saves the document in this tab with a filename provided by the user.
        /// </summary>
        /// <param name="savePath">The default directory for the Save As dialog.</param>
        /// <returns>A boolean value indicating whether the Save As operation succeeded.</returns>
        public bool SaveAs(string savePath = null)
        {
            using (var diag = new SaveFileDialog())
            {
                // set up the dialog parameters
                var filterString = "";
                foreach (string ext in View.FileExtensions)
                {
                    if (filterString != string.Empty)
                        filterString += "|";
                    filterString += $".{ext} File|*.{ext}";
                }
                diag.Title = "Save As";
                diag.InitialDirectory = savePath;
                diag.FileName = tabText;
                diag.Filter = filterString;
                diag.DefaultExt = View.FileExtensions[0];

                // show the Save As dialog
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    FileName = diag.FileName;
                    tabText = Path.GetFileName(FileName);
                    refreshTabText();
                    Save(savePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Makes the tab active and notifies the underlying document that it has
        /// received focus.
        /// </summary>
        public void Activate()
        {
            dockContent.DockHandler.Activate();
            View.Activate();
        }

        /// <summary>
        /// Closes the tab.
        /// </summary>
        /// <param name="forceClose">Whether to bypass the Unsaved Changes prompt.</param>
        /// <returns>A boolean value indicating whether the tab was actually closed.</returns>
        public bool Close(bool forceClose = false)
        {
            if (forceClose || PromptSave())
            {
                // unsubscribe FormClosing event to prevent duplicate prompt
                dockContent.FormClosing -= dockContent_FormClosing;

                // save the current view state and close the tab
                saveViewState();
                dockContent.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sends a Copy command to the document.
        /// </summary>
        public void Copy()
        {
            View.Copy();
        }

        /// <summary>
        /// Sends a Cut command to the document view.
        /// </summary>
        public void Cut()
        {
            View.Cut();
        }

        /// <summary>
        /// Notifies the underlying document that it has lost focus.
        /// </summary>
        public void Deactivate()
        {
            View.Deactivate();
        }

        /// <summary>
        /// Sends a Paste command to the document.
        /// </summary>
        public void Paste()
        {
            View.Paste();
        }

        /// <summary>
        /// Sends a Redo command to the document.
        /// </summary>
        public void Redo()
        {
            View.Redo();
        }

        /// <summary>
        /// Notifies the document that a styling option changed.
        /// </summary>
        public void Restyle()
        {
            View.Restyle();
        }

        /// <summary>
        /// Sends a Select All command to the document.
        /// </summary>
        public void SelectAll()
        {
            View.SelectAll();
        }

        /// <summary>
        /// Sends an Undo command to the document.
        /// </summary>
        public void Undo()
        {
            View.Undo();
        }

        /// <summary>
        /// Sends a Zoom In command to the document.
        /// </summary>
        public void ZoomIn()
        {
            View.ZoomIn();
        }

        /// <summary>
        /// Sends a Zoom Out command to the document view.
        /// </summary>
        public void ZoomOut()
        {
            View.ZoomOut();
        }

        private void refreshTabText()
        {
            dockContent.TabText = View.Dirty ? $"{tabText} *" : tabText;
            if (View.ReadOnly)
                dockContent.TabText += " (read-only)";
            dockContent.ToolTipText = FileName;
        }

        private void saveViewState()
        {
            if (FileName == null || Session.Project == null || View.Dirty)
                return;  // save view only if clean

            // record breakpoints if this is a text tab
            if (View is TextView textView)
                Session.Project.SetBreakpoints(FileName, textView.Breakpoints);

            // save view (cursor position, etc.)
            Session.Project.UserSettings.SetValue(
                $"viewState:{FileName.GetHashCode():X8}",
                View.ViewState);
        }

        private void dockContent_FormClosed(object sender, FormClosedEventArgs e)
        {
            Closed?.Invoke(this, EventArgs.Empty);
            Dispose();
        }

        private void dockContent_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !PromptSave();
            if (!e.Cancel)
                saveViewState();
        }

        private void documentView_DirtyChanged(object sender, EventArgs e)
        {
            refreshTabText();
        }

        private async void textView_BreakpointSet(object sender, BreakpointChangedEventArgs e)
        {
            if (FileName == null)
                return;
            var textView = (TextView)sender;
            Session.Project.SetBreakpoints(FileName, textView.Breakpoints);
            var debugger = ideWindow.Debugger;
            if (debugger != null)
            {
                if (e.Active)
                    await debugger.SetBreakpoint(FileName, e.LineNumber);
                else
                    await debugger.ClearBreakpoint(FileName, e.LineNumber);
            }
        }
    }
}
