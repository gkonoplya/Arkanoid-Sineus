using System;
using UnityEngine;

namespace Gameplay.Level
{
    [Serializable]
    public struct BlockData
    {
        public BlockType type;
        public float? currentHealth;
        public float maxHealth;
        public Color Color;
        public Vector2 Position;
    }
}