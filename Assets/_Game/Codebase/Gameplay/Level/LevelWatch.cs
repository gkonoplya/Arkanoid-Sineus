﻿using System;
using System.Collections.Generic;
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
    public class LevelWatch: IDisposable
    {
        private readonly LazyInject<LevelStateMachine> _levelFsm;
        private readonly DataProvider _dataProvider;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly LevelPresenter _uiPresenter;
        private IList<Row> _rows;
        private readonly LevelPresenter _levelPresenter;

        public LevelWatch(LazyInject<LevelStateMachine> levelFsm, DataProvider dataProvider, LevelPresenter uiPresenter, LevelPresenter levelPresenter)
        {
            _levelFsm = levelFsm;
            _dataProvider = dataProvider;
            _uiPresenter = uiPresenter;
            _levelPresenter = levelPresenter;
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
        

        public void Start()
        {
            _dataProvider.playerData.lives
                .SkipWhile(lives => lives > 0)
                .First()
                .Subscribe(_ => Loose())
                .AddTo(_compositeDisposable);

            Observable
                .Interval(TimeSpan.FromSeconds(0.5f))
                .Where(_ => Time.timeScale > Constants.Epsilon)
                .TakeWhile(_ => !_dataProvider.levelData.LevelFinished)
                .Subscribe(_ => CheckWon())
                .AddTo(_compositeDisposable);
        }

        private void CheckWon()
        {
            foreach (Row row in GameObject.FindObjectsByType<Row>(FindObjectsSortMode.None))
            {
                if (row.ActiveBlocks.Count > 0)
                    return;
            }
            _dataProvider.levelData.LevelFinished = true;
            _levelFsm.Value.Enter<LevelMenu>();
            _levelPresenter.ShowWonWindow();
        }

        private void Loose()
        {
            _uiPresenter.ShowLooseWindow();
            _levelFsm.Value.Enter<LevelMenu>();
        }
    }
}