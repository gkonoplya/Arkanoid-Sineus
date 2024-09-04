
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.Buffs
{
    public class Projectile: MonoBehaviour
    {
        public IObjectPool<Projectile> Pool;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<Health>(out var health))
                return;
            gameObject.SetActive(false);
            health.ApplyDamage(1f);
        }
        
    }
}