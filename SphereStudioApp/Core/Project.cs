using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using SphereStudio.Base;
using SphereStudio.IO;

namespace SphereStudio.Core
{
    /// <summary>
    /// Represents a loaded Sphere Studio project.
    /// </summary>
    class Project : IProject
    {
        private Dictionary<string, HashSet<int>> breakpoints = new Dictionary<string, HashSet<int>>();
        private IniSettings settings;

        private Project(string fileName)
        {
            fileName = Path.GetFullPath(fileName);
            var userFilePath = Path.Combine(Path.GetDirectoryName(fileName), "sphereStudio.usr");
            settings = new IniSettings(new IniFile(fileName, false), ".ssproj");
            FileName = fileName;
            UserSettings = new UserSettings(userFilePath);
        }

        /// <summary>
        /// Creates a new, empty Sphere Studio project.
        /// </summary>
        /// <param name="rootPath">Path of the directory where the project will reside. Must be empty.</param>
        /// <param name="name">The name of the project to create.</param>
        /// <returns>A <c>Project</c> object that represents the newly created project.</returns>
        public static Project Create(string rootPath, string name)
        {
            var dirInfo = new DirectoryInfo(rootPath);
            if (dirInfo.Exists && dirInfo.GetFileSystemInfos().Length > 0)
                throw new ArgumentException("Root directory for a new project must be empty.");
            dirInfo.Create();
            var project = new Project(Path.Combine(dirInfo.FullName, makeFileName(name)))
            {
                Name = name
            };
            return project;
        }

        /// <summary>
        /// Loads a Sphere game manifest (<c>.sgm</c>) file as a Sphere Studio project.
        /// </summary>
        /// <param name="fileName">The full path of the <c>.sgm</c> file to load.</param>
        /// <returns>A <c>Project</c> object that represents the loaded project.</returns>
        public static Project FromSgm(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException();

            var project = new Project(fileName)
            {
                GameOnly = true,
                Name = "Untitled",
                Author = "Author Unknown",
                Summary = "",
            };

            var sgmText = File.ReadAllLines(fileName);
            var apiVersion = 0;
            var apiLevel = 1;
            var resolution = new Size(320, 240);
            var screenWidth = 320;
            var screenHeight = 240;
            var scriptPath = string.Empty;
            var saveId = string.Empty;
            foreach (var line in sgmText)
            {
                try
                {
                    var match = new Regex("(.+)=(.*)").Match(line);
                    if (match.Success)
                    {
                        string key = match.Groups[1].Value;
                        string value = match.Groups[2].Value;
                        switch (key)
                        {
                            case "version":
                                apiVersion = int.Parse(value);
                                break;
                            case "api":
                                if (apiVersion == 0)
                                    apiVersion = 2;
                                apiLevel = int.Parse(value);
                                break;
                            case "name": project.Name = value; break;
                            case "author": project.Author = value; break;
                            case "description": project.Summary = value; break;
                            case "script": scriptPath = value; break;
                            case "saveID": saveId = value; break;
                            case "main":
                                if (apiVersion == 0)
                                    apiVersion = 2;
                                scriptPath = value;
                                break;
                            case "resolution":
                                Match resoMatch = new Regex(@"(\d+)x(\d+)").Match(value);
                                if (resoMatch.Success)
                                    resolution = new Size(int.Parse(resoMatch.Groups[1].Value), int.Parse(resoMatch.Groups[2].Value));
                                break;
                            case "screen_width":
                                screenWidth = int.Parse(value);
                                break;
                            case "screen_height":
                                screenHeight = int.Parse(value);
                                break;
                        }
                    }
                }
                catch
                {
                    // ignore any parsing errors. if an error occurs parsing the manifest,
                    // we'll just use the default values. this ensures it is always possible
                    // to upgrade a Sphere 1.x project even if the game.sgm is damaged.
                }
            }

            apiVersion = Math.Max(apiVersion, 1);
            if (apiVersion < 2 && scriptPath != string.Empty)
                scriptPath = $"scripts/{scriptPath}";
            project.Compiler = Defaults.Compiler;
            project.Settings.SetInteger("apiVersion", apiVersion);
            project.Settings.SetInteger("apiLevel", apiLevel);
            project.Settings.SetSize("resolution", apiVersion >= 2
                ? resolution
                : new Size(screenWidth, screenHeight));
            project.Settings.SetString("mainScript", scriptPath);
            project.Settings.SetString("saveID", saveId);

            var jsonPath = Path.Combine(project.RootPath, "game.json");
            if (File.Exists(jsonPath))
                project.settings.SetValue("manageGameJson", true);
            return project;
        }

        /// <summary>
        /// Loads an existing project.
        /// </summary>
        /// <param name="fileName">The full path of a Sphere project file, either <c>.ssproj</c> or <c>.sgm</c>.</param>
        /// <returns>A <c>Project</c> object that represents the loaded project.</returns>
        public static Project Open(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException();

            return Path.GetFileName(fileName).Equals("game.sgm", StringComparison.OrdinalIgnoreCase)
                ? Project.FromSgm(fileName)
                : new Project(fileName);
        }

        /// <summary>
        /// Gets the full path of the project's <c>.ssproj</c> file.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the full path of the project's root directory.
        /// </summary>
        public string RootPath => Path.GetDirectoryName(FileName);

        /// <summary>
        /// Gets the <c>ISettings</c> object used to store settings for this project.
        /// </summary>
        public ISettings Settings => settings;

