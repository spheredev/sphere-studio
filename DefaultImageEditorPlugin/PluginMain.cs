using System;
using System.Drawing;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Plugins.Forms;

namespace SphereStudio.Plugins
{
    public class PluginMain : IPluginMain, INewFileOpener, IEditor<ImageView>
    {
        public string Name => "Default Image Editor";
        public string Description => "Sphere Studio default image editor";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        public string FileTypeName => "Bitmap Image";
        public string[] FileExtensions => new[] { "bmp", "gif", "jpg", "png", "tif", "tiff" };
        public Bitmap FileIcon => Properties.Resources.palette;

        private ToolStripMenuItem imageMenu;
        private ToolStripMenuItem rescaleMenuItem;
        private ToolStripMenuItem resizeMenuItem;

        public void Initialize(ISettings conf)
        {
            imageMenu = new ToolStripMenuItem("&Image") { Visible = false };
            rescaleMenuItem = new ToolStripMenuItem("Re&scale...", Properties.Resources.arrow_inout, rescaleMenuItem_Click);
            resizeMenuItem = new ToolStripMenuItem("&Resize...", Properties.Resources.arrow_inout, resizeMenuItem_Click);
            imageMenu.DropDownItems.AddRange(new[] {
                resizeMenuItem,
                rescaleMenuItem
            });

            PluginManager.Register(this, this, Name);
            PluginManager.Core.AddMenuItem(imageMenu, "Project");
        }

        public void ShutDown()
        {
            PluginManager.Core.RemoveMenuItem(imageMenu);
            PluginManager.UnregisterAll(this);
        }

        public ImageView CreateEditView()
        {
            return new ImageEditView(this);
        }

        public DocumentView New()
        {
            var view = new ImageEditView(this);
            return view.NewDocument() ? view : null;
        }

        public DocumentView Open(string fileName)
        {
            var view = new ImageEditView(this);
            view.Load(fileName);
            return view;
        }

        internal void showMenus(bool visible)
        {
            imageMenu.Visible = visible;
        }

        private void rescaleMenuItem_Click(object sender, EventArgs e)
        {
            using (SizeForm form = new SizeForm())
            {
                ImageEditView editor = PluginManager.Core.ActiveDocument as ImageEditView;
                form.WidthSize = editor.Content.Width;
                form.HeightSize = editor.Content.Height;
                if (form.ShowDialog() == DialogResult.OK)
                    editor.Rescale(form.WidthSize, form.HeightSize, form.Mode);
            }
        }

        private void resizeMenuItem_Click(object sender, EventArgs e)
        {
            using (SizeForm form = new SizeForm())
            {
                ImageEditView editor = PluginManager.Core.ActiveDocument as ImageEditView;
                form.WidthSize = editor.Content.Width;
                form.HeightSize = editor.Content.Height;
                form.UseScale = false;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    editor.Resize(form.WidthSize, form.HeightSize);
                }
            }
        }
    }
}
