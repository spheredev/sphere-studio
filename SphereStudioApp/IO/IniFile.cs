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
        /// <param name="autoSave">Whether to save the file automatically after a value is written.</param>
        public IniFile(string fileName, bool autoSave = true)
        {
            this.fileName = fileName;
            AutoSave = autoSave;

            sections = new Dictionary<string, Dictionary<string, string>>
            {
                { string.Empty, new Dictionary<string, string>() }
            };
            if (File.Exists(this.fileName))
            {
                using (StreamReader file = File.OpenText(this.fileName))
                {
                    var sectionRegex = new Regex(@"^\[(.*)\]$");
                    var itemRegex = new Regex(@"^(.*)=(.*)$");
                    var section = sections[string.Empty];
                    while (!file.EndOfStream)
                    {
                        var line = file.ReadLine().Trim();
                        var isSection = sectionRegex.Match(line);
                        var isItem = itemRegex.Match(line);
                        if (isSection.Success)
                        {
                            var name = isSection.Groups[1].Value;
                            if (!(sections.ContainsKey(name)))
                                sections.Add(name, new Dictionary<string, string>());
                            section = sections[name];
                        }
                        else if (isItem.Success)
                        {
                            var name = isItem.Groups[1].Value;
                            var value = isItem.Groups[2].Value;
                            section.Add(name, value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Releases any resources held by the <c>IniFile</c> object.
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
        /// Gets a string from a top-level section of the INI file.
        /// </summary>
        /// <param name="key">The name of the setting to read.</param>
        /// <param name="defValue">A default string to return if the key isn't found.</param>
        /// <returns>The value read from the INI file, or `defValue` if the key doesn't exist.</returns>
        public string GetValue(string key, string defValue)
        {
            return GetValue(null, key, defValue);
        }

        /// <summary>
        /// Reads a string from a specified section of the INI file.
        /// </summary>
        /// <param name="section">The <c>[section]</c> to read from, or <c>null</c> for the top level.</param>
        /// <param name="key">The name of the setting to read.</param>
        /// <param name="defValue">A default string to return if the key isn't found.</param>
        /// <returns>The value read from the INI file, or `defValue` if the key doesn't exist.</returns>
        public string GetValue(string section, string key, string defValue)
        {
            section = section ?? string.Empty;
            if (sections.ContainsKey(section) && sections[section].ContainsKey(key))
                return sections[section][key];
            else
                return defValue;
        }

        public void RemoveValue(string key)
        {
            RemoveValue(null, key);
        }

        public void RemoveValue(string section, string key)
        {
            section = section ?? string.Empty;
            if (sections.ContainsKey(section))
                sections[section].Remove(key);
            if (AutoSave)
                Save();
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
                    bool closingSection = false;
                    foreach (var name in sections)
                    {
                        if (closingSection)
                        {
                            file.WriteLine();
                            closingSection = false;
                        }
                        if (name != string.Empty)
                            file.WriteLine($"[{name}]");
                        var keys = from key in this.sections[name].Keys
                                   orderby key ascending
                                   select key;
                        foreach (var key in keys)
                        {
                            var value = this.sections[name][key];
                            file.WriteLine($"{key}={value}");
                        }
                        closingSection = true;
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
        /// Writes a string to the top-level section of the INI file.
        /// </summary>
        /// <param name="key">The name of the setting to write.</param>
        /// <param name="value">The value of the setting.</param>
        public void SetValue(string key, string value)
        {
            SetValue(string.Empty, key, value);
        }

        /// <summary>
        /// Writes a string to the specified section of the INI file.
        /// </summary>
        /// <param name="section">The <c>[section]</c> to write, or <c>null</c> for the top level.</param>
        /// <param name="key">The name of the setting to write.</param>
        /// <param name="value">The value of the setting.</param>
        public void SetValue(string section, string key, string value)
        {
            section = section ?? string.Empty;
            value = value ?? string.Empty;
            if (!sections.ContainsKey(section))
                sections.Add(section, new Dictionary<string, string>());
            sections[section][key] = value;
            if (AutoSave)
                Save();
        }
    }
}
