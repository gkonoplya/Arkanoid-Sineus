using Gameplay.Data;
using Gameplay.Level;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace Infrastructure.FSM.States
{
  public class GameLoop: IState
  {
    private readonly LazyInject<LevelStateMachine> _levelStateMachine;
    private readonly SaveLoadService _saveLoadService;
    private readonly LevelConstructor _levelConstructor;
    private readonly DataProvider _dataProvider;
    private LevelWatch _levelWatch;

    public GameLoop(LazyInject<LevelStateMachine> levelStateMachine, DataProvider dataProvider,
      LevelConstructor levelConstructor, SaveLoadService saveLoadService, LevelWatch levelWatch)
    {
      _levelStateMachine = levelStateMachine;
      _dataProvider = dataProvider;
      _levelConstructor = levelConstructor;
      _saveLoadService = saveLoadService;
      _levelWatch = levelWatch;
    }
    
    public void Enter()
    {
      Time.timeScale = 1f;
      
      _dataProvider.playerData.LevelCompleted = false;
      
    }

    public void Exit()
    {
      UpdateHighScore();

      _saveLoadService.Save(_dataProvider.playerData);
      _saveLoadService.Save(_levelConstructor.GetBlockData());
    }

    private void UpdateHighScore()
    {
      if (_dataProvider.levelData.Highscore >= _dataProvider.playerData.scores.Value) 
        return;
      var highscore = _saveLoadService.Load<HighscoreTable>();
      highscore.Table[_dataProvider.levelData.LevelIndex-1] = _dataProvider.playerData.scores.Value;
      _saveLoadService.Save(highscore);
    }
  }
}