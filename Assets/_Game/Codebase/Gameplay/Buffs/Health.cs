using System;
using UniRx;
using UnityEngine;

namespace Gameplay.Buffs
{
    public class Health: MonoBehaviour
    {
        public float amount;
        private IDisposable _observable;

        private void OnEnable()
        {
            _observable = this
                .ObserveEveryValueChanged(health => health.amount)
                .Where(health => health <= 0)
                .Subscribe(_ => gameObject.SetActive(false));
        }

        private void OnDisable()
        {
            _observable.Dispose();
        }

        public void ApplyDamage(float damage)
        {
            amount -= damage;
        }
    }
}