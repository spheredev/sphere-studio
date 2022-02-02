using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SphereStudio.Base;
using SphereStudio.DocumentViews;

namespace SphereStudio.FileOpeners
{
    class SpritesetOpener : INewFileOpener
    {
        private PluginMain plugin;
        
        public SpritesetOpener(PluginMain plugin)
        {
            this.plugin = plugin;
        }
        
        public string FileTypeName => "Sphere Spriteset";

        public string[] FileExtensions => new[] { "rss" };

        public Bitmap FileIcon => Properties.Resources.PersonIcon;

        public DocumentView New()
        {
            var documentView = new SpritesetView(this.plugin);
            return documentView.NewDocument() ? documentView : null;
        }

        public DocumentView Open(string fileName)
        {
            var documentView = new SpritesetView(this.plugin);
            documentView.Load(fileName);
            return documentView;
        }

    }
}
