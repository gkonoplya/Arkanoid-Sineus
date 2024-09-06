using System;
using UnityEngine;

namespace Gameplay.Data
{
    [Serializable]
    public class LevelData
    {
        public GameObject Paddle;
        public GameObject Ball;
        public int LevelIndex;
        public int Highscore;
        public bool LevelFinished;
        public bool  NeedRestart;
    }
}