        /// <summary>
        /// Provides access to the project's local user settings.
        /// </summary>
        public UserSettings UserSettings { get; private set; }

        /// <summary>
        /// Gets or sets the registered name of the compiler to use when building
        /// this project.
        /// </summary>
        public string Compiler
        {
            get => !GameOnly ? settings.GetString("compiler", Defaults.Compiler) : Defaults.Compiler;
            set => settings.SetValue("compiler", value);
        }

        /// <summary>
        /// Gets or sets the project name (usually a title).
        /// </summary>
        public string Name
        {
            get => settings.GetString("name", "Untitled");
            set => settings.SetValue("name", value);
        }

        /// <summary>
        /// Gets or sets the name of the project author.
        /// </summary>
        public string Author
        {
            get => settings.GetString("author", "");
            set => settings.SetValue("author", value);
        }

        /// <summary>
        /// Gets or sets a short description of the game.
        /// </summary>
        public string Summary
        {
            get => settings.GetString("description", "");
            set => settings.SetValue("description", value);
        }

        /// <summary>
        /// Gets or sets the game's vertical resolution.
        /// </summary>
        public int ScreenWidth
        {
            get => settings.GetInteger("screenWidth", 320);
            set => settings.SetValue("screenWidth", value);
        }

        /// <summary>
        /// Gets or sets the game's horizontal resolution.
        /// </summary>
        public int ScreenHeight
        {
            get => settings.GetInteger("screenHeight", 240);
            set => settings.SetValue("screenHeight", value);
        }

        /// <summary>
        /// Gets whether the project is game-only (e.g. synthesized from an SGM file).
        /// </summary>
        public bool GameOnly
        {
            get => settings.GetBoolean("backCompatible", false);
            set => settings.SetValue("backCompatible", value);
        }

        public IReadOnlyDictionary<string, int[]> GetAllBreakpoints()
        {
            var retval = new Dictionary<string, int[]>();
            foreach (var key in breakpoints.Keys)
            {
                retval.Add(key, breakpoints[key].ToArray());
            }
            return retval;
        }

        public int[] GetBreakpoints(string scriptPath)
        {
            if (scriptPath == null)
                return new int[0];
            var hash = scriptPath.GetHashCode();
            if (breakpoints.ContainsKey(scriptPath))
            {
                return breakpoints[scriptPath].ToArray();
            }
            else
            {
                var lines = new int[0];
                try
                {
                    lines = Array.ConvertAll(
                        UserSettings.GetString($"breakpointsSet:{hash:X8}", "").Split(','),
                        int.Parse);
                }
                catch (Exception)
                {
                    // *MUNCH*
                }
                breakpoints.Add(scriptPath, new HashSet<int>(lines));
                return lines;
            }
        }

        /// <summary>
        /// Saves any changes made to the project.
        /// </summary>
        public void Save()
        {
            UserSettings.SaveAs(Path.Combine(RootPath, "sphereStudio.usr"));
            if (GameOnly)
            {
                // Sphere 1.x-compatible project mode (treat .sgm as project file)
                string fileName = Path.Combine(Path.GetDirectoryName(FileName), "game.sgm");
                using (var writer = new StreamWriter(fileName, false, new UTF8Encoding(false)))
                {
                    var apiVersion = settings.GetInteger("apiVersion", 1);
                    var apiLevel = settings.GetInteger("apiLevel", 1);
                    var resolution = settings.GetSize("resolution", new Size(320, 240));
                    var mainPath = settings.GetString("mainScript", "scripts/main.js");
                    var saveId = settings.GetString("saveID", string.Empty);
                    writer.WriteLine($"version={apiVersion}");
                    if (apiVersion >= 2)
                        writer.WriteLine($"api={apiLevel}");
                    writer.WriteLine($"name={Name}");
                    writer.WriteLine($"author={Author}");
                    writer.WriteLine($"description={Summary}");
                    writer.WriteLine($"saveID={saveId}");
                    if (apiVersion >= 2)
                    {
                        writer.WriteLine($"resolution={resolution.Width}x{resolution.Height}");
                        writer.WriteLine($"main={mainPath}");
                    }
                    else
                    {
                        var scriptPath = mainPath.StartsWith("scripts/")
                            ? mainPath.Substring(8)
                            : $"../{mainPath}";
                        writer.WriteLine($"screen_width={resolution.Width}");
                        writer.WriteLine($"screen_height={resolution.Height}");
                        writer.WriteLine($"script={scriptPath}");
                    }
                }
            }
            else
            {
                settings.SaveAs(FileName);
            }
        }

        public void SetBreakpoints(string scriptPath, int[] lineNumbers)
        {
            breakpoints[scriptPath] = new HashSet<int>(lineNumbers);
            foreach (var k in breakpoints.Keys)
            {
                UserSettings.SetValue($"breakpointsSet:{k.GetHashCode():X8}",
                    string.Join(",", breakpoints[k]));
            }
        }

        /// <summary>
        /// Upgrades a Sphere game to a full Sphere Studio project.
        /// </summary>
        public void Upgrade()
        {
            var basePath = Path.GetDirectoryName(FileName);
            FileName = Path.Combine(basePath, makeFileName(Name));
            GameOnly = false;
            Compiler = Defaults.Compiler;
            Save();
        }

        private static string makeFileName(string name)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var pattern = $@"([{invalidChars}]*\.+$)|([{invalidChars}]+)";
            return $"{Regex.Replace(name, pattern, "_")}.ssproj";
        }
    }
}
