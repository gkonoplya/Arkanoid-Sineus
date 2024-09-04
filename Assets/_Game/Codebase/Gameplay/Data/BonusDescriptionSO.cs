using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "bonusDescription", menuName = "StaticData/BonusDescription", order = 50)]
    public class BonusDescriptionSO: ScriptableObject
    {
        public BonusDescription description;
    }
}