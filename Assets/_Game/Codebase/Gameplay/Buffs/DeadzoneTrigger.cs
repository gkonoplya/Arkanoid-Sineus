using UnityEngine;

namespace Gameplay.Buffs
{
    public class DeadzoneTrigger: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<Deadzone>(out var deadzone) || !deadzone.enabled)
                return;

            deadzone.RemoveLife();
        }
    }
}