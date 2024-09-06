using System;

namespace Infrastructure.FSM.States
{
    [Serializable]
    public struct EpisodeEndPayload
    {
        public enum Endings
        {
            Restart,
            NextLevel,
            MainMenu
        }

        public Endings ending;
        public bool needRestart;
        public bool levelFinished;
    
    }
}