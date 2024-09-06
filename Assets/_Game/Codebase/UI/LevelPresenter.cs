using System;
using Gameplay.Data;
using Infrastructure.FSM.States;
using Infrastructure.InputService;
using Infrastructure.StateMachine;
using TMPro;
using UI.Windows;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LevelPresenter: MonoBehaviour
    {
        public TMP_Text scoresText;
        public TMP_Text highscoreText;
        public LivesImageSpawner liveSpawner;
        private DataProvider _dataProvider;
        private IInputService _inputService;
        private LevelMenuWindow _levelMenuWindow;
        private LevelStateMachine _fsm;
        private UIFactory _uiFactory;

        [Inject]
        public void Construct(UIFactory uiFactory, DataProvider dataProvider, LevelStateMachine fsm,
            IInputService inputService)
        {
            _uiFactory = uiFactory;
            _dataProvider = dataProvider;
            _fsm = fsm;
            _inputService = inputService;
        }

        private void Start()
        {
            highscoreText.text = _dataProvider.levelData.Highscore.ToString();
            _dataProvider.playerData.scores
                .Subscribe(score => scoresText.text = score.ToString())
                .AddTo(this);
            _dataProvider.playerData.lives
                .Subscribe(lives => liveSpawner.ResolveLives(lives))
                .AddTo(this);

            _inputService.IsCancelPressedReactive
                .Where(active => active)
                .Throttle(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ => NavigateMenuWindow())
                .AddTo(this);
        }

        private void NavigateMenuWindow()
        {
            if (_levelMenuWindow != null)
            {
                _levelMenuWindow.Close();
                _fsm.Enter<GameLoop>();
                return;
            }

            _levelMenuWindow = _uiFactory.Create<LevelMenuWindow>(transform);
            _fsm.Enter<LevelMenu>();
        }

        public void ShowHelpWindow() => 
            _uiFactory.Create<HelpWindow>(transform);

        public void ShowLooseWindow() => 
            _uiFactory.Create<LevelLooseWindow>();
    }
}