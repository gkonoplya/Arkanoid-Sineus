using UnityEngine;

namespace Gameplay.Buffs
{
    public class DamageDealer: CollisionBuff
    {
        public float amount;

        protected override void Execute(GameObject target, GameObject self)
        {
            if (!target.TryGetComponent<Health>(out var health))
                return;

            health.ApplyDamage(amount);
        }
    }
}