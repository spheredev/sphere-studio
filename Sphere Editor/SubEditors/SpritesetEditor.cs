﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Sphere_Editor.SphereObjects;
using Sphere_Editor.Forms;
using Sphere_Editor.EditorComponents;
using Sphere_Editor.SpritesetComponents;
using WeifenLuo.WinFormsUI.Docking;

namespace Sphere_Editor.SubEditors
{
    public partial class SpritesetEditor : EditorObject
    {
        private string _filename = null;
        private Spriteset _sprite = null;
        private int _zoom = 3;
        private DirectionLayout _selected_direction = null;
        private TilesetControl _tileset_ctrl = null;
        private FramePanel _selected_frame = null;

        // Dock controls:
        private DockPanel MainDockPanel      = null;
        private DockContent DrawContent      = null;
        private DockContent DirectionContent = null;
        private DockContent ImageContent     = null;
        private DockContent AnimContent      = null;
        private DockContent BaseContent      = null;

        public SpritesetEditor()
        {
            InitializeComponent();
            InitializeDocking();

            if (Global.CurrentEditor.UseDockForm) Controls.Remove(DirectionSplitter);

            _sprite = new Spriteset();
            DirectionAnim.Sprite = _sprite;
            FrameBaseEditor.Sprite = _sprite;
            FrameBaseEditor.Invalidate(true);
        }

        #region dock content
        private void InitializeDocking()
        {
            if (!Global.CurrentEditor.UseDockForm) return;

            SpriteDrawer.Dock = DockStyle.Fill;
            DrawContent = new DockContent();
            DrawContent.Text = "Sprite Drawer";
            DrawContent.DockAreas = DockAreas.Document;
            DrawContent.DockHandler.CloseButtonVisible = false;
            DrawContent.Controls.Add(SpriteDrawer);

            DirectionHolder.Dock = DockStyle.Fill;
            DirectionContent = new DockContent();
            DirectionContent.Text = "Sprite Directions";
            DirectionContent.DockAreas = DockAreas.Document;
            DirectionContent.DockHandler.CloseButtonVisible = false;
            DirectionContent.Controls.Add(DirectionHolder);

            ImagePanel.Dock = DockStyle.Fill;
            ImageContent = new DockContent();
            ImageContent.Text = "Spriteset Images";
            ImageContent.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float;
            ImageContent.DockHandler.CloseButtonVisible = false;
            ImageContent.Controls.Add(ImagePanel);

            AnimPanel.Dock = DockStyle.Fill;
            AnimContent = new DockContent();
            AnimContent.Text = "Direction Animation";
            AnimContent.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float | DockAreas.DockBottom | DockAreas.DockTop;
            AnimContent.DockHandler.CloseButtonVisible = false;
            AnimContent.Controls.Add(AnimPanel);

            BasePanel.Dock = DockStyle.Fill;
            BaseContent = new DockContent();
            BaseContent.Text = "Base Editor";
            BaseContent.DockAreas = DockAreas.Document | DockAreas.DockTop | DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight;
            BaseContent.DockHandler.CloseButtonVisible = false;
            BaseContent.Controls.Add(BasePanel);

            MainDockPanel = new DockPanel();
            MainDockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            MainDockPanel.Dock = DockStyle.Fill;
            if (System.IO.File.Exists("SpriteEditor.xml"))
            {
                DeserializeDockContent dc = new DeserializeDockContent(GetContent);
                MainDockPanel.LoadFromXml("SpriteEditor.xml", dc);
            }
            else
            {
                DirectionContent.Show(MainDockPanel, DockState.Document);
                BaseContent.Show(DirectionContent.Pane, DockAlignment.Bottom, 0.40);
                DrawContent.Show(BaseContent.PanelPane, BaseContent);
                ImageContent.Show(MainDockPanel, DockState.DockRight);
                AnimContent.Show(ImageContent.Pane, DockAlignment.Bottom, 0.40);
            }

            Controls.Add(MainDockPanel);
        }

        private int _id = -1;
        public IDockContent GetContent(string persist)
        {
            if (persist == "WeifenLuo.WinFormsUI.Docking.DockContent")
            {
                _id++;
                switch (_id)
                {
                    case 0: return DirectionContent;
                    case 1: return BaseContent;
                    case 2: return DrawContent;
                    case 3: return ImageContent;
                    case 4: return AnimContent;
                    default: return new DockContent();
                }
            }
            else return new DockContent();
        }
        #endregion

