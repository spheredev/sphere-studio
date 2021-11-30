using System.Drawing;

using SphereStudio.Base;
using SphereStudio.UI;

namespace SphereStudio.Plugins
{
    public class PluginMain : IPluginMain, INewFileOpener
    {
        public string Name => "Sphere Font Importer";
        public string Description => "TrueType to Sphere v1 RFN font converter";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        public string FileTypeName => "RFN Font";
        public string[] FileExtensions => new[] { "rfn" };
        public Bitmap FileIcon => Properties.Resources.style;

        public void Initialize(ISettings conf)
        {
            PluginManager.Register(this, this, Name);
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
        }

        public DocumentView New()
        {
            DocumentView view = new FontEditView();
            return view.NewDocument() ? view : null;
        }

        public DocumentView Open(string fileName)
        {
            DocumentView view = new FontEditView();
            view.Load(fileName);
            return view;
        }
    }
}
