using System.Drawing;

using SphereStudio.Base;
using SphereStudio.DocumentViews;

namespace SphereStudio.FileOpeners
{
    class SphereFontOpener : INewFileOpener
    {
        public string FileTypeName => "Sphere Font";

        public string[] FileExtensions => new[] { "rfn" };

        public Bitmap FileIcon => Properties.Resources.style;

        public DocumentView New()
        {
            var fontView = new FontImporterView();
            return fontView.NewDocument() ? fontView : null;
        }

        public DocumentView Open(string fileName)
        {
            var fontView = new FontImporterView();
            fontView.Load(fileName);
            return fontView;
        }
    }
}
