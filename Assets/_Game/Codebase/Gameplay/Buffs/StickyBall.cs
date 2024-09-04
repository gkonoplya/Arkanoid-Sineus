using Infrastructure.InputService;
using TriInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Buffs
{
    public class StickyBall : TimedMonobeh
    {
        [ReadOnly]
        public Vector2 savedVelocity;
        private IInputService _inputService;
        private Rigidbody2D _rb;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        protected void Awake()
        {
            _inputService
                .IsAttackButtonPressedReactive
                .SkipLatestValueOnSubscribe()
                .Where(_ => _inputService.Active)
                .Where(x => x)
                .TakeWhile(_ => this)
                .Subscribe(_ => ActivateBall(_rb))
                .AddTo(this);
            TimerCallback = Dispose;
        }

        private void Dispose()
        {
            if (!this)
                return;
            
            if (_rb !=null && !_rb.simulated)
            {
                ActivateBall(_rb);
            }
            Destroy(this);
        }

        protected void OnCollisionEnter2D(Collision2D other) =>
            Execute(
                other.collider.gameObject, 
                other.otherCollider.gameObject);

        protected virtual void Execute(GameObject target, GameObject self)
        {
            if (!target.TryGetComponent<Paddle.Paddle>(out var paddle) || !this) 
                return;
            
            _rb = self.GetComponent<Rigidbody2D>();
            savedVelocity = _rb.linearVelocity;
            _rb.simulated = false;

            Vector3 offset = self.transform.position - paddle.transform.position;
            paddle.transform
                .ObserveEveryValueChanged(tr => tr.position)
                .Where(_ => !_rb.simulated)
                .Subscribe(position => self.transform.position = position + offset)
                .AddTo(this);
        }

        protected virtual void ActivateBall(Rigidbody2D rb) =>
              SetActive(rb);

        private void SetActive(Rigidbody2D rb)
        {
            rb.simulated = true;
            rb.linearVelocity = savedVelocity;
        }
    }
}