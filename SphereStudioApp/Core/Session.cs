using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

using SphereStudio.Base;
using SphereStudio.IO;

namespace SphereStudio.Core
{
    static class Session
    {
        static Session()
        {
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sphere Studio");
            var iniPath = Path.Combine(appDataPath, "userSettings.ini");

            MainIniFile = new IniFile(iniPath);
            Settings = new CoreSettings(Session.MainIniFile);

            // load plugin modules (user-installed plugins first)
            Plugins = new Dictionary<string, PluginShim>();
            var searchPaths = new []
            {
                Path.Combine(appDataPath, "Plugins"),
                Path.Combine(Application.StartupPath, "Plugins"),
            };
            foreach (var path in searchPaths)
            {
                var dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    continue;
                foreach (var fileInfo in dirInfo.GetFiles("*.dll")) {
                    var handle = Path.GetFileNameWithoutExtension(fileInfo.Name);
                    if (!Plugins.Keys.Contains(handle))  // only the first by that name is used
                        try {
                            Plugins[handle] = new PluginShim(fileInfo.FullName, handle);
                        }
                        catch (Exception error) {
                            MessageBox.Show(
                                $"Sphere Studio was unable to load the plugin file {fileInfo.FullName}.\n\nThe error encountered was:\n{error.Message}",
                                "Couldn't Load Plugin Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                }
            }
        }

        /// <summary>
        /// Provides access to the INI file used for global settings (<c>coreSettings.ini</c>).
        /// </summary>
        public static IniFile MainIniFile { get; private set; }

        /// <summary>
        /// Gets or sets the currently loaded project.
        /// </summary>
        public static Project Project { get; set; }

        /// <summary>
        /// Gets the list of loaded plugins.
        /// </summary>
        public static Dictionary<string, PluginShim> Plugins { get; private set; }

        /// <summary>
        /// Provides access to the global IDE settings.
        /// </summary>
        public static CoreSettings Settings { get; private set; }

        /// <summary>
        /// Gets the registered name of the file opener plugin handling a specified filename.
        /// </summary>
        /// <param name="fileName">The filename to find a file opener for.</param>
        /// <returns>The registered name of the correct file opener, or <c>null</c> if none was found.</returns>
        public static string GetFileOpenerName(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            if (fileExtension.StartsWith("."))  // remove dot from extension
                fileExtension = fileExtension.Substring(1);
            var names = from name in PluginManager.GetNames<IFileOpener>()
                        let plugin = PluginManager.Get<IFileOpener>(name)
                        where plugin.FileExtensions.Any(it => it.Equals(fileExtension, StringComparison.OrdinalIgnoreCase))
                        select name;
            return names.FirstOrDefault();
        }
    }
}
