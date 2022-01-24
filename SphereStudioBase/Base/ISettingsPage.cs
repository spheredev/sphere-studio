using System.Windows.Forms;

namespace SphereStudio.Base
{
    /// <summary>
    /// Specifies which category a settings page belongs to.
    /// </summary>
    public enum SettingsCategory
    {
        /// <summary>
        /// A top-level settings page, which is uncategorized.
        /// </summary>
        TopLevel,

        /// <summary>
        /// A settings page pertaining to engine support.
        /// </summary>
        Engine,

        /// <summary>
        /// A settings page for a compiler plugin.
        /// </summary>
        Compiler,
    }
    
    /// <summary>
    /// Specifies the interface for a Preferences tab page.
    /// </summary>
    public interface ISettingsPage : IPlugin
    {
        /// <summary>
        /// Specifies which category the settings page is listed under.
        /// </summary>
        SettingsCategory Category { get; }

        /// <summary>
        /// Gets the physical <c>UserControl</c> for this settings page.
        /// </summary>
        Control Control { get; }

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
