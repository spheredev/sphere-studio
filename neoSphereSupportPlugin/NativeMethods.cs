﻿using System;
using System.Runtime.InteropServices;

namespace SphereStudio
{
    static class NativeMethods
    {
        [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
