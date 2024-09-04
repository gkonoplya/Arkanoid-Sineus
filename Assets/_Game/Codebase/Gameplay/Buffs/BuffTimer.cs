﻿using System;
using UniRx;

namespace Gameplay.Buffs
{
    public class BuffTimer
    {
        private const float TimerInterval = 0.1f;
        public readonly FloatReactiveProperty RemainingTime = new();
        private readonly IDisposable _disposable;

        public BuffTimer(float time, Action timerCallback)
        {
            RemainingTime.Value = time;
            
            _disposable = Observable
                .Interval(TimeSpan.FromSeconds(TimerInterval))
                .TakeWhile(_ => RemainingTime.Value > 0f)
                .Subscribe(_ =>
                {
                    RemainingTime.Value -= TimerInterval;
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
        }
        

        public void AddTime(float f) => 
            RemainingTime.Value += f;
    }
}