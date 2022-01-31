using System.Threading.Tasks;

namespace SphereStudio.Base
{
    /// <summary>
    /// Specifies the base interface for a compiler. Compilers correspond 1:1 to project
    /// types in Sphere Studio.
    /// </summary>
    public interface ICompiler : IPlugin
    {
        /// <summary>
        /// Builds a game distribution from a Sphere Studio project.
        /// </summary>
        /// <param name="project">The project to build.</param>
        /// <param name="debuggable">Whether to include debugging information in the build.</param>
        /// <param name="console">An IConsole where compiler output will be sent.</param>
        /// <returns>The full path of the directory where the game was built.</returns>
        Task<string> Build(IProject project, bool debuggable, IConsole console);

        /// <summary>
        /// Prepare a new project for use with this compiler.
        /// </summary>
        /// <param name="project">The project to prepare.</param>
        /// <param name="con">An IConsole where project prep progress can be printed.</param>
        /// <returns>true if the project was successfully prepared.</returns>
        bool Prep(IProject project, IConsole con);

        /// <summary>
        /// Completely rebuilds a game distribution from a Sphere Studio project, even parts that have
        /// already been built.
        /// </summary>
        /// <param name="project">The project to rebuild.</param>
        /// <param name="debuggable">Whether to include debugging information in the build.</param>
        /// <param name="console">An IConsole where compiler output will be sent.</param>
        /// <returns>The full path of the directory where the game was built.</returns>
        Task<string> Rebuild(IProject project, bool debuggable, IConsole console);
    }

    /// <summary>
    /// Specifies the interface for a packaging compiler.
    /// </summary>
    public interface IPackager : ICompiler
    {
        /// <summary>
        /// Gets a list of package file filters, in the same format as used for
        /// SaveFileDialog.
        /// </summary>
        string SaveFileFilters { get; }

        /// <summary>
        /// Builds a game package from a Sphere Studio project.
        /// </summary>
        /// <param name="project">The project to build.</param>
        /// <param name="fileName">The pathname of the package. If this file exists, it will be overwritten.</param>
        /// <param name="debuggable">'true' if debugging info should be included in the package.</param>
        /// <param name="con">An IConsole where compiler output will be sent.</param>
        /// <returns>'true' if packaging succeeded, false if not.</returns>
        Task<bool> Package(IProject project, string fileName, bool debuggable, IConsole con);
    }
}
