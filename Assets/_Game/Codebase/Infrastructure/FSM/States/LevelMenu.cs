using Gameplay.Data;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Infrastructure.FSM.States
{
    public class LevelMenu: IState
    {
        private readonly DataProvider _dataProvider;
        private readonly SaveLoadService _saveLoadService;

        public LevelMenu(DataProvider dataProvider, SaveLoadService saveLoadService)
        {
            _dataProvider = dataProvider;
            _saveLoadService = saveLoadService;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            
            _dataProvider.playerData.LevelCompleted = _dataProvider.levelData.LevelFinished;
            _saveLoadService.Save(_dataProvider.playerData);
      
            if (_dataProvider.playerData.LevelCompleted || _dataProvider.levelData.NeedRestart)
                _saveLoadService.Save<ConstructorData>(null);
        }

        public void Enter()
        {
            Time.timeScale = 0f;
        }
    }
}