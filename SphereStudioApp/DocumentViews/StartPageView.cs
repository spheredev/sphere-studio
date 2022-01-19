using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Core;
using SphereStudio.Forms;
using SphereStudio.Properties;

namespace SphereStudio.DocumentViews
{
    [ToolboxItem(false)]
    partial class StartPageView : DocumentView, IStyleAware
    {
        private ImageList iconImageList = new ImageList();
        private IdeWindowForm ideWindow;
        private ImageList smallIconImageList = new ImageList();

        public StartPageView(IdeWindowForm ideWindow)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            Icon = Icon.FromHandle(Resources.SphereEditor.GetHicon());

            this.ideWindow = ideWindow;

            iconImageList.ImageSize = new Size(48, 48);
            iconImageList.ColorDepth = ColorDepth.Depth32Bit;
            iconImageList.Images.Add(Resources.SphereEditor);
            smallIconImageList.ImageSize = new Size(16, 16);
            smallIconImageList.ColorDepth = ColorDepth.Depth32Bit;
            smallIconImageList.Images.Add(Resources.SphereEditor);

            projectListView.LargeImageList = iconImageList;
            projectListView.SmallImageList = smallIconImageList;
            projectListView.View = Session.Settings.StartPageView;
        }

        public override bool CanSave => false;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);

            style.AsHeading(header);
            style.AsTextView(projectListView);
        }

        public override void Refresh()
        {
            iconImageList.Images.Clear();
            iconImageList.Images.Add(Resources.SphereEditor);
            smallIconImageList.Images.Clear();
            smallIconImageList.Images.Add(Resources.SphereEditor);

            projectListView.BeginUpdate();
            projectListView.Items.Clear();

            // search through a list of supplied directories.
            var projectsDirPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Sphere Projects");
            Directory.CreateDirectory(projectsDirPath);
            var paths = new List<string>(Session.Settings.ProjectPaths);
            paths.Insert(0, projectsDirPath);
            foreach (string path in paths)
            {
                if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                    continue;
                var baseDir = new DirectoryInfo(path);
                var ssprojFileInfos = baseDir.GetFiles("*.ssproj", SearchOption.AllDirectories);
                var ssprojDirs = ssprojFileInfos.Select(fi => $@"{fi.DirectoryName}\");
                foreach (var fileInfo in ssprojFileInfos)
                {
                    var projectRoot = Path.GetDirectoryName(fileInfo.FullName);
                    var imageIndex = getImageIndex(projectRoot);
                    var proj = Project.Open(fileInfo.FullName);
                    var item = new ListViewItem(proj.Name, imageIndex) { Tag = fileInfo.FullName };
                    item.SubItems.Add(proj.Compiler);
                    item.SubItems.Add(proj.Author);
                    item.SubItems.Add(fileInfo.FullName);
                    projectListView.Items.Add(item);
                }
                var sgmFileInfos = from fi in baseDir.GetFiles("game.sgm", SearchOption.AllDirectories)
                                   where !ssprojDirs.Any(x => fi.FullName.StartsWith(x))
                                   select fi;
                foreach (var fileInfo in sgmFileInfos)
                {
                    var projectRoot = Path.GetDirectoryName(fileInfo.FullName);
                    var imageIndex = getImageIndex(projectRoot);
                    var proj = Project.Open(fileInfo.FullName);
                    var item = new ListViewItem(proj.Name, imageIndex) { Tag = fileInfo.FullName };
                    item.SubItems.Add("Sphere Game");
                    item.SubItems.Add(proj.Author);
                    item.SubItems.Add(fileInfo.FullName);
                    projectListView.Items.Add(item);
                }
            }
            projectListView.EndUpdate();

            base.Refresh();
        }

        private int getImageIndex(string projectRoot)
        {
            try
            {
                var paths = Directory.GetFiles(projectRoot);
                foreach (var path in paths)
                {
                    if (path.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        iconImageList.Images.Add(Image.FromFile(path));
                        smallIconImageList.Images.Add(Image.FromFile(path));
                        return iconImageList.Images.Count - 1;
                    }
                    if (path.EndsWith(".ico", StringComparison.OrdinalIgnoreCase))
                    {
                        iconImageList.Images.Add(new Icon(path));
                        smallIconImageList.Images.Add(new Icon(path));
                        return iconImageList.Images.Count - 1;
                    }
                }
            }
            catch
            {
                // *MUNCH*
            }
            return 0;
        }

        private void projectListView_ItemActivate(object sender, EventArgs e)
        {
            var selectedItem = projectListView.SelectedItems[0];
            ideWindow.OpenProject((string)selectedItem.Tag);
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            var haveProject = projectListView.SelectedItems.Count > 0;
            var view = projectListView.View;

            exploreMenuItem.Enabled = haveProject;
            openMenuItem.Enabled = haveProject;
            setIconMenuItem.Enabled = haveProject;
            testGameMenuItem.Enabled = haveProject;

            detailsViewMenuItem.Checked = view == View.Details;
            largeIconsViewMenuItem.Checked = view == View.LargeIcon;
            listViewMenuItem.Checked = view == View.List;
            smallIconsViewMenuItem.Checked = view == View.SmallIcon;
            tilesViewMenuItem.Checked = view == View.Tile;
        }

        private void detailsViewMenuItem_Click(object sender, EventArgs e)
        {
            projectListView.View = View.Details;
            Session.Settings.StartPageView = projectListView.View;
        }

        private void exploreMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = projectListView.SelectedItems[0];
            var path = (string)selectedItem.Tag;
            Process.Start("explorer.exe", $@"/select,""{path}""");
        }

        private void largeIconsViewMenuItem_Click(object sender, EventArgs e)
        {
            projectListView.View = View.LargeIcon;
            Session.Settings.StartPageView = projectListView.View;
        }

        private void listViewMenuItem_Click(object sender, EventArgs e)
        {
            projectListView.View = View.List;
            Session.Settings.StartPageView = projectListView.View;
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = projectListView.SelectedItems[0];
            ideWindow.OpenProject((string)selectedItem.Tag);
        }

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void setIconMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = projectListView.SelectedItems[0];
            using (var dialog = new OpenFileDialog())
            {
                var projectRoot = Path.GetDirectoryName((string)selectedItem.Tag);
                var iconFilePath = Path.Combine(projectRoot, "icon.png");
                dialog.Filter = @"PNG Image Files (.png)|*.png";
                dialog.InitialDirectory = projectRoot;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.FileName == iconFilePath)
                        return;
                    if (File.Exists(iconFilePath))
                        File.Delete(iconFilePath);
                    File.Copy(dialog.FileName, iconFilePath);
                    if (selectedItem.ImageIndex == 0)
                    {
                        iconImageList.Images.Add(Image.FromFile(iconFilePath));
                        selectedItem.ImageIndex = iconImageList.Images.Count - 1;
                    }
                    else
                    {
                        iconImageList.Images.RemoveAt(selectedItem.ImageIndex);
                        iconImageList.Images.Add(Image.FromFile(iconFilePath));
                        selectedItem.ImageIndex = iconImageList.Images.Count - 1;
                    }
                    Refresh();
                }
            }
        }

        private void smallIconsViewMenuItem_Click(object sender, EventArgs e)
        {
            projectListView.View = View.SmallIcon;
            Session.Settings.StartPageView = projectListView.View;
        }

        private async void testGameMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = projectListView.SelectedItems[0];
            var project = Project.Open((string)selectedItem.Tag);
            await BuildEngine.Test(project);
        }

        private void tilesViewMenuItem_Click(object sender, EventArgs e)
        {
            projectListView.View = View.Tile;
            Session.Settings.StartPageView = projectListView.View;
        }
    }
}
