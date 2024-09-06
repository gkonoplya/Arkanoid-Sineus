using Infrastructure.FSM;
using Infrastructure.FSM.States;
using UniRx;
using UnityEngine.UI;

namespace UI.Windows
{
    public class LevelWonWindow: WindowBase
    {
        public Button NextLevelButton;
        private GameFSM _gameFsm;

        protected override void Initialize()
        {
            base.Initialize();
            NextLevelButton.OnClickAsObservable()
                .First()
                .Subscribe(_ => _gameFsm.Enter<Level>())
                .AddTo(this);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _gameFsm.Enter<MainMenu>();
        }
    }
}