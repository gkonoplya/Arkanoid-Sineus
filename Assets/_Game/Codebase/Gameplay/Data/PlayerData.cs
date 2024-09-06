using System;
using UniRx;

namespace Gameplay.Data
{
    [Serializable]
    public class PlayerData
    {
        public IntReactiveProperty scores;
        public IntReactiveProperty lives;
        public bool LevelCompleted;
        public int currentLevelIndex;

        public static PlayerData Default()
        {
            return new PlayerData()
            {
                LevelCompleted = false,
                currentLevelIndex = 1,
                scores = new IntReactiveProperty(0),
                lives = new IntReactiveProperty(3)
            };
        }
    }
}