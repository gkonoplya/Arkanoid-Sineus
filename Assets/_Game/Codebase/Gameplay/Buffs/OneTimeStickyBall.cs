using UnityEngine;

namespace Gameplay.Buffs
{
    public class OneTimeStickyBall: StickyBall
    {
        public float startingForce = 5f;

        protected override void Execute(GameObject target, GameObject self)
        {
            base.Execute(target, self);
            savedVelocity = Vector2.up * startingForce;
        }

        protected override void ActivateBall(Rigidbody2D rb)
        {
            base.ActivateBall(rb);
            Destroy(this);
        }
    }
}