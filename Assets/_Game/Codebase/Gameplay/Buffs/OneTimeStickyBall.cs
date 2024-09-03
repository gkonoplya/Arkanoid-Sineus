using UnityEngine;

namespace Gameplay.Buffs
{
    public class OneTimeStickyBall: StickyBall
    {
        public float startingForce = 5f;
        private Rigidbody2D _paddleRb;

        protected override void Execute(GameObject target, GameObject self)
        {
            _paddleRb = target.GetComponent<Rigidbody2D>();
            base.Execute(target, self);
        }

        protected override void ActivateBall(Rigidbody2D rb)
        {
            savedVelocity = new Vector2(_paddleRb.linearVelocity.x, startingForce);
            base.ActivateBall(rb);
            Destroy(this);
        }
    }
}