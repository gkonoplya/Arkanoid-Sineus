using System;
using UniRx;
using UnityEngine;

namespace Gameplay.Buffs
{
    public abstract class TimedBuff: MonoBehaviour
    {
        public float destroyInterval;
        private protected CompositeDisposable disposable = new();

        protected virtual void Awake()
        {
            Observable
                .Interval(TimeSpan.FromSeconds(destroyInterval))
                .Subscribe(_ => Destroy(this))
                .AddTo(disposable);
        }

        protected virtual void OnDestroy()
        {
            disposable.Dispose();
        }

        protected abstract void Execute(GameObject target, GameObject self);
    }
}