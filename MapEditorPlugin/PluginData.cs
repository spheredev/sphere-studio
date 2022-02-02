using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using SphereStudio.Formats;

namespace SphereStudio
{
    internal static class PluginData
    {
        public static Entity CopiedEnt;
        public static List<string> Functions = new List<string>();

        static PluginData()
        {
            LoadFunctions();
        }
        
        public static void LoadFunctions()
        {
            var fileInfo = new FileInfo(Application.StartupPath + "/docs/functions.txt");
            if (fileInfo.Exists)
            {
                using (var reader = fileInfo.OpenText())
                {
                    while (!reader.EndOfStream)
                        Functions.Add(reader.ReadLine());
                }
            }
        }
    }
}
