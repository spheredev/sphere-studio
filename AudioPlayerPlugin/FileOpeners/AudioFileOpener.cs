using System.Drawing;

using SphereStudio.Base;
using SphereStudio.UI;

namespace SphereStudio.FileOpeners
{
    class AudioFileOpener : IFileOpener
    {
        AudioPlayerPane playerPane;

        public AudioFileOpener(AudioPlayerPane playerPane)
        {
            this.playerPane = playerPane;
        }

        public string FileTypeName => "Audio File";

        public Bitmap FileIcon => Properties.Resources.Icon;

        public string[] FileExtensions => new[]
        {
            "mp3", "ogg", "flac",      // compressed audio formats
            "mod", "it", "s3d", "s3m", // tracker formats
            "wav"                      // uncompressed/PCM formats
        };

        public DocumentView Open(string fileName)
        {
            playerPane.Play(fileName);
            return null;
        }
    }
}
