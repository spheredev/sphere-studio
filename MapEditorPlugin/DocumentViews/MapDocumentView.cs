﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using SphereStudio.Base;
using SphereStudio.Components;
using SphereStudio.UndoRedo;
using SphereStudio.Formats;

namespace SphereStudio.DocumentViews
{
    partial class MapDocumentView : DocumentView
    {
        DockContent _mapContent;
        DockContent _drawContent;
        DockContent _tileContent;
        DockContent _layerContent;
        DockContent _tilesetContent;
        DockContent _entityContent;
        DockPanel _mainPanel;

        public Map Map => MapControl.BaseMap;

        public MapDocumentView()
        {
            InitializeComponent();
            InitializeDocking();

            Icon = Icon.FromHandle(Properties.Resources.MapIcon.GetHicon());

            TileEditor.Tileset = TilesetControl;
            TilesetControl.MultiSelect = true;
        }

        public override string[] FileExtensions => new[] { "rmp" };

        public override string ViewState
        {
            get
            {
                var vScroll = MapControl.vScrollBar.Value;
                var hScroll = MapControl.hScrollBar.Value;
                var tile = TilesetControl.Selected[0];
                var layer = MapControl.CurrentLayer;
                return $"{vScroll}|{hScroll}|{tile}|{layer}";
            }
            set
            {
                string[] parse = value.Split('|');

                MapControl.vScrollBar.Value = Convert.ToInt32(parse[0]);
                MapControl.hScrollBar.Value = Convert.ToInt32(parse[1]);
                TilesetControl.Select(Convert.ToInt16(parse[2]));
                MapControl.CurrentLayer = Convert.ToInt16(parse[3]);
            }
        }

        public override bool NewDocument()
        {
            using (var dialog = new Forms.NewMapDialogue())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    CreateNew(dialog.MapWidth, dialog.MapHeight, dialog.TileWidth, dialog.TileHeight, dialog.Tileset);
                else
                    return false;
                return true;
            }
        }

        public override void Load(string filepath)
        {
            Map map = new Map();
            map.Load(filepath);

            MapControl.BaseMap = map;
            TilesetControl.Tileset = MapControl.BaseMap.Tileset;
            TilesetControl.Select(0);
            SelectTiles(TilesetControl.Selected);
            TilesetControl.ZoomIn();
            InitLayers();
            MapControl.UpdateView();

            Invalidate(true);
        }

