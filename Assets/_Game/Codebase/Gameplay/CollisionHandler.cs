using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionHandler : MonoBehaviour
    {
        public event Action<Collision2D> OnCollisionEnter;
        
        private void OnCollisionEnter2D(Collision2D other) => 
            OnCollisionEnter?.Invoke(other);
    }
}