using System;
using Gameplay.Buffs;
using Infrastructure.Utils;
using UnityEngine;

namespace Gameplay
{
    public class BallBouncer: MonoBehaviour
    {
        public float paddleAcceleration = 0.2f;
        
        private Rigidbody2D _rb;
        private bool _collided;
        private CollisionHandler _collisionHandler;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collisionHandler = GetComponent<CollisionHandler>();
        }

        private void OnEnable()
        {
            _collisionHandler.OnCollisionEnter += ResolveVelocity;
        }

        private void OnDisable()
        {
            _collisionHandler.OnCollisionEnter -= ResolveVelocity;
        }

        private void ResolveVelocity(Collision2D other)
        {
            if (TryGetComponent<StickyBall>(out _))
            {
                return;
            }

            var newVelocity = Vector2.Reflect(_rb.linearVelocity, other.contacts[0].normal);

            if (other.gameObject.TryGetComponent<Paddle.Paddle>(out _))
            {
                float relatedLinearVelocity = Mathf.Abs(other.rigidbody.linearVelocityX) < Constants.Epsilon 
                    ? newVelocity.x : other.rigidbody.linearVelocityX;
                
                newVelocity = new Vector2(relatedLinearVelocity, newVelocity.y);
            }

            Debug.Log($"Collided with {other.gameObject}, in velocity {_rb.linearVelocity}, new velocitt {newVelocity}", this);
            _rb.linearVelocity = newVelocity;
        }
        
    }
}