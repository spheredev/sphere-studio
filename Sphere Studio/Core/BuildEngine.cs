﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sphere.Plugins;
using Sphere.Plugins.Interfaces;
using SphereStudio.DockPanes;

namespace SphereStudio
{
    /// <summary>
    /// Exposes the build engine, which manages compilation and debugging.
    /// </summary>
    static class BuildEngine
    {
        private static BuildConsole _buildView;

        /// <summary>
        /// Initializes the build system.
        /// </summary>
        public static void Initialize()
        {
            _buildView = new BuildConsole();
            PluginManager.Register(null, _buildView, "Build Log");
        }

        /// <summary>
        /// Checks whether a project's engine supports single-step debugging.
        /// </summary>
        /// <returns>true iff the engine supports single-step debugging.</returns>
        public static bool CanDebug(Project project)
        {
            return PluginManager.Get<IDebugStarter>(project.Engine) != null;
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
            return PluginManager.Get<IStarter>(project.Engine) != null
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
            _buildView.Clear();
            PluginManager.Core.Docking.Show(_buildView);
            _buildView.Print(string.Format("-------------------- Prep started: {0} -------------------\n", project.Name));
            ICompiler compiler = PluginManager.Get<ICompiler>(project.Compiler);
            if (compiler.Prep(project, _buildView))
            {
                _buildView.Print(string.Format("================ Successfully prepped: {0} ===============\n", project.Name));
                return true;
            }
            else
            {
                _buildView.Print(string.Format("=================== Failed to prep: {0} ==================\n", project.Name));
                return false;
            }
        }

        /// <summary>
        /// Builds a game distribution from a project using the current compiler.
        /// </summary>
        /// <param name="project">The project to build.</param>
        /// <returns>The full path of the compiled distribution.</returns>
        public static async Task<string> Build(Project project, bool forceVisible = false)
        {
            ICompiler compiler = PluginManager.Get<ICompiler>(project.Compiler);
            if (compiler == null)
            {
                MessageBox.Show(
                    string.Format("Unable to build '{0}'.\n\nA required toolchain plugin is missing.  You may not have the necessary toolchain installed, or the plugin may be disabled.  Open Configuration Manager and check your plugins.\n\nToolchain Required:\n{1}/{2}", project.Name, project.Engine, project.Compiler),
                    "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            _buildView.Clear();
            PluginManager.Core.Docking.Show(_buildView);

            _buildView.Print(string.Format("------------------- Build started: {0} -------------------\n", project.Name));
            string outPath = Path.Combine(project.RootPath, project.BuildPath);
            if (await compiler.Build(project, outPath, _buildView))
            {
                _buildView.Print(string.Format("================= Successfully built: {0} ================\n", project.Name));
                if (Core.Settings.AutoHideBuild && !forceVisible)
                    PluginManager.Core.Docking.Hide(_buildView);
                return outPath;
            }
            else
            {
                _buildView.Print(string.Format("================== Failed to build: {0} ==================\n", project.Name));
                SystemSounds.Exclamation.Play();
                return null;
            }
        }

        /// <summary>
        /// Builds a game package from a project using the current compiler.
        /// </summary>
        /// <param name="project">The project to build.</param>
        /// <param name="fileName">The full path of the package to build. If the file exists, it will be overwritten.</param>
        /// <returns>'true' if packaging succeeded, false if not.</returns>
        public static async Task<bool> Package(Project project, string fileName)
        {
            if (!CanPackage(project))
                throw new NotSupportedException("The current compiler doesn't support packaging.");

            _buildView.Clear();
            PluginManager.Core.Docking.Show(_buildView);
            _buildView.Print(string.Format("----------------- Packaging started: {0} -----------------\n", project.Name));
            var packager = PluginManager.Get<IPackager>(project.Compiler);
            bool isOK = await packager.Package(project, fileName, _buildView);
            if (isOK)
                _buildView.Print(string.Format("=============== Successfully packaged: {0} ===============\n", project.Name));
            else
            {
                _buildView.Print(string.Format("================= Failed to package: {0} =================\n", project.Name));
                SystemSounds.Exclamation.Play();
            }
            return isOK;
        }

        /// <summary>
        /// Starts single-step debugging a project using the current engine.
        /// </summary>
        /// <param name="project">The project to debug.</param>
        /// <returns>An IDebugger used to manage the debugging session.</returns>
        public static async Task<IDebugger> Debug(Project project)
        {
            if (!CanDebug(project))
                throw new NotSupportedException("The current engine starter doesn't support debugging.");

            var starter = PluginManager.Get<IDebugStarter>(project.Engine);
            string outPath = await Build(project);
            if (outPath != null)
                return starter.Debug(outPath, false, project);
            else
                return null;
        }

        /// <summary>
        /// Tests the game with the current engine.
        /// </summary>
        /// <param name="project">The project to test.</param>
        public static async Task Test(Project project)
        {
            var starter = PluginManager.Get<IStarter>(project.Engine);
            if (starter != null)
            {
                string outPath = await Build(project);
                if (outPath != null)
                {
                    starter.Start(outPath, false);
                }
            }
            else
            {
                MessageBox.Show(
                    string.Format("Unable to test '{0}'.\n\nA required toolchain plugin is missing.  You may not have the necessary toolchain installed, or the plugin may be disabled.  Open Configuration Manager and check your plugins.\n\nToolchain Required:\n{1}/{2}", project.Name, project.Engine, project.Compiler),
                    "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
