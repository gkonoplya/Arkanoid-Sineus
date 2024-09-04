using System;
using Gameplay.Data;
using UnityEngine;
using Zenject;

namespace Gameplay.Buffs
{
    public class BuffFactory
    {
        private readonly IInstantiator _iInstatiator;
        private readonly LevelData _levelData;

        public BuffFactory(IInstantiator iInstatiator, LevelData levelData)
        {
            _iInstatiator = iInstatiator;
            _levelData = levelData;
        }

        public TimedMonobeh AddBuff(BonusDescription description)
        {
            var component = _iInstatiator.InstantiateComponent(description.Type, GetTarget(description.Target)) as TimedMonobeh;
            component.InitTimer(description.Duration);
            return component;
        }

        private GameObject GetTarget(BonusTargets descriptionTarget) =>
            descriptionTarget switch
            {
                BonusTargets.Paddle => _levelData.Paddle,
                BonusTargets.Ball => _levelData.Ball,
                _ => throw new ArgumentOutOfRangeException(nameof(descriptionTarget), descriptionTarget, null)
            };
    }
}