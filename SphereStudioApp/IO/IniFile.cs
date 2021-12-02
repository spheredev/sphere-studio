using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SphereStudio.IO
{
    /// <summary>
    /// Represents an INI format settings file.
    /// </summary>
    public class IniFile : IDisposable
    {
        private string fileName;
        private Dictionary<string, Dictionary<string, string>> sections;

        /// <summary>
        /// Constructs an IniFile object referencing the specified INI format file.
        /// </summary>
        /// <param name="fileName">The fully qualified INI file path.</param>
        /// <param name="autoSave">
        /// Whether to save the file automatically after a value is written. If this is false,
        /// Save() must be called to persist the changes.
        /// </param>
        public IniFile(string fileName, bool autoSave = true)
        {
            this.fileName = fileName;
            AutoSave = autoSave;

            sections = new Dictionary<string, Dictionary<string, string>>();
            sections.Add("", new Dictionary<string, string>());
            Dictionary<string, string> section = sections[""];
            if (File.Exists(this.fileName))
            {
                using (StreamReader file = File.OpenText(this.fileName))
                {
                    Regex sectionRegex = new Regex(@"^\[(.*)\]$");
                    Regex itemRegex = new Regex(@"^(.*)=(.*)$");
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine().Trim();
                        var isSection = sectionRegex.Match(line);
                        var isItem = itemRegex.Match(line);
                        if (isSection.Success)
                        {
                            string name = isSection.Groups[1].Value;
                            if (!(sections.ContainsKey(name)))
                                sections.Add(name, new Dictionary<string, string>());
                            section = sections[name];
                        }
                        else if (isItem.Success)
                        {
                            string name = isItem.Groups[1].Value;
                            string value = isItem.Groups[2].Value;
                            section.Add(name, value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Releases any resources held by the INI object.
        /// </summary>
        public void Dispose()
        {
            // nothing to dispose at present.
        }

        /// <summary>
        /// Gets or sets whether to save the INI file automatically when a
        /// value is changed.
        /// </summary>
        public bool AutoSave { get; set; }

        /// <summary>
        /// Reads a string from the INI file.
        /// </summary>
        /// <param name="section">The [section] to read from.</param>
        /// <param name="key">The name of the setting to read.</param>
        /// <param name="defValue">A default string to return if the key isn't found.</param>
        /// <returns>The value read from the INI file, or `defValue` if the key doesn't exist.</returns>
        public string Read(string section, string key, string defValue)
        {
            if (sections.ContainsKey(section) && sections[section].ContainsKey(key))
                return sections[section][key];
            else
                return defValue;
        }
        
        /// <summary>
        /// Saves the current values to the INI file.
        /// </summary>
        /// <returns>true if the save succeeded, otherwise false.</returns>
        public bool Save()
        {
            return SaveAs(fileName);
        }

        /// <summary>
        /// Saves the current values to a specified INI file.
        /// </summary>
        /// <param name="fileName">The fully qualified path of the file to save.</param>
        /// <returns>true if the save succeeded, otherwise false.</returns>
        public bool SaveAs(string fileName)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                using (StreamWriter file = new StreamWriter(fileName))
                {
                    var sections = from section in this.sections
                                   where section.Value.Count > 0
                                   select section.Key;
                    bool sectionClosing = false;
                    foreach (string name in sections)
                    {
                        if (sectionClosing)
                        {
                            file.WriteLine();
                            sectionClosing = false;
                        }
                        file.WriteLine(string.Format("[{0}]", name));
                        var itemNames = from itemName in this.sections[name].Keys
                                        orderby itemName ascending
                                        select itemName;
                        foreach (string itemName in itemNames)
                        {
                            file.WriteLine(string.Format("{0}={1}",
                                itemName,
                                this.sections[name][itemName]));
                        }
                        sectionClosing = true;
                    }
                }
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Writes a string to the INI file.
        /// </summary>
        /// <param name="section">The [section] to write.</param>
        /// <param name="key">The name of the setting to write.</param>
        /// <param name="value">The value of the setting.</param>
        public void Write(string section, string key, string value)
        {
            value = value ?? "";
            if (!sections.ContainsKey(section))
            {
                sections.Add(section, new Dictionary<string, string>());
            }
            sections[section][key] = value;
            if (AutoSave)
            {
                Save();
            }
        }
    }
}
