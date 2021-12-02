﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using SphereStudio.Base;

namespace SphereStudio.Compilers
{
    class ClassicCompiler : ICompiler
    {
        private readonly string[] fileFilters =
        {
            "*.rmp", "*.rss", "*.rts", "*.rfn", "*.rws",
            "*.js", "*.mjs", "*.glsl",
            "*.mp3", "*.ogg", "*.mid", "*.wav", "*.flac", "*.it", "*.s3m", "*.mod",
            "*.png", "*.jpg", "*.bmp", "*.pcx", "*.mng",
        };

        public bool Prep(IProject project, IConsole console)
        {
            console.Print("preparing Sphere Classic project... ");
            var scriptPath = Path.Combine(project.RootPath, "scripts", "main.js");
            Directory.CreateDirectory(Path.GetDirectoryName(scriptPath));
            var code = string.Join(Environment.NewLine,
                "// main.js automatically generated by Sphere Studio",
                "",
                "function game()",
                "{",
                "\t// your game code here",
                "\t",
                "}",
            "");
            File.WriteAllText(scriptPath, code);
            console.Print("OK.\n");

            console.Print("Success!\n");
            return true;
        }

        public async Task<bool> Build(IProject project, string outPath, bool debuggable, IConsole console)
        {
            console.Print($"building Sphere Classic project '{project.Name}'...\n");

            Directory.CreateDirectory(outPath);

            // if source and destination directories are the same, we can skip the copy step and
            // just generate game.sgm in-place.
            if (Path.GetFullPath(project.RootPath) != Path.GetFullPath(outPath))
            {
                console.Print("copying Sphere-compatible game files... ");
                int installCount = 0;
                await Task.Run(() =>
                {
                    DirectoryInfo inDir = new DirectoryInfo(project.RootPath);
                    DirectoryInfo outDir = new DirectoryInfo(outPath);
                    foreach (string filter in fileFilters)
                    {
                        var fileInfos = from info in inDir.GetFiles(filter, SearchOption.AllDirectories)
                                        where !info.FullName.StartsWith(outDir.FullName)  // ignore build directory
                                        select info;
                        foreach (FileInfo info in fileInfos)
                        {
                            string relFilePath = info.FullName.Substring(inDir.FullName.Length + 1);
                            string destFilePath = Path.Combine(outDir.FullName, relFilePath);

                            // copy file only if destination doesn't exist or is older than source
                            Directory.CreateDirectory(Path.GetDirectoryName(destFilePath));
                            if (!File.Exists(destFilePath) || File.GetLastWriteTimeUtc(destFilePath) < info.LastWriteTimeUtc)
                            {
                                if (installCount == 0)
                                    console.Print("\n");
                                console.Print($"      {relFilePath}\n");
                                File.Copy(info.FullName, destFilePath, true);
                                ++installCount;
                            }
                        }
                    }
                });
                if (installCount > 0)
                    console.Print(string.Format("      {0} file(s) copied.\n", installCount));
                else
                    console.Print("up to date.\n");
            }

            console.Print("writing game manifest 'game.sgm'... ");
            string sgmPath = Path.Combine(outPath, "game.sgm");
            var apiVersion = project.Settings.GetInteger("apiVersion", 1);
            var apiLevel = project.Settings.GetInteger("apiLevel", 1);
            var mainPath = project.Settings.GetString("mainScript", "scripts/main.js");
            var resolution = project.Settings.GetSize("resolution", new Size(320, 240));
            var saveId = project.Settings.GetString("saveID", string.Empty);
            using (StreamWriter sw = new StreamWriter(sgmPath))
            {
                sw.WriteLine($"version={apiVersion}");
                if (apiVersion >= 2)
                    sw.WriteLine($"api={apiLevel}");
                sw.WriteLine($"name={project.Name}");
                sw.WriteLine($"author={project.Author}");
                sw.WriteLine($"description={project.Summary}");
                if (apiVersion >= 2)
                {
                    sw.WriteLine($"resolution={resolution.Width}x{resolution.Height}");
                    sw.WriteLine($"main={mainPath}");
                    if (saveId != string.Empty)
                        sw.WriteLine($"saveID={saveId}");
                }
                else
                {
                    var scriptPath = mainPath.StartsWith("scripts/")
                        ? mainPath.Substring(8)
                        : $"../{mainPath}";
                    sw.WriteLine($"screen_width={resolution.Width}");
                    sw.WriteLine($"screen_height={resolution.Height}");
                    sw.WriteLine($"script={scriptPath}");
                }
            }
            console.Print("OK.\n");

            console.Print("Sphere Classic build succeeded.\n");
            return true;
        }
    }
}