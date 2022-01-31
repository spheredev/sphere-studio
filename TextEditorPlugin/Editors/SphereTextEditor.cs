using SphereStudio.Base;
using SphereStudio.DocumentViews;

namespace SphereStudio.Editors
{
    class SphereTextEditor : IEditor<TextView>
    {
        private PluginMain plugin;

        public SphereTextEditor(PluginMain plugin)
        {
            this.plugin = plugin;
        }

        public TextView CreateEditView()
        {
            return new SphereTextView(plugin, true);
        }
    }
}
