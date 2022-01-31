using System.Drawing;

using SphereStudio.Base;

namespace SphereStudio.StyleProviders
{
    class DefaultStyleProvider : IStyleProvider
    {
        public DefaultStyleProvider()
        {
            var darkTheme = new UIStyle("Dark Mode") {
                AccentColor = Color.FromArgb(32, 32, 48),
                BackColor = Color.FromArgb(24, 24, 32),
                FixedFont = new Font("Consolas", 10.0f),
                Font = new Font("Segoe UI", 9.0f),
                HighlightColor = Color.DarkSlateBlue,
                LabelColor = Color.FromArgb(32, 32, 32),
                TextColor = Color.LightGray,
                ToolColor = Color.FromArgb(48, 48, 48),
            };

            var blueTheme = new UIStyle("Blue") {
                AccentColor = Color.FromArgb(208, 208, 224),
                BackColor = Color.White,
                FixedFont = new Font("Consolas", 10.0f),
                Font = new Font("Segoe UI", 9.0f),
                HighlightColor = Color.LightSkyBlue,
                LabelColor = Color.LightSteelBlue,
                TextColor = Color.Black,
                ToolColor = Color.FromArgb(192, 192, 208),
            };

            Styles = new[] { blueTheme, darkTheme };
        }

        public UIStyle[] Styles { get; private set; }
    }
}
