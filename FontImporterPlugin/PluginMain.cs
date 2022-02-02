using System.Drawing;

using SphereStudio.Base;
using SphereStudio.FileOpeners;
using SphereStudio.UI;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Sphere Font Importer";
        public string Description => "Convert TrueType fonts to the Sphere RFN format";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        public void Initialize(ISettings conf)
        {
            PluginManager.Register(this, new SphereFontOpener(), Name);
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
        }
    }
}
