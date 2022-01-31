using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using SphereStudio.Base;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SphereStudio.Compilers
{
    class SphereCompiler : ICompiler
    {
        public async Task<string> Build(IProject project, bool debuggable, IConsole console)
        {
            console.Print($"Sphere Studio {Versioning.Version} Sphere manifest compiler\n");
            console.Print($"built-in IDE tooling for Sphere v1 and v2 games\n");
            console.Print($"(c) {Versioning.Copyright}\n");
            console.Print("\n");
            console.Print("writing new Sphere game manifest... ");
            var sgmPath = Path.Combine(project.RootPath, "game.sgm");
            var jsonPath = Path.Combine(project.RootPath, "game.json");
            var apiVersion = project.Settings.GetInteger("apiVersion", 1);
            var apiLevel = project.Settings.GetInteger("apiLevel", 1);
            var mainPath = project.Settings.GetString("mainScript", string.Empty);
            var resolution = project.Settings.GetSize("resolution", new Size(320, 240));
            var saveId = project.Settings.GetString("saveID", string.Empty);
            var managingJson = project.Settings.GetBoolean("manageGameJson", false);
            using (var sw = new StreamWriter(sgmPath))
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
            
            if (managingJson)
            {
                console.Print("updating values in 'game.json'... ");
                var jsonData = File.Exists(jsonPath)
                    ? JsonConvert.DeserializeObject<JObject>(File.ReadAllText(jsonPath, Encoding.UTF8))
                    : new JObject();
                jsonData["version"] = apiVersion;
                if (apiVersion >= 2)
                    jsonData["apiLevel"] = apiLevel;
                else
                    jsonData.Remove("apiLevel");
                jsonData["name"] = project.Name;
                jsonData["author"] = project.Author;
                jsonData["description"] = project.Summary;
                jsonData["resolution"] = $"{resolution.Width}x{resolution.Height}";
                jsonData["main"] = mainPath;
                if (saveId != string.Empty)
                    jsonData["saveID"] = saveId;
                else
                    jsonData.Remove("saveID");
                using (var sw = new StreamWriter(jsonPath))
                {
                    sw.Write(JsonConvert.SerializeObject(jsonData, Formatting.Indented));
                }
                console.Print("OK.\n");
            }

            console.Print("Sphere Classic build succeeded.\n");
            return project.RootPath;
        }

        public bool Prep(IProject project, IConsole console)
        {
            console.Print("preparing Sphere Classic project... ");
            var scriptPath = Path.Combine(project.RootPath, "scripts", "main.js");
            Directory.CreateDirectory(Path.GetDirectoryName(scriptPath));
            var code = string.Join(Environment.NewLine,
                $"// {project.Name} by {project.Author}",
                $"// {project.Summary}",
                "",
                "export default",
                "function main()",
                "{",
                "\t// your game code here",
                "\t",
                "}",
            "");
            File.WriteAllText(scriptPath, code);
            console.Print("OK.\n");

            project.Settings.SetInteger("apiVersion", 2);
            project.Settings.SetInteger("apiLevel", 1);
            project.Settings.SetSize("resolution", new Size(320, 240));
            project.Settings.SetString("mainScript", "scripts/main.js");
            project.Settings.SetValue("manageGameJson", true);

            console.Print("Success!\n");
            return true;
        }

        public async Task<string> Rebuild(IProject project, bool debuggable, IConsole console)
        {
            return await Build(project, debuggable, console);
        }
    }
}
