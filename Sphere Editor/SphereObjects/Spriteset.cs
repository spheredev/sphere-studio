﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Sphere_Editor.Bitmaps;

namespace Sphere_Editor.SphereObjects
{
    public class Spriteset : IDisposable
    {
        private short version = 3;
        private short frame_width = 16, frame_height = 32;

        private Base sprite_base = new Base();
        private List<Direction> directions = new List<Direction>();
        private List<Bitmap> images = new List<Bitmap>();

        public Spriteset() { }

        /// <summary>
        /// Populates this spriteset with default info.
        /// </summary>
        public void MakeNew()
        {
            this.images.Add(new Bitmap(frame_width, frame_height));
            directions.Add(new Direction("north"));
            directions.Add(new Direction("northeast"));
            directions.Add(new Direction("east"));
            directions.Add(new Direction("southeast"));
            directions.Add(new Direction("south"));
            directions.Add(new Direction("southwest"));
            directions.Add(new Direction("west"));
            directions.Add(new Direction("northwest"));
            foreach (Direction d in directions) d.frames.Add(new Frame());
        }

        private void Purge()
        {
            version = 3;
            frame_width = frame_height = 0;
            sprite_base = new Base();
            directions.Clear();
            foreach (Bitmap b in images) b.Dispose();
            images.Clear();
        }

        public bool Load(String filename)
        {
            if (!File.Exists(filename)) return false;
            using (BinaryReader stream = new BinaryReader(File.OpenRead(filename)))
            {

                // purge anything already inside this object:
                this.Purge();

                // start reading the header //
                string sig = new string(stream.ReadChars(4));
                if (sig != ".rss") return false;

                version = stream.ReadInt16();
                short num_images = stream.ReadInt16();
                frame_width = stream.ReadInt16();
                frame_height = stream.ReadInt16();
                short num_dirs = stream.ReadInt16();

                // read sprite base //
                sprite_base.x1 = stream.ReadInt16();
                sprite_base.y1 = stream.ReadInt16();
                sprite_base.x2 = stream.ReadInt16();
                sprite_base.y2 = stream.ReadInt16();

                // reserved //
                stream.ReadBytes(106);
                switch (version)
                {
                    case 3:
                        BitmapLoader loader = new BitmapLoader(frame_width, frame_height);
                        int amt = frame_width * frame_height * 4;
                        while (num_images-- > 0) images.Add(loader.LoadFromStream(stream, amt));
                        loader.Close();

                        short num_frames;
                        string name;
                        short length;
                        while (num_dirs-- > 0)
                        {
                            num_frames = stream.ReadInt16();
                            stream.ReadBytes(6);
                            length = stream.ReadInt16();
                            name = new String(stream.ReadChars(length)).Substring(0, length - 1);
                            Direction dir = new Direction(name);
                            Frame f;
                            while (num_frames-- > 0)
                            {
                                f = new Frame();
                                f.Index = stream.ReadInt16();
                                f.Delay = stream.ReadInt16();
                                stream.ReadBytes(4);
                                dir.frames.Add(f);
                            }
                            directions.Add(dir);
                        }
                        break;
                }
            }
            return true;
        }

        public void Save(string filename)
        {
            using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(filename)))
            {
                // save header data:
                writer.Write(".rss".ToCharArray());
                writer.Write(version);
                writer.Write((short)images.Count);
                writer.Write(frame_width);
                writer.Write(frame_height);
                writer.Write((short)directions.Count);

                // save the sprite base:
                writer.Write(sprite_base.x1);
                writer.Write(sprite_base.y1);
                writer.Write(sprite_base.x2);
                writer.Write(sprite_base.y2);

                //reserved:
                writer.Write(new byte[106]);

                switch (version)
                {
                    case 3:
                        BitmapSaver saver = new BitmapSaver(frame_width, frame_height);
                        for (short i = 0; i < images.Count; ++i) saver.SaveToStream(Images[i], writer);
                        foreach (Direction d in directions)
                        {
                            writer.Write((short)d.frames.Count);
                            writer.Write(new byte[6]);
                            writer.Write((short)(d.Name.Length + 1));
                            writer.Write((d.Name + "\0").ToCharArray());
                            foreach (Frame f in d.frames)
                            {
                                writer.Write(f.Index);
                                writer.Write(f.Delay);
                                writer.Write(new byte[4]);
                            }
                        }
                        break;
                }

                writer.Flush();
            }
        }

        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (images != null)
                    {
                        foreach (Bitmap b in images) b.Dispose();
                        images.Clear();
                    }
                }

                images = null;
            }
            _disposed = true;
        }

        /// <summary>
        /// Removes the frame from the spriteset and its image array.
        /// </summary>
        /// <param name="reference"></param>
        public void RemoveFrameReference(int reference)
        {
            foreach (Direction d in directions)
            {
                foreach (Frame f in d.frames)
                {
                    if (f.Index == reference) f.Index = 0;
                    if (f.Index > reference) f.Index--;
                }
            }
            images.RemoveAt(reference);
        }

        public Image GetImage(string direction, int num)
        {
            Direction dir = null;
            foreach (Direction d in directions)
            {
                if (d.Name.Equals(direction)) { dir = d; break; }
            }
            if (dir == null) return null;
            return images[dir.frames[num].Index];
        }

        public short SpriteWidth
        {
            get { return frame_width; }
            set { frame_width = value; }
        }

        public short SpriteHeight
        {
            get { return frame_height; }
            set { frame_height = value; }
        }

        public Image GetImage(int index)
        {
            return images[index];
        }

        public Base SpriteBase
        {
            get { return sprite_base; }
            set { sprite_base = value; }
        }

        public List<Direction> Directions
        {
            get { return this.directions; }
            set { this.directions = value; }
        }

        public List<Bitmap> Images
        {
            get { return this.images; }
            set
            {
                this.images = value;
                this.frame_height = (short)value[0].Height;
                this.frame_width  = (short)value[0].Width;
            }
        }

        // returns an array with the names of each direction:
        public string[] GetDirections()
        {
            string[] dirs = new string[directions.Count];
            for (int i = 0; i < directions.Count; ++i)
                dirs[i] = directions[i].Name;
            return dirs;
        }
    }

    /// <summary>
    /// Represents the base to a spriteset object:
    /// </summary>
    public class Base
    {
        public short x1 = 0, y1 = 15;
        public short x2 = 15, y2 = 31;

        public int Height
        {
            get { return (int)(y2 - y1); }
        }

        public int Width
        {
            get { return (int)(x2 - x1); }
        }

        /// <summary>
        /// Returns the base as a .NET retcangle object. Or constructs a base from a .NET rectangle.
        /// </summary>
        public Rectangle Rectangle
        {
            get { return new Rectangle(x1, y1, Width, Height); }
            set
            {
                x1 = (short)value.X;
                y1 = (short)value.Y;
                x2 = (short)(value.X + value.Width);
                y2 = (short)(value.Y + value.Height);
            }
        }
    }

    public class Frame
    {
        private short index = 0;
        private short delay = 8;

        public Frame() {}

        public short Delay
        {
            get { return delay; }
            set { delay = value; }
        }

        public short Index
        {
            get { return index; }
            set { index = value; }
        }
    }

    public class Direction
    {
        public List<Frame> frames = new List<Frame>();
        private string name;

        public Direction(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
