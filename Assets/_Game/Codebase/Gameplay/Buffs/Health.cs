using System;
using Infrastructure.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Buffs
{
    public class Health: MonoBehaviour
    {
        public FloatReactiveProperty amount = new();
        public float MaxHealth { get; set; }
        private IDisposable _observable;
        private ScoreWatch _scoreWatch;

        [Inject]
        public void Construct(ScoreWatch scoreWatch)
        {
            _scoreWatch = scoreWatch;
        }

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
            _scoreWatch.ReportDamage(damage);
        }
    }
}