using UnityEngine;

namespace Gameplay.Buffs
{
    public class DeadzoneTrigger: MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.TryGetComponent<Deadzone>(out var deadzone) || !deadzone.enabled)
                return;

            deadzone.RemoveLife();
        }
    }
}