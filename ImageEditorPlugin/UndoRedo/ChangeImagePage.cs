using System.Drawing;
using System.Drawing.Drawing2D;

using SphereStudio.Components;
using SphereStudio.Utility;

namespace SphereStudio.UndoRedo
{
    class ChangeImagePage : HistoryPage
    {
        private Image afterImage;
        private Image beforeImage;
        private ImageEditControl editor;
        private Point position;

        public ChangeImagePage(ImageEditControl editor, Point position, Image before, Image after)
        {
            this.editor = editor;
            this.position = position;
            beforeImage = before;
            afterImage = after;
        }

        public override void Dispose()
        {
            beforeImage?.Dispose();
            afterImage?.Dispose();
            beforeImage = null;
            afterImage = null;
        }

        public override void Redo()
        {
            using (var graphics = Graphics.FromImage(editor.EditImage))
            {
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(afterImage, position);
            }
        }

        public override void Undo()
        {
            using (var graphics = Graphics.FromImage(editor.EditImage))
            {
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(beforeImage, position);
            }
        }
    }
}
