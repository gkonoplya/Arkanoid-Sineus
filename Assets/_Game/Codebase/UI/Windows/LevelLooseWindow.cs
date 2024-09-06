using Gameplay.Data;
using Infrastructure.FSM;
using Infrastructure.FSM.States;
using Infrastructure.SaveLoad;
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
        private GameFSM _gameFsm;
        private DataProvider _dataProvider;
        private SaveLoadService _saveLoadService;

        [Inject]
        public void Construct(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
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
                    _dataProvider.levelData.NeedRestart = true;
                    _gameFsm.Enter<Level>();
                })
                .AddTo(this);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _gameFsm.Enter<MainMenu>();
        }
    }
}