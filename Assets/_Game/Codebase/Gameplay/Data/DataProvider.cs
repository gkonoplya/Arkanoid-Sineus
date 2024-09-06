using System;
using UnityEngine;

namespace Gameplay.Data
{
    public class DataProvider: MonoBehaviour
    {
        public float Timescale;
        
        public LevelData levelData;
        public PlayerData playerData;
        public ConstructorData constructorData;

        private void Start()
        {
            Timescale = Time.timeScale;
        }

        private void Update()
        {
            Timescale = Time.timeScale;
        }
    }
}