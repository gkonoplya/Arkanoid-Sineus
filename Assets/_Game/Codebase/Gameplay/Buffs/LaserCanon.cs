using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Buffs
{
    public class LaserCanon: TimedMonobeh
    {
        private ProjectileFactory _projectileFactory;

        [Inject]
        public void Construct(ProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
            Fire();
        }

        private void Awake()
        {
            Observable
                .Interval(TimeSpan.FromSeconds(0.5f))
                .Where(_ => Time.timeScale > 0)
                .TakeWhile(_ => this)
                .Subscribe(_ => Fire())
                .AddTo(this);
            TimerCallback = () => Destroy(this);
        }

        private void Fire()
        {
            _projectileFactory.Create(transform.position);
        }
    }
}