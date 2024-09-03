using UnityEngine;

namespace Gameplay.Buffs
{
    public class DeadzoneTrigger: CollisionBuff
    {
        public override void Execute(GameObject target, GameObject self)
        {
            if (!target.TryGetComponent<Deadzone>(out var deadzone) || !deadzone.enabled)
                return;

            deadzone.RemoveLife();
        }
    }
}