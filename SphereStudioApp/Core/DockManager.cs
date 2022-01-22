using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using SphereStudio.Base;

using WeifenLuo.WinFormsUI.Docking;

namespace SphereStudio.Core
{
    struct DockPaneShim
    {
        public string Name;
        public DockContent Content;
        public IDockPane Pane;
    }

    class DockManager : IDock
    {
        List<DockPaneShim> activePanes = new List<DockPaneShim>();
        DockPanel mainDockPanel;

        public DockManager(DockPanel mainPanel)
        {
            mainDockPanel = mainPanel;
        }

        public void Activate(IDockPane pane)
        {
            Refresh();
            var shim = activePanes.Find(x => x.Pane == pane);
            if (shim.Pane != null && IsVisible(shim.Pane))
            {
                var oldFocus = mainDockPanel.Parent;
                while (oldFocus is ContainerControl container)
                    oldFocus = container.ActiveControl;
                shim.Content.Show();
                if (isAutoHidden(shim))
                    mainDockPanel.ActiveAutoHideContent = shim.Content;
                if (oldFocus != null)
                    oldFocus.Focus();
            }
        }

        public void Hide(IDockPane pane)
        {
            Refresh();
            var form = activePanes.Find(x => x.Pane == pane);
            if (form.Pane != null)
                form.Content.Hide();
        }

        public bool IsVisible(IDockPane pane)
        {
            var shim = activePanes.Find(x => x.Pane == pane);
            return shim.Pane != null && !shim.Content.IsHidden;
        }

        public void Persist()
        {
            Session.Settings.AutoHidePanes = activePanes
                .Where(form => isAutoHidden(form))
                .Select(form => form.Name).ToArray();
            Session.Settings.HiddenPanes = activePanes
                .Where(x => x.Pane.ShowInViewMenu)
                .Where(x => !IsVisible(x.Pane))
                .Select(x => x.Name).ToArray();
        }

        public void Refresh()
        {
            var removedForms = activePanes
                .Where(x => PluginManager.Get<IDockPane>(x.Name) == null)
                .ToArray();
            foreach (DockPaneShim form in removedForms)
            {
                form.Content.Dispose();
                activePanes.Remove(form);
            }
            var newPanels = from name in PluginManager.GetNames<IDockPane>()
                            where activePanes.All(form => form.Name != name)
                            select name;
            foreach (string name in newPanels)
            {
                var plugin = PluginManager.Get<IDockPane>(name);
                var shim = new DockPaneShim() { Name = name, Pane = plugin };
                shim.Content = new DockContent() { Name = name, TabText = name };
                shim.Content.Controls.Add(plugin.Control);
                shim.Content.Icon = plugin.DockIcon != null
                    ? Icon.FromHandle(plugin.DockIcon.GetHicon())
                    : null;
                shim.Content.HideOnClose = true;
                shim.Content.DockAreas = DockAreas.Float
                    | DockAreas.DockLeft | DockAreas.DockRight
                    | DockAreas.DockTop | DockAreas.DockBottom;
                var autoHide = Session.Settings.AutoHidePanes.Contains(name);
                DockState state = plugin.DockHint == DockHint.Float ? DockState.Float
                    : plugin.DockHint == DockHint.Left ? (autoHide ? DockState.DockLeftAutoHide : DockState.DockLeft)
                    : plugin.DockHint == DockHint.Right ? (autoHide ? DockState.DockRightAutoHide : DockState.DockRight)
                    : plugin.DockHint == DockHint.Top ? (autoHide ? DockState.DockTopAutoHide : DockState.DockTop)
                    : plugin.DockHint == DockHint.Bottom ? (autoHide ? DockState.DockBottomAutoHide : DockState.DockBottom)
                    : DockState.Float;  // stacked and nested ternary = awesome
                plugin.Control.Dock = DockStyle.Fill;
                shim.Content.Show(mainDockPanel, state);
                if (!plugin.ShowInViewMenu || Session.Settings.HiddenPanes.Contains(name))
                    shim.Content.Hide();
                activePanes.Add(shim);
            }
        }

        public void Show(IDockPane pane)
        {
            Refresh();
            var shim = activePanes.Find(x => x.Pane == pane);
            if (shim.Pane != null && !IsVisible(shim.Pane))
            {
                var oldFocus = mainDockPanel.Parent;
                while (oldFocus is ContainerControl container)
                    oldFocus = container.ActiveControl;
                shim.Content.Show();
                if (oldFocus != null)
                    oldFocus.Focus();
            }
        }

        public void Toggle(IDockPane pane)
        {
            var form = activePanes.Find(x => x.Pane == pane);
            if (form.Pane != null)
            {
                if (!IsVisible(form.Pane))
                    Show(pane);
                else
                    Hide(pane);
            }
        }

        private bool isAutoHidden(DockPaneShim form)
        {
            var dockState = form.Content.DockState;
            return dockState == DockState.DockLeftAutoHide
                || dockState == DockState.DockRightAutoHide
                || dockState == DockState.DockTopAutoHide
                || dockState == DockState.DockBottomAutoHide;
        }
    }
}
