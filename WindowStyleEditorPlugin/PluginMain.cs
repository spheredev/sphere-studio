using SphereStudio.Base;
using SphereStudio.FileOpeners;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Sphere Window Style Editor";
        public string Description => "Sphere RWS format window style editor";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        public void Initialize(ISettings settings)
        {
            PluginManager.Register(this, new WindowStyleOpener(), Name);
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
        }
   }
}
