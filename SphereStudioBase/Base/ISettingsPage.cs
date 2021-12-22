using System.Windows.Forms;

namespace SphereStudio.Base
{
    public enum SettingsPageType
    {
        TopLevel,
        Engine,
        Compiler,
    }
    
    /// <summary>
    /// Specifies the interface for a Preferences tab page.
    /// </summary>
    public interface ISettingsPage : IPlugin
    {
        /// <summary>
        /// Gets the physical UserControl for this settings page.
        /// </summary>
        Control Control { get; }

        /// <summary>
        /// Specifies which category the settings page is listed under.
        /// </summary>
        SettingsPageType Type { get; }

        /// <summary>
        /// Populates the settings page with the current settings.
        /// </summary>
        void Populate();

        /// <summary>
        /// Saves the settings the user entered on this project page.  Only called if <c>Verify</c>
        /// succeeds for all settings pages.
        /// </summary>
        void Save();

        /// <summary>
        /// Validates the settings the user entered on this settings page. If any settings are
        /// invalid, the method should display an error message and return <c>false</c>.
        /// </summary>
        /// <returns><c>true</c> if the user-provided settings are valid.</returns>
        bool Verify();
    }
}
