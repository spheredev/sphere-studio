namespace SphereStudio.Base
{
    /// <summary>
    /// Specifies the interface for an engine starter. Starters handle launching
    /// an engine with the proper command line arguments, etc.
    /// </summary>
    public interface IStarter : IPlugin
    {
        /// <summary>
        /// Gets a value indicating whether this engine supports configuration.
        /// </summary>
        bool CanConfigure { get; }

        /// <summary>
        /// Launches this engine's configuration program. If the engine doesn't support configuration,
        /// this throws an error.
        /// </summary>
        void Configure();

        /// <summary>
        /// Starts the engine.
        /// </summary>
        /// <param name="gamePath">The pathname of the game or package to launch.</param>
        /// <param name="isPackage">Pass 'true' if gamePath specifies a package.</param>
        void Start(string gamePath, bool isPackage);
    }

    /// <summary>
    /// Specifies the interface for an engine starter supporting single-step
    /// debugging.
    /// </summary>
    public interface IDebugStarter : IStarter
    {
        /// <summary>
        /// Starts the engine in single-step debugging mode.
        /// </summary>
        /// <param name="gamePath">The pathname of the game or package to launch.</param>
        /// <param name="isPackage">Pass 'true' if gamePath specifies a package.</param>
        /// <param name="project">The Sphere Studio project hosting the debugger.</param>
        /// <returns></returns>
        IDebugger Debug(string gamePath, bool isPackage, IProject project);
    }
}
