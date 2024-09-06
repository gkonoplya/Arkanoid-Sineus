using Infrastructure.FSM;
using Infrastructure.FSM.States;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LevelMenuWindow : WindowBase
    {
        public Button menuButton;
        public Button helpButton;
        private GameFSM _gameFSM;
        private LevelPresenter _levelPresenter;

        [Inject]
        public void Construct(GameFSM gameFSM, LevelPresenter levelPresenter)
        {
            _gameFSM = gameFSM;
            _levelPresenter = levelPresenter;
        }

        protected override void Initialize()
        {
            base.Initialize();
            menuButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => _gameFSM.Enter<MainMenu>())
                .AddTo(this);

            helpButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => _levelPresenter.ShowHelpWindow());
        }

        public void Close() => Cleanup();
    }
}