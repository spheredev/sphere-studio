using System.Drawing;

using SphereStudio.Base;
using SphereStudio.DocumentViews;
using SphereStudio.Properties;

namespace SphereStudio.FileOpeners
{
    class TextFileOpener : INewFileOpener
    {
        private PluginMain plugin;
        
        public TextFileOpener(PluginMain plugin)
        {
            this.plugin = plugin;
        }
        
        public string FileTypeName => "JavaScript Code";

        public string[] FileExtensions => new[] { "js", "mjs", "ts", "json" };

        public Bitmap FileIcon => Resources.ScriptIcon;

        public DocumentView New()
        {
            var scriptView = new SphereTextView(plugin);
            return scriptView.NewDocument() ? scriptView : null;
        }

        public DocumentView Open(string fileName)
        {
            var scriptView = new SphereTextView(plugin);
            scriptView.Load(fileName);
            return scriptView;
        }
    }
}
