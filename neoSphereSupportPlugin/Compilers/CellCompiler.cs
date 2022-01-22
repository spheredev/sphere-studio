using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.Compilers
{
    class CellCompiler : IPackager
    {
        private PluginSettings settings;

        public CellCompiler(PluginSettings settings)
        {
            this.settings = settings;
        }

        public string SaveFileFilters => "Sphere Game Package|*.spk";

        public async Task<string> Build(IProject project, bool debuggable, IConsole console)
        {
            var inPath = project.RootPath.Replace(Path.DirectorySeparatorChar, '/');
            var outPath = $"{inPath}/dist";
            var options = debuggable ? "--debug" : "--release";
            var succeeded = await runCell(
                $@"--in-dir ""{inPath}"" --out-dir ""{outPath}"" {options}",
                console);
            return succeeded ? outPath : null;
        }

        public async Task<bool> Package(IProject project, string fileName, bool debuggable, IConsole console)
        {
            var inPath = project.RootPath.Replace(Path.DirectorySeparatorChar, '/');
            var outPath = Path.Combine(inPath, "dist");
            var packagePath = fileName.Replace(Path.DirectorySeparatorChar, '/');
            var options = settings.MakeDebugPackages ? "--debug" : "--release";
            return await runCell(
                $@"pack --in-dir ""{inPath}"" --out-dir ""{outPath}"" {options} ""{packagePath}""",
                console);
        }

        public bool Prep(IProject project, IConsole console)
        {
            var templatePath = Path.Combine(settings.EnginePath, "system", "template");
            if (!Directory.Exists(templatePath))
            {
                MessageBox.Show(
                    "The Cell project template couldn't be found. Check that you have neoSphere installed and that the neoSphere path is set correctly in Preferences.",
                    "Unable to Prep Cell Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            console.Print("Installing project template... ");
            copyDirectory(templatePath, project.RootPath);
            console.Print("OK.\n");

            var cellTemplatePath = Path.Combine(project.RootPath, "Cellscript.js.tmpl");
            var scriptTemplatePath = Path.Combine(project.RootPath, "scripts\\main.js.tmpl");
            try
            {
                console.Print("Generating Cellscript... ");
                var cellscriptPath = Path.Combine(project.RootPath, "Cellscript.js");
                var mainScriptPath = Path.Combine(project.RootPath, "scripts\\main.js");
                var template = File.ReadAllText(cellTemplatePath);
                var resolution = project.Settings.GetSize("resolution", new Size(320, 240));
                var script = string.Format(template,
                    jsifyString(project.Name, '"'),
                    jsifyString(project.Author, '"'),
                    jsifyString(project.Summary, '"'),
                    $"{resolution.Width}x{resolution.Height}");
                File.WriteAllText(cellscriptPath, script);
                File.Delete(cellTemplatePath);
                console.Print("OK.\n");
                console.Print("Generating main module... ");
                template = File.ReadAllText(scriptTemplatePath);
                script = string.Format(template,
                    jsifyString(project.Name, '"'),
                    jsifyString(project.Author, '"'),
                    jsifyString(project.Summary, '"'),
                    $"{resolution.Width}x{resolution.Height}");
                File.WriteAllText(mainScriptPath, script);
                File.Delete(scriptTemplatePath);
                console.Print("OK.\n");
            }
            catch (Exception error)
            {
                console.Print($"\n[error] {error.Message}\n");
                return false;
            }

            console.Print("Success!\n");
            return true;
        }

        public async Task<string> Rebuild(IProject project, bool debuggable, IConsole console)
        {
            var inPath = project.RootPath.Replace(Path.DirectorySeparatorChar, '/');
            var outPath = $"{inPath}/dist";
            var options = debuggable ? "--debug" : "--release";
            var succeeded = await runCell(
                $@"--rebuild --in-dir ""{inPath}"" --out-dir ""{outPath}"" {options}",
                console);
            return succeeded ? outPath : null;
        }

        private void copyDirectory(string sourcePath, string destPath)
        {
            var source = new DirectoryInfo(sourcePath);
            var target = new DirectoryInfo(destPath);
            target.Create();
            foreach (var fileInfo in source.GetFiles())
            {
                var destFileName = Path.Combine(target.FullName, fileInfo.Name);
                File.Copy(fileInfo.FullName, destFileName, true);
            }
            foreach (var dirInfo in source.GetDirectories())
            {
                var destFileName = Path.Combine(target.FullName, dirInfo.Name);
                copyDirectory(dirInfo.FullName, destFileName);
            }
        }

        private string jsifyString(string str, char quoteChar)
        {
            str = str
                .Replace("\n", @"\n").Replace("\r", @"\r")
                .Replace(@"\", @"\\");
            if (quoteChar == '"')
                return str.Replace(@"""", @"\""");
            else if (quoteChar == '\'')
                return str.Replace("'", @"\'");
            else
                return str;
        }

        private async Task<bool> runCell(string options, IConsole console)
        {
            string cellPath = Path.Combine(settings.EnginePath, "cell.exe");
            if (!File.Exists(cellPath))
            {
                console.Print("ERROR: unable to build the project as the Cell compiler was not found.\n");
                console.Print("       check that you have neoSphere installed and that the neoSphere path is\n");
                console.Print("       set correctly in Preferences.\n");
                return false;
            }

            ProcessStartInfo psi = new ProcessStartInfo(cellPath, options);
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            Process proc = Process.Start(psi);
            var lineCount = 0;
            proc.OutputDataReceived += (sender, e) =>
            {
                var head = lineCount > 0 ? "\r\n" : "";
                console.Print(head + (e.Data ?? ""));
                ++lineCount;
            };
            proc.BeginOutputReadLine();
            await proc.WaitForExitAsync();
            return proc.ExitCode == 0;
        }
    }
}