        private WeifenLuo.WinFormsUI.Docking.IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == "WeifenLuo.WinFormsUI.Docking.DockContent") return DirectionContent;
            else return null;
        }

        public void Init()
        {
            DirectionLayout layout;
            int i = 0;
            foreach (Direction d in _sprite.Directions)
            {
                layout = new DirectionLayout(_sprite, d, this);
                layout.OnFrameClick += new System.EventHandler(layout_OnFrameClick);
                layout.Modified += new System.EventHandler(Modified);
                layout.Zoom = _zoom;
                DirectionHolder.Controls.Add(layout);
                layout.Location = new Point(2, i++ * (layout.Height + 2) + 2);
            }
            ((DirectionLayout)DirectionHolder.Controls[0]).Select(0);
            _selected_frame = ((DirectionLayout)DirectionHolder.Controls[0]).SelectedFrame;
            SpriteDrawer.SetImage((Bitmap)_sprite.GetImage((((DirectionLayout)DirectionHolder.Controls[0]).SelectedFrame.Index)));
            SpriteDrawer.ZoomIn();
            SpriteDrawer.ZoomIn();
            _tileset_ctrl = TilesetControl.FromSprite(_sprite);
            _tileset_ctrl.IsMulti = false;
            _tileset_ctrl.CanDrag = true;
            _tileset_ctrl.CanInsert = false;
            _tileset_ctrl.Sprite = this._sprite;
            _tileset_ctrl.Zoom = 2;
            _tileset_ctrl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            _tileset_ctrl.TileSelected += new TilesetControl.EventHandler(tileset_TileSelected);
            ImageHolder.Controls.Add(_tileset_ctrl);
            _tileset_ctrl.Width = ImageHolder.Width - 6;
            DirectionAnim.Sprite = this._sprite;
            DirectionAnim.Direction = this._sprite.Directions[0];
            FrameBaseEditor.Frame = this._sprite.Directions[0].frames[0];
        }

        public override void CreateNew()
        {
            _sprite.MakeNew();
            Init();
        }

        public override void LoadFile(string filename)
        {
            if (!_sprite.Load(filename))
            {
                _filename = filename;
                MessageBox.Show("Error: Can't load spriteset: " + filename);
                ((DockContent)this.Parent).Close();
            }
            else Init();
        }

        public override void Save()
        {
            if (string.IsNullOrEmpty(_filename)) SaveAs();
            else
            {
                Parent.Text = System.IO.Path.GetFileName(_filename);
                _sprite.Save(_filename);
            }
        }

        public override void SaveAs()
        {
            SaveFileDialog diag = new SaveFileDialog();
            diag.Filter = "Spriteset Files (.rss)|*.rss";

            if (Global.CurrentProject.Path != null)
                diag.InitialDirectory = Global.CurrentProject.Path + "\\spritesets";

            if (diag.ShowDialog() == DialogResult.OK)
            {
                _filename = diag.FileName;
                Save();
            }
        }

        public override void SaveLayout()
        {
            MainDockPanel.SaveAsXml("SpriteEditor.xml");
        }

        public void ResizeAll()
        {
            SizeForm frm = new SizeForm();
            frm.WidthSize = _tileset_ctrl.TileWidth;
            frm.HeightSize = _tileset_ctrl.TileHeight;
            frm.UseScale = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _tileset_ctrl.ResizeAllTiles((short)frm.WidthSize, (short)frm.HeightSize);
                for (short i = 0; i < _sprite.Images.Count; ++i )
                {
                    _sprite.Images[i].Dispose();
                    _sprite.Images[i] = _tileset_ctrl.Tiles[i].Graphic;
                }
                _sprite.SpriteWidth = (short)frm.WidthSize;
                _sprite.SpriteHeight = (short)frm.HeightSize;
            }
            SpriteDrawer.SetImage((Bitmap)_sprite.GetImage(_selected_frame.Index));
            UpdateControls();
            Modified(null, EventArgs.Empty);
        }

        public void RescaleAll()
        {
            SizeForm frm = new SizeForm();
            frm.WidthSize = _tileset_ctrl.TileWidth;
            frm.HeightSize = _tileset_ctrl.TileHeight;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _tileset_ctrl.RescaleAllTiles((short)frm.WidthSize, (short)frm.HeightSize);
                for (short i = 0; i < _sprite.Images.Count; ++i)
                {
                    _sprite.Images[i].Dispose();
                    _sprite.Images[i] = _tileset_ctrl.Tiles[i].Graphic;
                }
                _sprite.SpriteWidth = (short)frm.WidthSize;
                _sprite.SpriteHeight = (short)frm.HeightSize;
            }
            SpriteDrawer.SetImage((Bitmap)_sprite.GetImage(_selected_frame.Index));
            UpdateControls();
            Modified(null, EventArgs.Empty);
            frm.Dispose();
        }

        void tileset_TileSelected(object sender, EventArgs e)
        {
            _selected_frame.Index = _tileset_ctrl.Selection;
            DirectionHolder.Invalidate(true);
            DirectionHolder.Update();
            SpriteDrawer.SetImage(_tileset_ctrl.GetTile(_tileset_ctrl.Selection).Graphic);
        }

        public override void Destroy()
        {
            _tileset_ctrl.Dispose();
            _sprite.Dispose();
        }

        private void layout_OnFrameClick(object sender, EventArgs e)
        {
            if (_selected_frame != null) _selected_frame.Selected = false;
            _selected_direction = (DirectionLayout)sender;
            _selected_frame = _selected_direction.SelectedFrame;
            SpriteDrawer.SetImage((Bitmap)_sprite.GetImage(_selected_frame.Index));
            FrameBaseEditor.Frame = _selected_frame.Frame;
            DirectionAnim.Direction = _selected_direction.Direction;
            DirectionAnim.Invalidate(true);
        }

        public override void ZoomIn()
        {
            if (_zoom < 8) _zoom++;
            else return;
            UpdateControls();
        }

        public override void ZoomOut()
        {
            if (_zoom > 1) _zoom--;
            else return;
            UpdateControls();
        }

        public void UpdateControls()
        {
            int i = 0;
            DirectionHolder.VerticalScroll.Value = 0;
            foreach (DirectionLayout l in DirectionHolder.Controls)
            {
                l.Zoom = _zoom;
                l.Location = new Point(2, i++ * (l.Height + 2) + 2);
            }
            Refresh();
        }

        public bool CanZoomIn { get { return _zoom < 8; } }
        public bool CanZoomOut { get { return _zoom > 1; } }
        public TilesetControl Tileset
        {
            get { return _tileset_ctrl; }
            set { _tileset_ctrl = value; }
        }

        public void AddNewDirection()
        {
            Direction d = new Direction("Direction_" + DirectionHolder.Controls.Count);
            d.frames.Add(new Frame());
            _sprite.Directions.Add(d);
            DirectionLayout layout = new DirectionLayout(_sprite, d, this);
            layout.OnFrameClick += new System.EventHandler(layout_OnFrameClick);
            layout.Modified += new System.EventHandler(Modified);
            layout.Zoom = _zoom;
            DirectionHolder.Controls.Add(layout);
            layout.Location = new Point(2, DirectionHolder.Controls.Count-1 * (layout.Height + 2) + 2);
            Modified(null, EventArgs.Empty);
        }

        public void RemoveDirection(DirectionLayout layout)
        {
            _sprite.Directions.Remove(layout.Direction);
            DirectionHolder.Controls.Remove(layout);
            LayoutDirections();
            Modified(null, EventArgs.Empty);
        }

        public void LayoutDirections()
        {
            int i = 0;
            foreach (DirectionLayout dl in DirectionHolder.Controls)
                dl.Location = new Point(2, i++ * (dl.Height + 2) + 2);
        }

        private void SpriteDrawer_ImageEdited(object sender, EventArgs e)
        {
            Bitmap img = SpriteDrawer.GetImage();
            _sprite.Images[_selected_frame.Index] = img;
            _tileset_ctrl.Tiles[_selected_frame.Index].Graphic = img;
            Modified(null, EventArgs.Empty);
            Invalidate(true);
        }

        private void Modified(object sender, EventArgs e)
        {
            if (!Parent.Text.EndsWith("*")) Parent.Text += "*";
        }
    }
}
