using System;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.DocumentViews;
using SphereStudio.Editors;
using SphereStudio.FileOpeners;
using SphereStudio.Forms;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Raster Image Editor";
        public string Description => "Basic raster image editor optimized for pixel art";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        private ToolStripMenuItem imageMenu;
        private ToolStripMenuItem rescaleMenuItem;
        private ToolStripMenuItem resizeMenuItem;

        public void Initialize(ISettings settings)
        {
            imageMenu = new ToolStripMenuItem("&Image") { Visible = false };
            rescaleMenuItem = new ToolStripMenuItem("Re&scale...", Properties.Resources.arrow_inout, rescaleMenuItem_Click);
            resizeMenuItem = new ToolStripMenuItem("&Resize...", Properties.Resources.arrow_inout, resizeMenuItem_Click);
            imageMenu.DropDownItems.AddRange(new[] {
                resizeMenuItem,
                rescaleMenuItem
            });

            PluginManager.Register(this, new RasterImageEditor(this), Name);
            PluginManager.Register(this, new ImageFileOpener(this), Name);
            PluginManager.Core.AddMenuItem(imageMenu, "Project");
        }

        public void ShutDown()
        {
            PluginManager.Core.RemoveMenuItem(imageMenu);
            PluginManager.UnregisterAll(this);
        }

        internal void showMenus(bool visible)
        {
            imageMenu.Visible = visible;
        }

        private void rescaleMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SizeForm())
            {
                var imageView = PluginManager.Core.ActiveDocument as RasterImageView;
                dialog.WidthSize = imageView.Content.Width;
                dialog.HeightSize = imageView.Content.Height;
                if (dialog.ShowDialog() == DialogResult.OK)
                    imageView.Rescale(dialog.WidthSize, dialog.HeightSize, dialog.Mode);
            }
        }

        private void resizeMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SizeForm())
            {
                var imageView = PluginManager.Core.ActiveDocument as RasterImageView;
                dialog.WidthSize = imageView.Content.Width;
                dialog.HeightSize = imageView.Content.Height;
                dialog.UseScale = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                    imageView.Resize(dialog.WidthSize, dialog.HeightSize);
            }
        }
    }
}
