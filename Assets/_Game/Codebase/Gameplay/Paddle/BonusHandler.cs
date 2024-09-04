using Gameplay.Buffs;
using UnityEngine;

namespace Gameplay.Paddle
{
    public class BonusHandler: MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<Bonus>(out var bonus)) 
                return;
            
            bonus.ApplyBonus();
            Destroy(bonus.gameObject,0.1f);
        }
    }
}