        public override void Save(string filepath)
        {
            if (!Map.Save(filepath))
            {
                if (MessageBox.Show(@"Tileset needs to be saved.", @"Save the Tileset", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    using (SaveFileDialog diag = new SaveFileDialog())
                    {
                        diag.Filter = @"Tileset Files (.rts)|*.rts";
                        diag.InitialDirectory = PluginManager.Core.Project.RootPath + "\\maps";
                        if (diag.ShowDialog() == DialogResult.OK)
                        {
                            Map.Scripts[0] = Path.GetFileName(diag.FileName);
                            Map.Save(filepath);
                        }
                    }
                }
            }
            Dirty = false;
        }

        public override void Activate()
        {
            PluginMain.ShowMenus(true);
        }

        public override void Deactivate()
        {
            PluginMain.ShowMenus(false);
        }

        public override void ZoomIn()
        {
            MapControl.ZoomIn();
        }

        public override void ZoomOut()
        {
            MapControl.ZoomOut();
        }

        #region docking
        public void InitializeDocking()
        {
            Controls.Remove(mapSplitter);
            Controls.Remove(SplitContainer);
            Controls.Remove(layerTileSplit);
            Controls.Remove(EditorTabs);

            _mapContent = new DockContent();
            _mapContent.Controls.Add(MapToolContainer);
            _mapContent.Text = @"Map Editor";
            _mapContent.DockAreas = DockAreas.DockBottom | DockAreas.Document | DockAreas.DockTop;
            _mapContent.DockHandler.CloseButtonVisible = false;

            _drawContent = new DockContent();
            _drawContent.Controls.Add(TileDrawer);
            _drawContent.Text = @"Tile Image Editor";
            _drawContent.DockAreas = DockAreas.DockBottom | DockAreas.DockTop | DockAreas.Document;
            _drawContent.DockHandler.CloseButtonVisible = false;

            _tilesetContent = new DockContent();
            _tilesetContent.Controls.Add(TilesetPanel);
            TilesetControl.Width = TilesetPanel.Width - 2;
            _tilesetContent.Text = @"Tileset";
            _tilesetContent.DockAreas = DockAreas.DockRight | DockAreas.DockLeft;
            _tilesetContent.DockHandler.CloseButtonVisible = false;
            _tilesetContent.AutoScroll = true;

            _entityContent = new DockContent();
            _entityContent.Controls.Add(EntitiesControl);
            _entityContent.Text = @"Entity List";
            _entityContent.DockAreas = DockAreas.DockRight | DockAreas.DockLeft | DockAreas.DockBottom;
            _entityContent.DockHandler.CloseButtonVisible = false;

            _tileContent = new DockContent();
            _tileContent.Controls.Add(TileEditor);
            _tileContent.Text = @"Tile Properties";
            _tileContent.DockAreas = DockAreas.DockBottom | DockAreas.DockTop | DockAreas.Document;
            _tileContent.DockHandler.CloseButtonVisible = false;

            _layerContent = new DockContent();
            _layerContent.Controls.Add(LayerEditor);
            LayerEditor.Layers.LayerChanged += Layers_LayerChanged;
            LayerEditor.Layers.LayerSelected += Layers_LayerSelected;
            LayerEditor.Layers.LayerVisibilityChanged += Layers_LayerVisibilityChanged;
            _layerContent.Text = @"Map Layers";
            _layerContent.DockAreas = DockAreas.DockLeft | DockAreas.DockRight;
            _layerContent.DockHandler.CloseButtonVisible = false;

            _mainPanel = new DockPanel { Dock = DockStyle.Fill, DocumentStyle = DocumentStyle.DockingWindow };
            if (System.IO.File.Exists("MapEditor.xml"))
            {
                DeserializeDockContent dc = GetContent;
                _mainPanel.LoadFromXml("MapEditor.xml", dc);
            }
            else
            {
                _mapContent.Show(_mainPanel, DockState.Document);
                _tileContent.Show(_mapContent.Pane, DockAlignment.Bottom, 0.40);
                _drawContent.Show(_tileContent.PanelPane, _tileContent);
                _entityContent.Show(_mainPanel, DockState.DockRight);
                _layerContent.Show(_entityContent.Pane, _entityContent);
                _tilesetContent.Show(_layerContent.Pane, DockAlignment.Bottom, 0.66);
            }

            Controls.Add(_mainPanel);
        }

        private int _id = -1;
        public IDockContent GetContent(string persist)
        {
            if (persist == "WeifenLuo.WinFormsUI.Docking.DockContent")
            {
                _id++;
                switch (_id)
                {
                    case 0: return _mapContent;
                    case 1: return _tileContent;
                    case 2: return _drawContent;
                    case 3: return _layerContent;
                    case 4: return _tilesetContent;
                    default: return new DockContent();
                }
            }
            return new DockContent();
        }

        #endregion

        public void CreateNew(short width = 20,
                              short height = 15, short tileWidth = 16,
                              short tileHeight = 16, string tilesetPath = null)
        {
            Map map = new Map();
            map.CreateNew(width, height, tileWidth, tileHeight, tilesetPath);
            MapControl.BaseMap = map;

            TilesetControl.Tileset = MapControl.BaseMap.Tileset;
            TilesetControl.Select(0);
            SelectTiles(TilesetControl.Selected);
            InitLayers();
            MapControl.UpdateView();

            Invalidate(true);
        }

        public void SaveLayout()
        {
            _mainPanel.SaveAsXml("MapEditor.xml");
        }

        private void InitLayers()
        {
            LayerEditor.Layers.ClearItems();
            foreach (Layer lay in MapControl.BaseMap.Layers)
            {
                LayerItem item = new LayerItem(lay);
                LayerEditor.Layers.AddItem(item);
            }

            foreach (Entity ent in Map.Entities)
                ent.Visible = Map.Layers[ent.Layer].Visible;

            foreach (Zone zone in Map.Zones)
                zone.Visible = Map.Layers[zone.Layer].Visible;

            LayerEditor.Layers.StartLayer = MapControl.BaseMap.StartLayer;
            LayerEditor.Layers.SelectItem(MapControl.CurrentLayer);
            EntitiesControl.UpdateList(Map.Entities, Map.Layers);
        }

        public override void Undo()
        {
            undoButton.Enabled = MapControl.Undo();
            redoButton.Enabled = true;
            InitLayers();
        }

        public override void Redo()
        {
            redoButton.Enabled = MapControl.Redo();
            undoButton.Enabled = true;
            InitLayers();
        }

        public void SetTileSize(int tileWidth, int tileHeight)
        {
            MapControl.ResizeLayers(tileWidth, tileHeight);
            TilesetControl.UpdateTileSize();
            TilesetControl.Invalidate();
            Dirty = true;
        }

        public void UpdateTileset(string filename)
        {
            Map.Tileset.UpdateFromImage(filename);
            MapControl.RefreshLayers();
            TilesetControl.Invalidate();
            Invalidate(true);
        }

        public void SaveTileset(string filename)
        {
            Map.Tileset.SaveImage(filename);
        }

        private void Layers_LayerVisibilityChanged(LayerControl sender, LayerItem layer)
        {
            Map.Layers[layer.Index].Visible = layer.Visible;
            foreach (Entity ent in Map.Entities)
                if (ent.Layer == layer.Index) ent.Visible = layer.Visible;
            foreach (Zone zone in Map.Zones)
                if (zone.Layer == layer.Index) zone.Visible = layer.Visible;
            MapControl.Invalidate();
            Dirty = true;
        }

        private void Layers_LayerSelected(LayerControl sender, LayerItem layer)
        {
            MapControl.CurrentLayer = (short)layer.Index;
            MapControl.Invalidate();
        }

        private void Layers_LayerChanged(LayerControl sender, LayerItem layer)
        {
            List<Layer> layers = sender.Items.Select(li => li.Layer).ToList();
            byte start = (byte)((layer.Start) ? layer.Index : sender.StartLayer);

            if (layer.State == ListViewItemStates.Selected)
                MapControl.CurrentLayer = (short)layer.Index;

            MapControl.SetLayers(layers, start);
            redoButton.Enabled = MapControl.CanRedo;
            undoButton.Enabled = MapControl.CanUndo;
            Dirty = true;
        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            zoomInButton.Enabled = MapControl.ZoomIn();
            zoomOutButton.Enabled = true;
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            zoomOutButton.Enabled = MapControl.ZoomOut();
            zoomInButton.Enabled = true;
        }

        private void PenButton_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            PenButton.Checked = true;
            MapControl.Tool = MapControl.MapTool.Pen;
            MapControl.Invalidate();
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            RectangleButton.Checked = true;
            MapControl.Tool = MapControl.MapTool.Rectangle;
            MapControl.Invalidate();
        }

