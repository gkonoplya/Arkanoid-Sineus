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

        public BuffFactory(IInstantiator iInstatiator, DataProvider data)
        {
            _iInstatiator = iInstatiator;
            _levelData = data.levelData;
        }

        public TimedMonobeh AddBuff(BonusDescription description)
        {
            GameObject target = GetTarget(description.Target);

            if (target.TryGetComponent(description.Type, out var bonusComponent))
            {
                var bonus = bonusComponent as TimedMonobeh;
                bonus.AddTime(description.Duration);
                return bonus;
            }
            
            var component = _iInstatiator.InstantiateComponent(description.Type, target) as TimedMonobeh;
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