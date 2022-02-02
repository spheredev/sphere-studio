using SphereStudio.Base;
using SphereStudio.DocumentViews;

namespace SphereStudio.Editors
{
    class RasterImageEditor : IEditor<ImageView>
    {
        private PluginMain plugin;
        
        public RasterImageEditor(PluginMain plugin)
        {
            this.plugin = plugin;
        }
        
        public ImageView CreateEditView()
        {
            return new RasterImageView(plugin);
        }
    }
}
