using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    [RequireComponent(typeof(CollisionHandler), typeof(Rigidbody2D))]
    public class StuckChecker: MonoBehaviour
    {
        private const float Minfactor = 0.5f;
        private const float StuckDelta = 0.3f;

        [Range(Minfactor, 20f)]
        public float stuckPeriod;
        [Range(Minfactor,5)]
        public float verticalFactor;
        [Range(Minfactor, 5)] 
        public float horizontalFactor;

        private float _horizontalStuckTime;
        private float _verticalStuckTime;
        
        private bool StuckHorizontally => stuckPeriod < _horizontalStuckTime;
        private bool StuckVertically => stuckPeriod < _verticalStuckTime;
        private Rigidbody2D _rb;
        private CollisionHandler _collisionHandler;
        private float RandomDirection => Random.value >= 0.5f ? 1 : -1;

        private void Awake()
        {
            _collisionHandler = GetComponent<CollisionHandler>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable() => 
            _collisionHandler.OnCollisionEnter += ResolveStuck;

        private void OnDisable() => 
            _collisionHandler.OnCollisionEnter -= ResolveStuck;

        private void Update()
        {
            if (Mathf.Abs(_rb.linearVelocity.x) < StuckDelta)
                _horizontalStuckTime += Time.deltaTime;
            else
                _horizontalStuckTime = 0f;

            if (Mathf.Abs(_rb.linearVelocity.y) < StuckDelta)
                _verticalStuckTime += Time.deltaTime;
            else
                _verticalStuckTime = 0f;
        }

        private void ResolveStuck(Collision2D _)
        {
            if (StuckHorizontally)
                RandomizeHorizontal();
            if (StuckVertically)
                RandomizeVertical();
        }

        private void RandomizeVertical()
        {
            _rb.linearVelocity += new Vector2(0, Random.Range(Minfactor, verticalFactor)); //moving only up to prevent unexpected loose
            _verticalStuckTime = 0f;
        }

        private void RandomizeHorizontal()
        {
            _rb.linearVelocity += new Vector2(RandomDirection * Random.Range(Minfactor, horizontalFactor), 0);
            _horizontalStuckTime = 0f;
        }
    }
}