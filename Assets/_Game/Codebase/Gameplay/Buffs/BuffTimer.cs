using System;
using Infrastructure.Utils;
using UniRx;
using UnityEngine;

namespace Gameplay.Buffs
{
    public class BuffTimer
    {
        private const float TimerInterval = 0.1f;
        public readonly FloatReactiveProperty RemainingTime = new();
        private readonly IDisposable _disposable;
        public bool Disposed { get; private set; }


        public BuffTimer(float time, Action timerCallback)
        {
            RemainingTime.Value = time;
            
            _disposable = Observable
                .Interval(TimeSpan.FromSeconds(TimerInterval))
                .Where(_ => Time.timeScale > Constants.Epsilon)
                .TakeWhile(_ => RemainingTime.Value > 0f)
                .Subscribe(_ =>
                {
                    RemainingTime.Value -= TimerInterval * Time.timeScale;
                },
                    () =>
                    {
                        timerCallback?.Invoke();
                        Dispose();
                    });
        }


        public void Dispose()
        {
            _disposable.Dispose();
            Disposed = true;
        }
        

        public void AddTime(float f) => 
            RemainingTime.Value += f;
    }
}