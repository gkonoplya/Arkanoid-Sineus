using System.Linq;
using Gameplay.Data;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;
using Zenject;

namespace Infrastructure.FSM.States
{
  public class LoadData : IState
  {
    private readonly LazyInject<LevelStateMachine> _gameStateMachine;
    private readonly SaveLoadService _saveLoadService;
    private readonly DataProvider _dataProvider;

    public LoadData(LazyInject<LevelStateMachine> gameStateMachine, SaveLoadService saveLoadService, DataProvider dataProvider)
    {
      _gameStateMachine = gameStateMachine;
      _saveLoadService = saveLoadService;
      _dataProvider = dataProvider;
    }

    public void Exit()
    {
      
    }

    public void Enter()
    {
      LoadHighscores();
      
      _dataProvider.playerData = _saveLoadService.Load(PlayerData.Default());

      _dataProvider.constructorData = _saveLoadService.Load<ConstructorData>();

      _gameStateMachine.Value.Enter<EpisodeStart>();
    }

    private void LoadHighscores()
    {
      var highscoreTable = _saveLoadService.Load<HighscoreTable>(new HighscoreTable());
      int levelIndex = _dataProvider.levelData.LevelIndex;
      if (highscoreTable.Table.Count < levelIndex) 
        highscoreTable.Table.AddRange(Enumerable.Range(0,levelIndex - highscoreTable.Table.Count));
      _saveLoadService.Save(highscoreTable);
      
      _dataProvider.levelData.Highscore = highscoreTable.Table[levelIndex-1];
    }
  }
}