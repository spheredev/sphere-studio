﻿using System;

namespace SphereStudio
{
    // note: `const` is resolved at compile time in the referencing assembly.  this is
    //       desirable for built-in plugins to avoid them dynamically adopting the version number
    //       of the Sphere Studio installation they're running against.

    /// <summary>
    /// Provides versioning information for Sphere Studio.
    /// </summary>
    public static class Versioning
    {
        /// <summary>
        /// The name of the IDE used for branding.
        /// </summary>
        public const string Name = "Sphere Studio";

        /// <summary>
        /// The name of the author used for branding.
        /// </summary>
        public const string Author = "Where'd She Go? Productions";

        /// <summary>
        /// The version number of the software.
        /// </summary>
        public const string Version = "2.3.10";

        /// <summary>
        /// Whether the Sphere Studio version being built against is a WiP version.
        /// </summary>
        public const bool IsWiP = false;

        /// <summary>
        /// A string indicating the copyright holder and year(s) of copyright.
        /// </summary>
        public const string Copyright = "© 2024 Where'd She Go? LLC";

        /// <summary>
        /// A short description of the software along with a list of contributors.
        /// </summary>
        public const string Credits =
            "DEVELOPERS\r\n" +
            "    Bruce Pascoe ('Fat Cerberus')\r\n" +
            "    Andrew Helenius ('Radnen')\r\n" +
            "\r\nTESTERS\r\n" +
            "    DaVince\r\n" +
            "    Eggbertx\r\n" +
            "    Flying Jester\r\n";
    }
}
