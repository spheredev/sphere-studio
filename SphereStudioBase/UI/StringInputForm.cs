using System;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.UI
{
    /// <summary>
    /// Represents a dialog box that allows the user to enter a text string.
    /// </summary>
    public partial class StringInputForm : Form, IStyleAware
    {
        /// <summary>
        /// Initializes the <c>StringInputForm</c>.
        /// </summary>
        /// <param name="title">The text to put in the title bar of the dialog box.</param>
        /// <param name="labelText">A short description of the text-entry field.</param>
        public StringInputForm(string title, string labelText = null)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            if (title != null)
                Text = title;
            if (labelText != null)
                header.Text = labelText;
            NumbersOnly = false;
        }

        /// <summary>
        /// The string inputted into the form.
        /// </summary>
        public string Input
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
                textBox.Select();
            }
        }

        /// <summary>
        /// Specifies the maximum length of the string that can be entered.
        /// </summary>
        public int MaxLength
        {
            get => textBox.MaxLength;
            set => textBox.MaxLength = value;
        }

        /// <summary>
        /// Specifies if the text entry field should only accept numbers.
        /// </summary>
        public bool NumbersOnly { get; set; }

        /// <summary>
        /// Called automatically by the style manager to apply a UI style to this form.
        /// </summary>
        /// <param name="style">The UI style to apply.</param>
        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsHeading(header);
            style.AsHeading(footer);
            style.AsAccent(okButton);
            style.AsAccent(cancelButton);

            style.AsHeading(textHeading);
            style.AsAccent(textPanel);
            style.AsTextView(textBox);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NumbersOnly && !char.IsDigit(e.KeyChar) && e.KeyChar != 8;
        }
    }
}
