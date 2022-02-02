using System.Drawing;

using SphereStudio.Base;

namespace SphereStudio.FileOpeners
{
    class WindowStyleOpener : INewFileOpener
    {
        public string FileTypeName => "Sphere Window Style";

        public string[] FileExtensions => new[] { "rws" };

        public Bitmap FileIcon => Properties.Resources.GridToolIcon;

        public DocumentView New()
        {
            var documentView = new WindowStyleView();
            return documentView.NewDocument() ? documentView : null;
        }

        public DocumentView Open(string fileName)
        {
            var documentView = new WindowStyleView();
            documentView.Load(fileName);
            return documentView;
        }
    }
}
