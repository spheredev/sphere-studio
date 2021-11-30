using System.Drawing;

using SphereStudio.Base;

namespace SphereStudio.Plugins
{
    public class PluginMain : IPluginMain, INewFileOpener
    {
        public string Name => "Sphere Windowstyle Editor";
        public string Description => "Sphere v1 RWS format windowstyle editor";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        public string FileTypeName => "RWS Windowstyle";
        public string[] FileExtensions => new[] { "rws" };
        public Bitmap FileIcon => Properties.Resources.GridToolIcon;

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
            var view = new WindowstyleEditView();
            return view.NewDocument() ? view : null;
        }
        
        public DocumentView Open(string fileName)
        {
            WindowstyleEditView view = new WindowstyleEditView();
            view.Load(fileName);
            return view;
        }
   }
}
