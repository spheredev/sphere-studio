using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SphereStudio.Base;
using SphereStudio.DocumentViews;

namespace SphereStudio.FileOpeners
{
    class MapFileOpener : INewFileOpener
    {
        public string FileTypeName => "Sphere Map";

        public string[] FileExtensions => new[] { "rmp" };

        public Bitmap FileIcon => Properties.Resources.MapIcon;

        public DocumentView New()
        {
            var mapView = new MapDocumentView();
            return mapView.NewDocument() ? mapView : null;
        }

        public DocumentView Open(string fileName)
        {
            var mapView = new MapDocumentView();
            mapView.Load(fileName);
            return mapView;
        }
    }
}
