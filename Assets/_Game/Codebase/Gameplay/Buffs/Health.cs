using System;
using Infrastructure.Utils;
using UniRx;
using UnityEngine;

namespace Gameplay.Buffs
{
    public class Health: MonoBehaviour
    {
        public FloatReactiveProperty amount = new();
        public float MaxHealth { get; set; }
        private IDisposable _observable;

        private void OnEnable()
        {
            MaxHealth = amount.Value;
            _observable = amount
                .Where(health => health <= Constants.Epsilon)
                .Subscribe(_ => gameObject.SetActive(false));
        }

        private void OnDisable()
        {
            _observable.Dispose();
        }

        public void ApplyDamage(float damage)
        {
            amount.Value -= damage;
        }
    }
}