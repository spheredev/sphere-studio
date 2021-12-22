﻿using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.DockPanes;

namespace SphereStudio.Core
{
    /// <summary>
    /// Exposes the build engine, which manages compilation and debugging.
    /// </summary>
    static class BuildEngine
    {
        private static BuildLogPane buildView;

        /// <summary>
        /// Initializes the build system.
        /// </summary>
        public static void Initialize()
        {
            buildView = new BuildLogPane();
            PluginManager.Register(null, buildView, "Build Log");
        }

        /// <summary>
        /// Checks whether a project's engine supports single-step debugging.
        /// </summary>
        /// <returns>true iff the engine supports single-step debugging.</returns>
        public static bool CanDebug(Project project)
        {
            return PluginManager.Get<IDebugStarter>(project.User.Engine) != null;
        }

        /// <summary>
        /// Checks whether a project's compiler supports packaging.
        /// </summary>
        /// <returns>true iff the compiler supports packaging.</returns>
        public static bool CanPackage(Project project)
        {
            return PluginManager.Get<IPackager>(project.Compiler) != null;
        }

        /// <summary>
        /// Checks whether a project can be run with the current configuration.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static bool CanTest(Project project)
        {
            return PluginManager.Get<IStarter>(project.User.Engine) != null
                && PluginManager.Get<ICompiler>(project.Compiler) != null;
        }

        /// <summary>
        /// Gets the SaveFileDialog filter for the current packaging compiler. Throws an
        /// exception if the compiler doesn't support packages.
        /// </summary>
        public static string GetSaveFileFilters(Project project)
        {
            if (!CanPackage(project))
                throw new NotSupportedException("The active compiler doesn't support packaging.");
            var packager = PluginManager.Get<IPackager>(project.Compiler);
            return packager.SaveFileFilters;
        }

        public static bool Prep(Project project)
        {
            buildView.Clear();
            PluginManager.Core.Docking.Show(buildView);
            PluginManager.Core.Docking.Activate(buildView);
            buildView.Print($"-------------------- Prep started: {project.Name} -------------------\n");
            var compiler = PluginManager.Get<ICompiler>(project.Compiler);
            if (compiler.Prep(project, buildView))
            {
                buildView.Print($"================ Successfully prepped: {project.Name} ===============");
                return true;
            }
            else
            {
                buildView.Print($"=================== Failed to prep: {project.Name} ==================");
                return false;
            }
        }

        /// <summary>
        /// Builds a game distribution from a project using the current compiler.
        /// </summary>
        /// <param name="project">The project to build.</param>
        /// <param name="debuggable">Whether the project should be built with debugging info.</param>
        /// <param name="rebuilding">Whether the project should be rebuilt from scratch.</param>
        /// <returns>The full path of the compiled distribution.</returns>
        public static async Task<string> Build(Project project, bool debuggable, bool rebuilding = false)
        {
            var compiler = PluginManager.Get<ICompiler>(project.Compiler);
            if (compiler == null)
            {
                MessageBox.Show(
                    $"Unable to build '{project.Name}'.\n\nA required plugin is missing.  You may not have the necessary compiler installed, or the plugin may be disabled.  Open Configuration Manager and check your plugins.\n\nCompiler required:\n{project.Compiler}",
                    "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            buildView.Clear();
            PluginManager.Core.Docking.Show(buildView);
            PluginManager.Core.Docking.Activate(buildView);

            buildView.Print($"------------------- Build started: {project.Name} -------------------\n");
            var outPath = rebuilding
                ? await compiler.Rebuild(project, debuggable, buildView)
                : await compiler.Build(project, debuggable, buildView);
            if (outPath != null)
            {
                buildView.Print($"================= Successfully built: {project.Name} ================");
                return outPath;
            }
            else
            {
                buildView.Print($"================== Failed to build: {project.Name} ==================");
                SystemSounds.Exclamation.Play();
                return null;
            }
        }

        /// <summary>
        /// Builds a game package from a project using the current compiler.
        /// </summary>
        /// <param name="project">The project to build.</param>
        /// <param name="fileName">The full path of the package to build. If the file exists, it will be overwritten.</param>
        /// <param name="debuggable">'true' if debugging info should be included in the package.</param>
        /// <returns>'true' if packaging succeeded, false if not.</returns>
        public static async Task<bool> Package(Project project, string fileName, bool debuggable)
        {
            if (!CanPackage(project))
                throw new NotSupportedException("The current compiler doesn't support packaging.");

            buildView.Clear();
            PluginManager.Core.Docking.Show(buildView);
            buildView.Print($"----------------- Packaging started: {project.Name} -----------------\n");
            var packager = PluginManager.Get<IPackager>(project.Compiler);
            bool isOK = await packager.Package(project, fileName, debuggable, buildView);
            if (isOK)
                buildView.Print($"=============== Successfully packaged: {project.Name} ===============");
            else
            {
                buildView.Print($"================= Failed to package: {project.Name} =================");
                SystemSounds.Exclamation.Play();
            }
            return isOK;
        }

        /// <summary>
        /// Starts single-step debugging a project using the current engine.
        /// </summary>
        /// <param name="project">The project to debug.</param>
        /// <param name="rebuilding">Whether the project should be completely rebuilt first.</param>
        /// <returns>An IDebugger used to manage the debugging session.</returns>
        public static async Task<IDebugger> Debug(Project project, bool rebuilding = false)
        {
            if (!CanDebug(project))
                throw new NotSupportedException("The current engine starter doesn't support debugging.");

            var starter = PluginManager.Get<IDebugStarter>(project.User.Engine);
            string outPath = await Build(project, true, rebuilding);
            try
            {
                if (outPath != null)
                    return starter.Debug(outPath, false, project);
                else
                    return null;
            }
            catch (Exception exc)
            {
                MessageBox.Show(
                    $"An error occurred while starting '{project.Name}'.\n\nexception: \"{exc.Message}\"\n{exc.StackTrace}",
                    "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Tests the game with the current engine.
        /// </summary>
        /// <param name="project">The project to test.</param>
        /// <param name="rebuilding">Whether the project should be completely rebuilt first.</param>
        public static async Task Test(Project project, bool rebuilding = false)
        {
            var starter = PluginManager.Get<IStarter>(project.User.Engine);
            if (starter != null)
            {
                string outPath = await Build(project, false, rebuilding);
                if (outPath != null)
                {
                    try
                    {
                        starter.Start(outPath, false);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(
                            $"An error occurred while starting '{project.Name}'.\n\nexception: \"{exc.Message}\"\n{exc.StackTrace}",
                            "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    $"Unable to test '{project.Name}'.\n\nEither no engines are installed, or all engine plugins are disabled.  Open Configuration Manager and check your plugins.",
                    "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
