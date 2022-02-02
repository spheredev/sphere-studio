using IrrKlang;

namespace SphereStudio.MusicPlayers
{
    class IrrPlayer : IPlayer
    {
        private ISoundEngine engine = new ISoundEngine();
        private ISound stream;

        public IrrPlayer(string filename, bool wantRepeat)
        {
            stream = engine.Play2D(filename, wantRepeat, true);
        }

        public void Dispose()
        {
            stream.Dispose();
            engine.Dispose();
        }

        public bool Paused
        {
            get => stream.Paused;
            set => stream.Paused = value;
        }

        public uint Length
        {
            get { return stream.PlayLength; }
        }
        
        public uint Position
        {
            get { return stream.PlayPosition; }
            set { stream.PlayPosition = value; }
        }
        
        public void Play()
        {
            stream.Paused = false;
        }

        public void PlayOrPause()
        {
            stream.Paused = !stream.Paused;
        }
        
        public void Pause()
        {
            stream.Paused = true;
        }
        
        public void Stop()
        {
            stream.Stop();
        }
    }
}
