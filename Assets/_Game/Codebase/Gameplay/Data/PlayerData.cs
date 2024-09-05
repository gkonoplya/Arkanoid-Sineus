using System;
using UniRx;

namespace Gameplay.Data
{
    [Serializable]
    public class PlayerData
    {
        public string currentLevelName;
        public IntReactiveProperty scores;
        public IntReactiveProperty lives;
    }
}