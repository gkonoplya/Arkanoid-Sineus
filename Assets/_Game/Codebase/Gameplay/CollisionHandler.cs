using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionHandler : MonoBehaviour
    {
        private bool _collided;
        public event Action<Collision2D> OnCollisionEnter;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collided)
                return;
            
            _collided = true;
            OnCollisionEnter?.Invoke(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _collided = false;
        }
    }
}