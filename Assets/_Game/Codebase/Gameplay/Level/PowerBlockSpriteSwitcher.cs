using Gameplay.Buffs;
using Infrastructure.Utils;
using UniRx;
using UnityEngine;

namespace Gameplay.Level
{
    public class PowerBlockSpriteSwitcher: BlockSpriteSwitcher
    {
        private float _maxHealth;
        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            var health = GetComponent<Health>();
            _maxHealth = health.MaxHealth;

            health.amount
                .TakeWhile(x => x > Constants.Epsilon)
                .Subscribe(ChangeSprite)
                .AddTo(this);
        }

        private void ChangeSprite(float currentHealth)
        {
            int index = Mathf.CeilToInt(currentHealth / _maxHealth * Sprites.Count) - 1;
            _renderer.sprite = Sprites[index];
        }
    }
}