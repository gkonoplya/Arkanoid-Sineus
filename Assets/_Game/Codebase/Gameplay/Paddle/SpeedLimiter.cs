using UniRx;
using UnityEngine;

namespace Gameplay.Paddle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpeedLimiter: MonoBehaviour
    {
        [Range(1f, 100f)]
        public float minSpeed = 1f;
        [Range(1f, 100f)]
        public float maxSpeed = 100f;

        public bool debugInfo = false;
        public float currentSpeed;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();

            _rb.ObserveEveryValueChanged(rb => rb.linearVelocity.magnitude)
                .Where(magnitude => magnitude < minSpeed || magnitude > maxSpeed)
                .Subscribe(_ => LimitSpeed())
                .AddTo(this);
        }

        private void Update()
        {
            if (!debugInfo) return;
            currentSpeed = _rb.linearVelocity.magnitude;
        }

        private void LimitSpeed()
        {
            float speed = _rb.linearVelocity.magnitude;
            float limitingFactor = 1f;
            
            if (speed < minSpeed) 
                limitingFactor = Mathf.Approximately(speed, 0f) ? minSpeed : minSpeed / speed;
            else if (speed > maxSpeed) 
                limitingFactor = maxSpeed / speed;

            _rb.linearVelocity = limitingFactor * _rb.linearVelocity;
        }
    }
}