using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SphereStudio.Base;

namespace SphereStudio.SettingsPages
{
    partial class CellSettingsPage : UserControl, ISettingsPage, IStyleAware
    {
        private PluginConf conf;

        public CellSettingsPage(PluginConf conf)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            this.conf = conf;
        }

        public Control Control => this;
        public SettingsPageType Type => SettingsPageType.Compiler;

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            
            style.AsHeading(tipHeading);
            style.AsAccent(tipPanel);

            style.AsHeading(configHeading);
            style.AsAccent(configPanel);
            style.AsAccent(debuggableSpkCheckBox);
        }

        public void Populate()
        {
            debuggableSpkCheckBox.Checked = conf.MakeDebugPackages;
        }

        public void Save()
        {
            conf.MakeDebugPackages = debuggableSpkCheckBox.Checked;
        }

        public bool Verify()
        {
            return true;
        }
    }
}
