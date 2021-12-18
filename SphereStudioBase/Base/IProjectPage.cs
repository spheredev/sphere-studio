using System.Windows.Forms;

namespace SphereStudio.Base
{
    /// <summary>
    /// Specifies the interface for a Project Properties tab page.
    /// </summary>
    public interface IProjectPage : IPlugin
    {
        /// <summary>
        /// Gets the physical UserControl for this project page.
        /// </summary>
        Control Control { get; }

        /// <summary>
        /// Gets the registered name of the compiler plugin associated with this project page,
        /// or <c>null</c> for a compiler-agnostic page.
        /// </summary>
        string Compiler { get; }

        /// <summary>
        /// Populates the project page with the current settings.
        /// </summary>
        /// <param name="project">The project to use to populate the page.</param>
        void Populate(IProject project);

        /// <summary>
        /// Saves the settings the user entered on this project page.  Only called if <c>Verify</c>
        /// succeeds for all project pages.
        /// </summary>
        /// <param name="settings">The <c>ISettings</c> object to use to store the settings.</param>
        void Save(ISettings settings);

        /// <summary>
        /// Validates the settings the user entered on this project page. If any settings are
        /// invalid, the method should display an error message and return <c>false</c>.
        /// </summary>
        /// <returns><c>true</c> if the user-provided settings are valid.</returns>
        bool Verify();
    }
}
