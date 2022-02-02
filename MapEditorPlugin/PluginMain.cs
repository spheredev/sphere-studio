using System;
using System.Drawing;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.DocumentViews;
using SphereStudio.FileOpeners;
using SphereStudio.Forms;

namespace SphereStudio
{
    public class PluginMain : IPluginMain
    {
        public string Name => "Sphere Map Editor";
        public string Description => "Sphere RMP format tilemap editor";
        public string Version => Versioning.Version;
        public string Author => Versioning.Author;

        internal static void ShowMenus(bool visible)
        {
            _mapMenu.Visible = visible;
        }
        
        public void Initialize(ISettings settings)
        {
            PluginManager.Register(this, new MapFileOpener(), Name);
            PluginManager.Core.AddMenuItem(_mapMenu, "Project");
        }

        public void ShutDown()
        {
            PluginManager.UnregisterAll(this);
            PluginManager.Core.RemoveMenuItem(_mapMenu);
        }

        #region initialize the Map menu
        private static ToolStripMenuItem _mapMenu;
        private static ToolStripMenuItem _exportTilesetMenuItem;
        private static ToolStripMenuItem _mapPropertiesMenuItem;
        private static ToolStripMenuItem _recenterMenuItem;
        private static ToolStripMenuItem _importTilesetMenuItem;

        static PluginMain()
        {
            _mapMenu = new ToolStripMenuItem("&Map") { Visible = false };
            _exportTilesetMenuItem = new ToolStripMenuItem("E&xport Tileset...", null, menuExportTileset_Click);
            _importTilesetMenuItem = new ToolStripMenuItem("&Import Tileset...", null, menuImportTileset_Click);
            _mapPropertiesMenuItem = new ToolStripMenuItem("Map &Properties...", null, menuMapProps_Click);
            _recenterMenuItem = new ToolStripMenuItem("Re&center Map", Properties.Resources.arrow_inout, menuMapProps_Click);
            _mapMenu.DropDownItems.AddRange(new ToolStripItem[] {
                _recenterMenuItem,
                new ToolStripSeparator(),
                _exportTilesetMenuItem,
                _importTilesetMenuItem,
                new ToolStripSeparator(),
                _mapPropertiesMenuItem });
        }

        private static void menuRecenter_Click(object sender, EventArgs e)
        {
            var mapView = PluginManager.Core.ActiveDocument as MapDocumentView;
            mapView?.MapControl.CenterMap();
        }

        private static void menuImportTileset_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void menuExportTileset_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = PluginManager.Core.Project.RootPath;
                dialog.Filter = @"Image Files (.png)|*.png;";
                dialog.DefaultExt = "png";
                if (dialog.ShowDialog() == DialogResult.OK)
                    (PluginManager.Core.ActiveDocument as MapDocumentView).SaveTileset(dialog.FileName);
            }
        }

        private static void menuUpdateFromFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog diag = new OpenFileDialog())
            {
                diag.InitialDirectory = PluginManager.Core.Project.RootPath;
                diag.Filter = @"Image Files (.png)|*.png";

                if (diag.ShowDialog() == DialogResult.OK)
                    (PluginManager.Core.ActiveDocument as MapDocumentView).UpdateTileset(diag.FileName);
            }
        }

        private static void menuMapProps_Click(object sender, EventArgs e)
        {
            var mapView = PluginManager.Core.ActiveDocument as MapDocumentView;
            using (var dialog = new MapPropertiesForm(mapView.Map))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    mapView.SetTileSize(mapView.Map.Tileset.TileWidth, mapView.Map.Tileset.TileHeight);
            }
        }
        #endregion
    }
}
