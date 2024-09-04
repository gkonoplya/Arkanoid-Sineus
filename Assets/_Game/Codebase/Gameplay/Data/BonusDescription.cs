using System;
using Gameplay.Buffs;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "bonusDescription", menuName = "StaticData/BonusDescription", order = 50)]
    public class BonusDescription: ScriptableObject
    {
        public BuffKinds Kind;
        public BonusTargets Target;
        public float Probability;
        public float Duration;

        public Type Type => Kind switch
        {
            BuffKinds.StickyBall => typeof(StickyBall),
            BuffKinds.Expand => typeof(ExpandPaddle),
            /*BuffKinds.Shrink => expr,
            BuffKinds.LaserCanon => expr,
            BuffKinds.TripleBall => expr,
            BuffKinds.FasterBall => expr,
            BuffKinds.SlowerBall => expr,
            BuffKinds.MegaBall => expr,
            BuffKinds.Shield => expr,
            BuffKinds.ExpandBall => expr,
            BuffKinds.None => expr,*/
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}