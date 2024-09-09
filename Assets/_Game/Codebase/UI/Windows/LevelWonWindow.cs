using Infrastructure.FSM.States;
using Infrastructure.StateMachine;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class LevelWonWindow: WindowBase
    {
        public Button NextLevelButton;
        private LazyInject<LevelStateMachine> _gameFsm;

        [Inject]
        public void Construct(LazyInject<LevelStateMachine> gameFsm)
        {
            _gameFsm = gameFsm;
        }

        protected override void Initialize()
        {
            base.Initialize();
            NextLevelButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => _gameFsm.Value.Enter<EpisodeEnd, EpisodeEndPayload>(new()
                {
                    ending = EpisodeEndPayload.Endings.NextLevel,
                    levelFinished = true
                }))
                .AddTo(this);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _gameFsm.Value.Enter<EpisodeEnd, EpisodeEndPayload>(new()
            {
                ending = EpisodeEndPayload.Endings.MainMenu,
                levelFinished = true
            });
        }
    }
}