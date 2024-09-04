using UnityEngine;

namespace Gameplay.Buffs
{
    public class DamageDealer: MonoBehaviour
    {
        public float amount;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.TryGetComponent<Health>(out var health))
                return;

            health.ApplyDamage(amount);
        }
    }
}