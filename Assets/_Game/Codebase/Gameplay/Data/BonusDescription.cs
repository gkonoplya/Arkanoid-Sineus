using System;
using Gameplay.Buffs;

namespace Gameplay.Data
{
    [Serializable]
    public class BonusDescription
    {
        public BuffKinds Kind;
        public BonusTargets Target;
        public float Probability;
        public float Duration;

        public Type Type => Kind switch
        {
            BuffKinds.StickyBall => typeof(StickyBall),
            BuffKinds.Expand => typeof(ExpandPaddle),
            //BuffKinds.Shrink => expr,
            BuffKinds.LaserCanon => typeof(LaserCanon),
            /*BuffKinds.TripleBall => expr,
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