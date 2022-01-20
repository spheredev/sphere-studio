using System.Linq;

using SphereStudio.Base;
using SphereStudio.IO;

namespace SphereStudio.Core
{
    class UserSettings : IniSettings
    {
        public UserSettings(string filePath) :
            base(new IniFile(filePath, false), "sphereStudio.usr")
        {
        }

        /// <summary>
        /// Gets or sets the full path of the last document the user viewed.
        /// </summary>
        public string ActiveDocument
        {
            get => GetString("currentDocument", string.Empty);
            set => SetValue("currentDocument", value);
        }

        /// <summary>
        /// Gets or sets the full paths of all the documents the user has open.
        /// </summary>
        public string[] Documents
        {
            get => GetStringArray("openDocuments", new string[0]);
            set => SetValue("openDocuments", value);
        }

        /// <summary>
        /// Gets or sets the registered name of the engine plugin to use when testing or debugging
        /// this project.
        /// </summary>
        public string Engine
        {
            get
            {
                var engineNames = PluginManager.GetNames<IStarter>();
                var value = GetString("engine", Session.Settings.Engine);
                return engineNames.Contains(value) ? value : Session.Settings.Engine;
            }
            set
            {
                SetValue("engine", value);
            }
        }

        /// <summary>
        /// Gets or sets if the Start Page is hidden for this user.
        /// </summary>
        public bool StartPageHidden
        {
            get => GetBoolean("hideStartPage", false);
            set => SetValue("hideStartPage", value);
        }

    }
}
