using Gameplay.Data;
using Infrastructure.FSM.States;
using Infrastructure.StateMachine;
using Infrastructure.Utils;
using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Level
{
    public class LevelWatch
    {
        private readonly LazyInject<LevelStateMachine> _levelFsm;
        private readonly DataProvider _dataProvider;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly LevelPresenter _uiPresenter;
        private Row[] _rows;

        public LevelWatch(LazyInject<LevelStateMachine> levelFsm, DataProvider dataProvider, LevelPresenter uiPresenter)
        {
            _levelFsm = levelFsm;
            _dataProvider = dataProvider;
            _uiPresenter = uiPresenter;
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        public void Start()
        {
            _rows = GameObject.FindObjectsByType<Row>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            
            _dataProvider.playerData.lives
                .SkipWhile(lives => lives > 0)
                .First()
                .Subscribe(_ => Loose())
                .AddTo(_compositeDisposable);

            Observable
                .EveryUpdate()
                .ThrottleFrame(1)
                .Where(_ => Time.timeScale > Constants.Epsilon)
                .Subscribe(_ => CheckWon())
                .AddTo(_compositeDisposable);
        }

        private void CheckWon()
        {
            foreach (Row row in _rows)
            {
                if (row.ActiveBlocks.Count > 0)
                    return;
            }

            _dataProvider.levelData.LevelFinished = true;
            _levelFsm.Value.Enter<LevelMenu>();
        }

        private void Loose()
        {
            _uiPresenter.ShowLooseWindow();
            _levelFsm.Value.Enter<LevelMenu>();
        }
    }
}