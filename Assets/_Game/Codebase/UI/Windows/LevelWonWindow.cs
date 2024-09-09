using Gameplay.Data;
using Infrastructure.FSM.States;
using Infrastructure.StateMachine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class LevelWonWindow: WindowBase
    {
        public Button NextLevelButton;
        private LazyInject<LevelStateMachine> _gameFsm;
        private LevelPrefabs _levelPrefabs;
        private DataProvider _dataProvider;
        public TMP_Text wonText;

        [Inject]
        public void Construct(LazyInject<LevelStateMachine> gameFsm, LevelPrefabs levelPrefabs, DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _levelPrefabs = levelPrefabs;
            _gameFsm = gameFsm;
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (_levelPrefabs.Prefabs.Count == _dataProvider.playerData.currentLevelIndex)
            {
                NextLevelButton.gameObject.SetActive(false);
                wonText.text =
                    $"You won!!!\r\n Congratulations!\r\n Your score: {_dataProvider.playerData.scores.Value.ToString()}";
            }
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