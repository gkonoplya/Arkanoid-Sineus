using Gameplay.Data;
using Infrastructure.InputService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Buffs
{
    public class StartingBall: TimedMonobeh
    {
        public float startingForce = 5f;
        private Rigidbody2D _paddleRb;
        private DataProvider _dataProvider;
        private Rigidbody2D _ballRb;
        private IInputService _inputService;
        private CompositeDisposable _disposable = new();

        [Inject]
        public void Construct(DataProvider dataProvider, IInputService inputService)
        {
            _inputService = inputService;
            _dataProvider = dataProvider;
        }
        
        private void Awake()
        {
            _paddleRb = _dataProvider.levelData.Paddle.GetComponent<Rigidbody2D>();
            _ballRb = GetComponent<Rigidbody2D>();
            TimerCallback = ActivateBall;

            _inputService.IsAttackButtonPressedReactive
                .SkipLatestValueOnSubscribe()
                .First()
                .Subscribe(_ => ActivateBall())
                .AddTo(_disposable);
            
            Vector3 offset = transform.position - _paddleRb.transform.position;
            _paddleRb.transform
                .ObserveEveryValueChanged(tr => tr.position)
                .TakeWhile(_ => this || enabled)
                .Subscribe(position => transform.position = position + offset)
                .AddTo(_disposable);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _disposable.Dispose();
        }

        private void ActivateBall()
        { 
            _ballRb.linearVelocity = Mathf.Approximately(_paddleRb.linearVelocity.x, 0f) 
                  ? new Vector2(1f, startingForce)
                  : new Vector2(_paddleRb.linearVelocity.x, startingForce);
            Destroy(this);
        }
    }
}