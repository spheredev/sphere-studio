using System.Drawing;

using SphereStudio.Components;
using SphereStudio.Utility;

namespace SphereStudio.UndoRedo
{
    class ResizeImagePage : HistoryPage
    {
        Bitmap afterImage;
        Bitmap beforeImage;
        ImageEditControl editor;

        public ResizeImagePage(ImageEditControl editor, Image before, Image after)
        {
            this.editor = editor;
            beforeImage = new Bitmap(before);
            afterImage = new Bitmap(after);
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
            editor.SetImage(afterImage);
        }

        public override void Undo()
        {
            editor.SetImage(beforeImage);
        }
    }
}
