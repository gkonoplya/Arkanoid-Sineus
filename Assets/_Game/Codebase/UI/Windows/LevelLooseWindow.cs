using Gameplay.Data;
using Infrastructure.FSM.States;
using Infrastructure.StateMachine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class LevelLooseWindow: WindowBase
    {
        public TMP_Text StatisticsText;
        public Button NextLevelButton;
        private LazyInject<LevelStateMachine> _gameFsm;
        private DataProvider _dataProvider;
        
        [Inject]
        public void Construct(DataProvider dataProvider, LazyInject<LevelStateMachine> gameFsm)
        {
            _dataProvider = dataProvider;
            _gameFsm = gameFsm;
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            
            StatisticsText.text = _dataProvider.levelData.Highscore > _dataProvider.playerData.scores.Value
                ? $"Your score: {_dataProvider.playerData.scores}"
                : $"Congratulations!!!\r\n New highscore: {_dataProvider.playerData.scores.Value}";

            _dataProvider.playerData.scores.Value = 0;
            
            NextLevelButton.OnClickAsObservable()
                .First()
                .Subscribe(_ =>
                {
                    _gameFsm.Value.Enter<EpisodeEnd, EpisodeEndPayload>(new EpisodeEndPayload()
                    {
                        needRestart = true,
                        ending = EpisodeEndPayload.Endings.Restart
                    });
                })
                .AddTo(this);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _gameFsm.Value.Enter<EpisodeEnd, EpisodeEndPayload>(new()
            {
                ending = EpisodeEndPayload.Endings.MainMenu,
                levelFinished = false
            });
        }
    }
}