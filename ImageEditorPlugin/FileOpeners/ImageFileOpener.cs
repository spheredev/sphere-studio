using System.Drawing;

using SphereStudio.Base;
using SphereStudio.DocumentViews;

namespace SphereStudio.FileOpeners
{
    class ImageFileOpener : INewFileOpener
    {
        private PluginMain plugin;

        public ImageFileOpener(PluginMain plugin)
        {
            this.plugin = plugin;
        }
        
        public string FileTypeName => "Raster Image";

        public string[] FileExtensions => new[] { "bmp", "gif", "jpg", "png", "tif", "tiff" };

        public Bitmap FileIcon => Properties.Resources.palette;

        public DocumentView New()
        {
            var imageView = new RasterImageView(plugin);
            return imageView.NewDocument() ? imageView : null;
        }

        public DocumentView Open(string fileName)
        {
            var imageView = new RasterImageView(plugin);
            imageView.Load(fileName);
            return imageView;
        }
    }
}
