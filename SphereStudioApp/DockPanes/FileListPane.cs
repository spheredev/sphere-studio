using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

using SphereStudio.Base;
using SphereStudio.Core;
using SphereStudio.Forms;
using SphereStudio.Properties;
using SphereStudio.UI;

namespace SphereStudio.DockPanes
{
    [ToolboxItem(false)]
    partial class FileListPane : UserControl, IDockPane, IStyleAware
    {
        private ImageList iconImageList = new ImageList();
        private IdeWindowForm ideWindow;

        public FileListPane(IdeWindowForm ideWindow)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            this.ideWindow = ideWindow;

            // TODO: fix this hack ('New' submenu in FileListPane context menu)
            newMenuItem.DropDown = this.ideWindow.newMenuItem.DropDown;
            newMenuItem.DropDownOpening += this.ideWindow.newMenuItem_DropDownOpening;
            newMenuItem.DropDownClosed += this.ideWindow.newMenuItem_DropDownClosed;

            fileTreeView.ImageList = iconImageList;
            iconImageList.ColorDepth = ColorDepth.Depth32Bit;
        }

        public bool ShowInViewMenu => true;
        public Control Control => this;
        public DockHint DockHint => DockHint.Right;
        public Bitmap DockIcon => Resources.SphereEditor;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsHeading(header);
            style.AsTextView(fileTreeView);
        }

        public void Close()
        {
            fileWatcher.EnableRaisingEvents = false;
            fileTreeView.Nodes.Clear();
        }

        public void Open()
        {
            if (!string.IsNullOrEmpty(Session.Project.RootPath))
            {
                fileWatcher.Path = Session.Project.RootPath;
                fileWatcher.EnableRaisingEvents = true;
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            if (Session.Project == null || string.IsNullOrEmpty(Session.Project.RootPath))
                return;

            // update the icons
            iconImageList.Images.Clear();
            iconImageList.Images.Add(Resources.SphereEditor);
            iconImageList.Images.Add(Resources.folder_closed);
            iconImageList.Images.Add(Resources.folder);
            iconImageList.Images.Add(Resources.new_item);
            foreach (var name in PluginManager.GetNames<IFileOpener>())
            {
                var plugin = PluginManager.Get<IFileOpener>(name);
                iconImageList.Images.Add(name, plugin.FileIcon ?? Resources.new_item);
            }

            Cursor.Current = Cursors.WaitCursor;

            // save currently selected item and folder expansion states
            var selectedNodePath = fileTreeView.SelectedNode?.FullPath;
            var isExpandedTable = new Dictionary<string, bool>();
            var nodesToCheck = new Queue<TreeNode>();
            if (fileTreeView.TopNode != null)
            {
                nodesToCheck.Enqueue(fileTreeView.TopNode);
                while (nodesToCheck.Count > 0)
                {
                    TreeNode node = nodesToCheck.Dequeue();
                    isExpandedTable.Add(node.FullPath, node.IsExpanded);
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        // emulate a recursive search of the tree view:
                        nodesToCheck.Enqueue(subnode);
                    }
                }
            }

            // repopulate the file tree
            fileTreeView.BeginUpdate();
            fileTreeView.Nodes.Clear();
            var projectNode = new TreeNode(Session.Project.Name) { Tag = "projectNode" };
            fileTreeView.Nodes.Add(projectNode);
            var baseDir = new DirectoryInfo(fileWatcher.Path);
            populateFolderNode(fileTreeView.Nodes[0], baseDir);

            // re-expand folders and try to select the previously-selected item
            if (fileTreeView.TopNode != null)
            {
                nodesToCheck.Clear();
                nodesToCheck.Enqueue(fileTreeView.TopNode);
                while (nodesToCheck.Count > 0)
                {
                    var node = nodesToCheck.Dequeue();
                    isExpandedTable.TryGetValue(node.FullPath, out var isExpanded);
                    if (isExpanded)
                        node.Expand();
                    if (node.FullPath == selectedNodePath)
                        fileTreeView.SelectedNode = node;
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        // emulate a recursive search of the tree view:
                        nodesToCheck.Enqueue(subnode);
                    }
                }
            }

            if (fileTreeView.SelectedNode == null)
                fileTreeView.SelectedNode = fileTreeView.TopNode;
            if (!fileTreeView.Nodes[0].IsExpanded)
                fileTreeView.Nodes[0].Expand();
            Cursor.Current = Cursors.Default;
            fileTreeView.EndUpdate();
        }

        private void deleteNode(TreeNode node)
        {
            var path = getFullPath(node);
            switch ((string)node.Tag)
            {
                case "fileNode":
                    if (!File.Exists(path))
                        return;
                    FileSystem.DeleteFile(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                    break;
                case "folderNode":
                    if (!Directory.Exists(path))
                        return;
                    FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                    break;
            }
        }

        private string getFullPath(TreeNode node)
        {
            var projectRoot = Session.Project.RootPath;
            if ((string)node.Tag != "projectNode")
            {
                var nodePath = node.FullPath.Substring(node.FullPath.IndexOf(@"\") + 1);
                return Path.Combine(projectRoot, nodePath);
            }
            else
            {
                return projectRoot;
            }
        }

        private void openNode(TreeNode node)
        {
            if (ideWindow == null || node == null)
                return;

            var path = getFullPath(node);
            if ((string)node.Tag == "fileNode")
                ideWindow.OpenFile(path);
        }

        private void pauseFileWatcher(bool paused)
        {
            if (string.IsNullOrEmpty(fileWatcher.Path))
                return;
            fileWatcher.EnableRaisingEvents = !paused;
        }

        private void populateFolderNode(TreeNode baseNode, DirectoryInfo dir)
        {
            var dirInfos = from dirInfo in dir.GetDirectories()
                           where !dirInfo.Attributes.HasFlag(FileAttributes.Hidden)
                           orderby dirInfo.Name
                           select dirInfo;
            foreach (var dirInfo in dirInfos)
            {
                var subNode = new TreeNode(dirInfo.Name, 1, 1) { Tag = "folderNode" };
                baseNode.Nodes.Add(subNode);
                populateFolderNode(subNode, dirInfo);
            }

            var fileInfos = from fileInfo in dir.GetFiles()
                            where !fileInfo.Attributes.HasFlag(FileAttributes.Hidden)
                            orderby fileInfo.Name
                            select fileInfo;
            foreach (var fileInfo in fileInfos)
            {
                var subNode = new TreeNode(fileInfo.Name) { Tag = "fileNode" };
                updateImage(subNode);
                baseNode.Nodes.Add(subNode);
            }
        }

        private void updateImage(TreeNode node)
        {
            var pluginName = Session.GetFileOpenerName(node.Text);
            if (pluginName != null)
            {
                node.ImageKey = pluginName;
                node.SelectedImageKey = node.ImageKey;
            }
            else
            {
                node.ImageIndex = 3;
                node.SelectedImageIndex = node.ImageIndex;
            }
        }

        private void fileTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageIndex == 2)
            {
                e.Node.ImageIndex = 1;
                e.Node.SelectedImageIndex = 1;
            }
        }

        private void fileTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageIndex == 1)
            {
                e.Node.ImageIndex = 2;
                e.Node.SelectedImageIndex = 2;
            }
        }

        private void fileTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null || e.Label == e.Node.Text)
                return;

            var oldPath = getFullPath(e.Node);
            var oldRootPath = Path.GetDirectoryName(oldPath);
            var newPath = Path.Combine(oldRootPath, e.Label);
            if (!File.Exists(newPath) && !Directory.Exists(newPath))
            {
                pauseFileWatcher(true);
                Directory.Move(oldPath, newPath);
                pauseFileWatcher(false);
            }
            else
            {
                MessageBox.Show(
                    $"A file or directory with that name already exists. Please choose a different name.\n\nRenaming: {oldPath}\nTo: {newPath}",
                    "File Replacement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
                e.Node.BeginEdit();
            }
        }

        private void fileTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if ((string)e.Node.Tag == "projectNode")
                e.CancelEdit = true;
        }

        private void fileTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            var node = fileTreeView.SelectedNode;
            if (node == null)
                return;
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if ((string)node.Tag != "fileNode")
                        return;
                    openNode(fileTreeView.SelectedNode);
                    e.Handled = true;
                    break;
                case Keys.Delete:
                    if ((string)node.Tag == "projectNode")
                        return;
                    deleteNode(fileTreeView.SelectedNode);
                    e.Handled = true;
                    break;
                case Keys.F2:
                    fileTreeView.SelectedNode.BeginEdit();
                    e.Handled = true;
                    break;
            }
        }

        private void fileTreeView_KeyPress(object sender, KeyPressEventArgs e)
        {
            // prevent the annoying beep when pressing Enter in the treeview
            if (e.KeyChar == '\r')
                e.Handled = true;
        }

        private void fileTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                fileTreeView.SelectedNode = e.Node;

                copyPathMenuItem.Visible = false;
                deleteMenuItem.Visible = false;
                importMenuItem.Visible = false;
                newMenuItem.Visible = false;
                newFolderMenuItem.Visible = false;
                openMenuItem.Visible = false;
                projectPropertiesMenuItem.Visible = false;
                renameMenuItem.Visible = false;
                switch ((string)e.Node.Tag)
                {
                    case "fileNode":
                        copyPathMenuItem.Visible = true;
                        deleteMenuItem.Visible = true;
                        openMenuItem.Visible = true;
                        renameMenuItem.Visible = true;
                        break;
                    case "folderNode":
                        copyPathMenuItem.Visible = true;
                        deleteMenuItem.Visible = true;
                        importMenuItem.Visible = true;
                        newMenuItem.Visible = true;
                        newFolderMenuItem.Visible = true;
                        renameMenuItem.Visible = true;
                        break;
                    case "projectNode":
                        newFolderMenuItem.Visible = true;
                        projectPropertiesMenuItem.Visible = true;
                        break;
                }
                contextMenu.Show(fileTreeView, e.Location);
            }
        }

        private void fileTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            openNode(fileTreeView.SelectedNode);
        }

        private void fileWatcher_Created(object sender, IEnumerable<FileSystemEventArgs> eAll)
        {
            Refresh();
        }

        private void fileWatcher_Deleted(object sender, IEnumerable<FileSystemEventArgs> eAll)
        {
            Refresh();
        }

        private void fileWatcher_Renamed(object sender, IEnumerable<FileSystemEventArgs> eAll)
        {
            Refresh();
        }

        private void copyPathMenuItem_Click(object sender, EventArgs e)
        {
            var path = getFullPath(fileTreeView.SelectedNode);
            Clipboard.SetText(path, TextDataFormat.Text);
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            deleteNode(fileTreeView.SelectedNode);
        }

        private void exploreMenuItem_Click(object sender, EventArgs e)
        {
            var path = getFullPath(fileTreeView.SelectedNode);
            Process.Start("explorer.exe", $@"/select,""{path}""");
        }

        private void importMenuItem_Click(object sender, EventArgs e)
        {
            var path = getFullPath(fileTreeView.SelectedNode);
            var filesToAdd = ideWindow.getFilesToOpen(true);

            if (filesToAdd == null || filesToAdd.Length == 0)
                return;

            foreach (var sourcePath in filesToAdd)
            {
                var newPath = Path.Combine(path, Path.GetFileName(sourcePath));
                var canCopy = true;
                if (File.Exists(newPath))
                {
                    var text = $@"A file with the name '{newPath}' already exists.  Do you want to overwrite it with the file you're importing?";
                    canCopy = MessageBox.Show(text, "Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
                }
                if (canCopy)
                    File.Copy(sourcePath, newPath, true);
            }
        }

        private void newFolderMenuItem_Click(object sender, EventArgs e)
        {
            var rootPath = getFullPath(fileTreeView.SelectedNode);
            using (var form = new StringInputForm("New Folder", $"create a new folder in {rootPath}"))
            {
                form.Input = "untitled";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var path = Path.Combine(rootPath, form.Input);
                    if (!Directory.Exists(path) && !File.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        var node = new TreeNode(form.Input, 2, 1) { Tag = "folderNode" };
                        fileTreeView.SelectedNode.Nodes.Add(node);
                    }
                    else
                    {
                        MessageBox.Show("A file or directory by that name already exists.", "Directory Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            openNode(fileTreeView.SelectedNode);
        }

        private void projectPropertiesMenuItem_Click(object sender, EventArgs e)
        {
            var result = new ProjectPropertiesForm(Session.Project).ShowDialog();
            if (result == DialogResult.OK)
            {
                ideWindow.Refresh();
            }
        }

        private void renameMenuItem_Click(object sender, EventArgs e)
        {
            fileTreeView.SelectedNode.BeginEdit();
        }
    }
}