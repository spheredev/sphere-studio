using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

using SphereStudio.Base;
using SphereStudio.IO;

namespace SphereStudio.Core
{
    class IniSettings : ISettings
    {
        private IniFile iniFile;
        private string section;

        public IniSettings(IniFile iniFile, string section)
        {
            this.iniFile = iniFile;
            this.section = section;
        }

        public bool GetBoolean(string key, bool defValue)
        {
            return Convert.ToBoolean(GetString(key, defValue.ToString()));
        }

        public double GetFloat(string key, double defValue)
        {
            return Convert.ToDouble(GetString(key, defValue.ToString()));
        }

        public int GetInteger(string key, int defValue)
        {
            return Convert.ToInt32(GetString(key, defValue.ToString()));
        }

        public Size GetSize(string key, Size defValue)
        {
            var defString = $"{defValue.Width}x{defValue.Height}";
            var strValue = iniFile.GetValue(section, key, defString);
            var match = new Regex(@"(\d+)x(\d+)").Match(strValue);
            return match.Success
                ? new Size(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value))
                : defValue;
        }
        
        public string GetString(string key, string defValue)
        {
            return iniFile.GetValue(section, key, defValue ?? "");
        }
        
        public string[] GetStringArray(string key, string[] defValues)
        {
            string values = iniFile.GetValue(section, key, null);
            if (values == null && defValues != null)
                return defValues;
            return !string.IsNullOrEmpty(values)
                ? values.Split('|') : new string[0];
        }

        public bool Save()
        {
            return iniFile.Save();
        }

        public virtual bool SaveAs(string filepath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            return iniFile.SaveAs(filepath);
        }

        public void SetInteger(string key, int value)
        {
            SetString(key, value.ToString());
        }
        
        public void SetSize(string key, Size value)
        {
            SetValue(key, $"{value.Width}x{value.Height}");
        }

        public void SetString(string key, string value)
        {
            iniFile.SetValue(section, key, value);
        }

        public void SetValue(string key, object value)
        {
            value = value ?? string.Empty;
            var valueString = value is IEnumerable<string> valueList
                ? string.Join("|", valueList)
                : value.ToString();
            iniFile.SetValue(section, key, valueString);
        }
    }
}
