using System;
using System.Reflection;

using SphereStudio.Base;

namespace SphereStudio.Core
{
    class PluginShim
    {
        private bool m_isEnabled = false;

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

        public IPluginMain Main { get; private set; }
        public string Handle { get; private set; }

        public bool Enabled
        {
            get { return m_isEnabled; }
            set { if (value) Activate(); else Deactivate(); }
        }

        public void Activate()
        {
            if (!m_isEnabled)
            {
                ISettings conf = new IniSettings(Session.MainIniFile, Handle);
                try {
                    Main.Initialize(conf);
                    m_isEnabled = true;
                }
                catch { }
            }
        }

        public void Deactivate()
        {
            if (m_isEnabled)
            {
                m_isEnabled = false;
                Main.ShutDown();
                PluginManager.UnregisterAll(Main);
            }
        }
    }
}
