using System;
using TriInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gameplay.Buffs
{
    public class TimedMonobeh : MonoBehaviour
    {
        public bool HasTimer;

        [ShowIf(nameof(HasTimer))]
        public float RemainingTime;

        private BuffTimer _timer;
        protected Action TimerCallback { get; set; }

        private void Start()
        {
            if (HasTimer)
                SetTimer();
        }

        private void SetTimer()
        {
            Assert.IsTrue(RemainingTime > Constants.Epsilon,
                $"{this}: If \"Has Timer\" is set, then remaining time must be set");
            
            _timer = new BuffTimer(RemainingTime, TimerCallback);
            _timer.RemainingTime.Subscribe(time => RemainingTime = time).AddTo(this);
        }

        public void AddTime(float time) => _timer.RemainingTime.Value += time;
    }
}