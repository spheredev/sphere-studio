using System;
using System.Reflection;

using SphereStudio.Base;

namespace SphereStudio.Core
{
    class PluginShim
    {
        private bool enabled = false;

        public PluginShim(string fileName, string handle)
        {
            Handle = handle;
            Assembly assembly = Assembly.LoadFrom(fileName);
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetInterface("IPluginMain") != null)
                {
                    Main = type.InvokeMember(null, BindingFlags.CreateInstance, null, null, null) as IPluginMain;
                    break;
                }
            }
        }

        public PluginShim(IPluginMain main, string handle)
        {
            Handle = handle;
            Main = main;
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (value)
                    Activate();
                else
                    Deactivate();
            }
        }

        public string Handle { get; private set; }

        public IPluginMain Main { get; private set; }

        public void Activate()
        {
            if (!enabled)
            {
                var settings = new IniSettings(Session.MainIniFile, Handle);
                try {
                    Main.Initialize(settings);
                    enabled = true;
                }
                catch
                {
                    // *MUNCH*
                }
            }
        }

        public void Deactivate()
        {
            if (enabled)
            {
                enabled = false;
                Main.ShutDown();
                PluginManager.UnregisterAll(Main);
            }
        }
    }
}