        private void FloodFillButton_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            FloodFillButton.Checked = true;
            MapControl.Tool = MapControl.MapTool.FloodFill;
            MapControl.Invalidate();
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            LineButton.Checked = true;
            MapControl.Tool = MapControl.MapTool.Line;
            MapControl.Invalidate();
        }

        private void UncheckButtons()
        {
            PenButton.Checked = zoneButton.Checked = false;
            RectangleButton.Checked = EntityButton.Checked = false;
            LineButton.Checked = FloodFillButton.Checked = false;
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            Undo();
            TilesetControl.UpdateHeight();
            SelectTiles(TilesetControl.Selected);
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            Redo();
            TilesetControl.UpdateHeight();
            SelectTiles(TilesetControl.Selected);
        }

        private void SelectTiles(List<short> tiles)
        {
            MapControl.Tiles = tiles;
            MapControl.SelWidth = TilesetControl.Selection.Width;
            MapControl.CurrentTile = tiles[0];
            Bitmap img = TilesetControl.GetCompiledImage();
            TileDrawer.Content = img;
            img.Dispose();
            TileEditor.Tile = Map.Tileset.Tiles[tiles[0]];
        }

        private void showCameraButton_Click(object sender, EventArgs e)
        {
            MapControl.ShowCameraBounds = !MapControl.ShowCameraBounds;
            showCameraButton.Checked = !showCameraButton.Checked;
            MapControl.Invalidate();
        }

        private void MapControl_Edited(object sender, EventArgs e)
        {
            redoButton.Enabled = MapControl.CanRedo;
            undoButton.Enabled = MapControl.CanUndo;
            Dirty = true;
        }

        private void zoneButton_Click(object sender, EventArgs e)
        {
            PenButton.Checked = RectangleButton.Checked = false;
            zoneButton.Checked = true;
            MapControl.Tool = MapControl.MapTool.Zone;
            MapControl.Invalidate();
        }

        private void TileDrawer_ImageEdited(object sender, EventArgs e)
        {
            short tw = TilesetControl.Tileset.TileWidth;
            short th = TilesetControl.Tileset.TileHeight;
            TilesetControl.SetImages(TileDrawer.GetImages(tw, th));
            MapControl.RefreshLayers();
            Dirty = true;
        }

        private void MapControl_Paint(object sender, PaintEventArgs e)
        {
            var tile = MapControl.MouseTile.ToString();
            map_pos_label.Text = $"{tile} Start: {Map.StartLayer}";
            zoomInButton.Enabled = MapControl.CanZoomIn;
            zoomOutButton.Enabled = MapControl.CanZoomOut;
        }

        private void MapControl_PropChanged(object sender, EventArgs e)
        {
            LayerEditor.Layers.StartLayer = Map.StartLayer;
            LayerEditor.Layers.Invalidate();
            EntitiesControl.UpdateList(Map.Entities, Map.Layers);
            TilesetControl.Select(MapControl.CurrentTile);
            SelectTiles(TilesetControl.Selected);
            MapControl.SelWidth = 1;
        }

        private void EntityButton_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            EntityButton.Checked = true;
            MapControl.Tool = MapControl.MapTool.Entity;
        }

        private void Layers_LayerAdded(object sender, EventArgs e)
        {
            Layer lay = MapControl.AddLayer();
            LayerItem item = new LayerItem(lay) { Text = "Untitled", Visible = true };
            LayerEditor.Layers.AddItem(item);
            MapControl.RefreshLayers();
            Dirty = true;
        }

        private void Layers_LayerRemoved(object sender, EventArgs e)
        {
            Layer target = LayerEditor.Layers.Items[LayerEditor.Layers.SelectedIndex].Layer;

            for (int i = 0; i < MapControl.GraphicLayers.Count; ++i)
            {
                if (MapControl.GraphicLayers[i].TargetLayer == target)
                {
                    MapControl.GraphicLayers.RemoveAt(i);
                    break;
                }
            }

            Map.Layers.Remove(target);
            LayerEditor.Layers.RemoveItem(LayerEditor.Layers.SelectedIndex);
            MapControl.RefreshLayers();
            Dirty = true;
        }

        private void TilesetControl_TileSelected(List<short> tiles)
        {
            SelectTiles(tiles);
        }

        private void TilesetControl_TileRemoved(short tile, List<Tile> tiles)
        {
            TileRemovePage page = new TileRemovePage(this, tiles, tile, Map.CloneAllLayerTiles());
            MapControl.PushTileLayerPage(page);

            foreach (Layer lay in Map.Layers) lay.AdjustTiles(tile, (short)-tiles.Count);
            MapControl.RefreshLayers();

            redoButton.Enabled = MapControl.CanRedo;
            undoButton.Enabled = MapControl.CanUndo;
            TilesetControl.Select(tile);
            Dirty = true;
        }

        private void TilesetControl_TileAdded(short tile, List<Tile> tiles)
        {
            TileAddPage page = new TileAddPage(this, tiles, tile, Map.CloneAllLayerTiles());
            MapControl.PushTileLayerPage(page);

            foreach (Layer lay in Map.Layers) lay.AdjustTiles((short)(tile - 1), (short)tiles.Count);
            MapControl.RefreshLayers();

            redoButton.Enabled = MapControl.CanRedo;
            undoButton.Enabled = MapControl.CanUndo;
            TilesetControl.Select(tile);
            Dirty = true;
        }

        private void ShowNumButton_Click(object sender, EventArgs e)
        {
            MapControl.ShowTileNums = ShowNumButton.Checked;
            MapControl.Invalidate();
        }
    }
}
