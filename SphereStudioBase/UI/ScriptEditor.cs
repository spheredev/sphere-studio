using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.UI
{
    /// <summary>
    /// Defers text editing functionality to the active Text Editor plugin.
    /// </summary>
    public partial class TextEditor : UserControl
    {
        private TextView _view;

        /// <summary>
        /// Constructs a Script Editor control.
        /// </summary>
        public TextEditor()
        {
            InitializeComponent();

            // try to use a plugin for script editing
            var plugin = PluginManager.Get<IEditor<TextView>>(PluginManager.Core.Settings.TextEditor);
            _view = plugin != null ? plugin.CreateEditView() : null;
            if (_view != null)
            {
                _view.Dock = DockStyle.Fill;
                Controls.Add(_view);
                fallbackTextBox.Hide();
            }
        }

        /// <summary>
        /// Gets or sets the contents of the script.
        /// </summary>
        public override string Text
        {
            get
            {
                return _view != null ? _view.Text : fallbackTextBox.Text;
            }
            set
            {
                if (_view != null)
                    _view.Text = value;
                else
                    fallbackTextBox.Text = value;
            }
        }
    }
}
