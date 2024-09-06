using Infrastructure.FSM;
using Infrastructure.FSM.States;
using Infrastructure.StateMachine;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LevelMenuWindow : WindowBase
    {
        public Button menuButton;
        public Button helpButton;
        private LazyInject<LevelStateMachine> _gameFSM;
        private LevelPresenter _levelPresenter;

        [Inject]
        public void Construct(LazyInject<LevelStateMachine> gameFSM, LevelPresenter levelPresenter)
        {
            _gameFSM = gameFSM;
            _levelPresenter = levelPresenter;
        }

        protected override void Initialize()
        {
            base.Initialize();
            menuButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => _gameFSM.Value.Enter<EpisodeEnd, EpisodeEndPayload>(new()
                {
                    ending = EpisodeEndPayload.Endings.MainMenu,
                    levelFinished = false
                }))
                .AddTo(this);

            helpButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => _levelPresenter.ShowHelpWindow());
        }

        public void Close() => Cleanup();
    }
}