using System;
using Infrastructure.InputService;
using Infrastructure.Utils;
using UnityEngine;
using Zenject;

namespace Gameplay.Paddle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove: MonoBehaviour
    {
        private IInputService _inputService;
        private Rigidbody2D _rigidbody;
        public float moveSpeed;
        private bool _collided;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
         if (_inputService.Active && Mathf.Abs(_inputService.Axis.x) > Constants.Epsilon
             && !_collided) 
             _rigidbody.linearVelocityX = _inputService.Axis.x * moveSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
           if (!other.gameObject.CompareTag("Wall"))
               return;
           _collided = true;
           _rigidbody.linearVelocityX = other.contacts[0].normal.x * moveSpeed;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _collided = false;
        }
    }
}