using System.Drawing;

using SphereStudio.Base;
using SphereStudio.DocumentViews;
using SphereStudio.Properties;

namespace SphereStudio.FileOpeners
{
    class ScriptFileOpener : INewFileOpener
    {
        private PluginMain plugin;
        
        public ScriptFileOpener(PluginMain plugin)
        {
            this.plugin = plugin;
        }
        
        public string FileTypeName => "JavaScript Code";

        public string[] FileExtensions => new[] { "js", "mjs", "ts", "json" };

        public Bitmap FileIcon => Resources.ScriptIcon;

        public DocumentView New()
        {
            var scriptView = new ScriptTextView(plugin);
            return scriptView.NewDocument() ? scriptView : null;
        }

        public DocumentView Open(string fileName)
        {
            var scriptView = new ScriptTextView(plugin);
            scriptView.Load(fileName);
            return scriptView;
        }
    }
}
