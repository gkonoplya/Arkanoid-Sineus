using Infrastructure.InputService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Buffs
{
    public class StickyBall : CollisionBuff
    {
        public Vector2 savedVelocity;
        private IInputService _inputService;
        private Rigidbody2D _rb;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        protected override void Awake()
        {
            base.Awake();
            _inputService
                .IsAttackButtonPressedReactive
                .SkipLatestValueOnSubscribe()
                .Where(_ => _inputService.Active)
                .Where(x => x)
                .Subscribe(_ => ActivateBall(_rb))
                .AddTo(disposable);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_rb !=null && !_rb.simulated)
            {
                SetActive(_rb);
            }
        }

        protected override void Execute(GameObject target, GameObject self)
        {
            if (!target.TryGetComponent<Paddle>(out var paddle)) 
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