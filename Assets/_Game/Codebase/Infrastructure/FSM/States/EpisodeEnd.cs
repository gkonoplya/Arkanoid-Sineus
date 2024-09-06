using Gameplay.Data;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;

namespace Infrastructure.FSM.States
{
  public class EpisodeEnd: IState
  {
    private readonly DataProvider _dataProvider;
    private readonly SaveLoadService _saveLoadService;

    public EpisodeEnd(DataProvider dataProvider, SaveLoadService saveLoadService)
    {
      _dataProvider = dataProvider;
      _saveLoadService = saveLoadService;
    }

    public void Exit()
    {
      
    }

    public void Enter()
    {
      _dataProvider.playerData.LevelCompleted = _dataProvider.levelData.LevelFinished;
      _saveLoadService.Save(_dataProvider.playerData);
      
      if (_dataProvider.playerData.LevelCompleted || _dataProvider.levelData.NeedRestart)
        _saveLoadService.Save<ConstructorData>(null);
      
    }

    
  }
}