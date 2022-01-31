using SphereStudio.Base;
using SphereStudio.DocumentViews;

namespace SphereStudio.Editors
{
    class ScriptEditor : IEditor<TextView>
    {
        private PluginMain plugin;

        public ScriptEditor(PluginMain plugin)
        {
            this.plugin = plugin;
        }

        public TextView CreateEditView()
        {
            return new ScriptTextView(plugin, true);
        }
    }
}
