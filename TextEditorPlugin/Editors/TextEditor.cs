using SphereStudio.Base;
using SphereStudio.DocumentViews;

namespace SphereStudio.Editors
{
    class TextEditor : IEditor<TextView>
    {
        private PluginMain plugin;

        public TextEditor(PluginMain plugin)
        {
            this.plugin = plugin;
        }

        public TextView CreateEditView()
        {
            return new TextDocumentView(plugin, true);
        }
    }
}
