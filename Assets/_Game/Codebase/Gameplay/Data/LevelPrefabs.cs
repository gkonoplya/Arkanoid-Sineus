using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "levelsPrefab", menuName = "StaticData/levels prefab", order = 50)]
    public class LevelPrefabs : ScriptableObject
    {
        public List<GameObject> Prefabs;
    }
}