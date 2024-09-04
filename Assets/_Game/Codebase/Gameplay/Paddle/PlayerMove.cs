using Infrastructure.InputService;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove: MonoBehaviour
    {
        private IInputService _inputService;
        private Rigidbody2D _rigidbody;
        public float moveSpeed;

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
            if (_inputService.Active && Mathf.Abs(_inputService.Axis.x) > Constants.Epsilon )
            {
                _rigidbody.linearVelocityX = _inputService.Axis.x * moveSpeed;
            }
        }
    }
}