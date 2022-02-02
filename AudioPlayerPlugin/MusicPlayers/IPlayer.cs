using System;

namespace SphereStudio.MusicPlayers
{
    interface IPlayer : IDisposable
    {
        bool Paused { get; set; }
        uint Position { get; set; }
        uint Length { get; }
        
        void Play();
        void PlayOrPause();
        void Pause();
        void Stop();
    }
}
