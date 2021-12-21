using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

using SphereStudio.Base;

namespace SphereStudio.Compilers
{
    class CellCompiler : IPackager
    {
        private PluginMain main;

        public CellCompiler(PluginMain main)
        {
            this.main = main;
        }

        public string SaveFileFilters
        {
            get { return "Sphere Game Package|*.spk"; }
        }

        public bool Prep(IProject project, IConsole con)
        {
            con.Print("Installing project template... ");
            CopyDirectory(Path.Combine(main.Conf.EnginePath, "system", "template"), project.RootPath);
            con.Print("OK.\n");

            var cellTemplatePath = Path.Combine(project.RootPath, "Cellscript.js.tmpl");
            var scriptTemplatePath = Path.Combine(project.RootPath, "scripts\\main.js.tmpl");
            try
            {
                con.Print("Generating Cellscript... ");
                var cellscriptPath = Path.Combine(project.RootPath, "Cellscript.js");
                var mainScriptPath = Path.Combine(project.RootPath, "scripts\\main.js");
                var template = File.ReadAllText(cellTemplatePath);
                var resolution = project.Settings.GetSize("resolution", new Size(320, 240));
                var script = string.Format(template,
                    JSifyString(project.Name, '"'), JSifyString(project.Author, '"'),
                    JSifyString(project.Summary, '"'),
                    $"{resolution.Width}x{resolution.Height}");
                File.WriteAllText(cellscriptPath, script);
                File.Delete(cellTemplatePath);
                con.Print("OK.\n");
                con.Print("Generating main module... ");
                template = File.ReadAllText(scriptTemplatePath);
                script = string.Format(template,
                    JSifyString(project.Name, '"'), JSifyString(project.Author, '"'),
                    JSifyString(project.Summary, '"'),
                    $"{resolution.Width}x{resolution.Height}");
                File.WriteAllText(mainScriptPath, script);
                File.Delete(scriptTemplatePath);
                con.Print("OK.\n");
            }
            catch (Exception exc)
            {
                con.Print(string.Format("\n[error] {0}\n", exc.Message));
                return false;
            }

            con.Print("Success!\n");
            return true;
        }

        public async Task<string> Build(IProject project, bool debuggable, IConsole console)
        {
            var inPath = project.RootPath.Replace(Path.DirectorySeparatorChar, '/');
            var outPath = $"{inPath}/dist";
            var options = debuggable ? "--debug" : "--release";
            var succeeded = await RunCell(
                $@"--in-dir ""{inPath}"" --out-dir ""{outPath}"" {options}",
                console);
            return succeeded ? outPath : null;
        }

        public async Task<bool> Package(IProject project, string fileName, bool debuggable, IConsole console)
        {
            var inPath = project.RootPath.Replace(Path.DirectorySeparatorChar, '/');
            var outPath = Path.Combine(inPath, "dist");
            var packagePath = fileName.Replace(Path.DirectorySeparatorChar, '/');
            var options = main.Conf.MakeDebugPackages ? "--debug" : "--release";
            return await RunCell(
                $@"pack --in-dir ""{inPath}"" --out-dir ""{outPath}"" {options} ""{packagePath}""",
                console);
        }

        private void CopyDirectory(string sourcePath, string destPath)
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
                CopyDirectory(dirInfo.FullName, destFileName);
            }
        }

        private string JSifyString(string str, char quoteChar)
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

        private async Task<bool> RunCell(string options, IConsole console)
        {
            string cellPath = Path.Combine(main.Conf.EnginePath, "cell.exe");
            if (!File.Exists(cellPath))
            {
                console.Print("ERROR: unable to build - the Cell compiler was not found.\n");
                console.Print("       please check neoSphere settings in Preferences.\n");
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
