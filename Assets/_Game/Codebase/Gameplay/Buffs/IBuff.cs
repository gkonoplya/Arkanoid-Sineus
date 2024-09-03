using UnityEngine;

namespace Gameplay.Buffs
{
    public interface IBuff
    {
        void Execute(GameObject target, GameObject self);
    }
}