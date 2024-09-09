using Gameplay.Data;
using Gameplay.Level;
using Infrastructure.StateMachine;
using Zenject;

namespace Infrastructure.FSM.States
{
  public class EpisodeStart : IState
  {
    private readonly LazyInject<LevelStateMachine> _gameStateMachine;
    private readonly LevelConstructor _levelConstructor;
    private readonly DataProvider _dataProvider;
    private readonly LevelWatch _levelWatch;


    public EpisodeStart(LazyInject<LevelStateMachine> gameStateMachine, LevelConstructor levelConstructor, DataProvider dataProvider, LevelWatch levelWatch)
    {
      _gameStateMachine = gameStateMachine;
      _levelConstructor = levelConstructor;
      _dataProvider = dataProvider;
      _levelWatch = levelWatch;
    }

    public void Exit()
    {
      
    }

    public void Enter()
    {
      if (_dataProvider.playerData.LevelCompleted)
        _dataProvider.playerData.currentLevelIndex++;
      
      if (!_dataProvider.constructorData.IsValid())
        _levelConstructor.BuildFromPrefab(_dataProvider.playerData.currentLevelIndex);
      else
        _levelConstructor.BuildFromConstructorData(_dataProvider.constructorData);

      _levelWatch.Start();
      _gameStateMachine.Value.Enter<GameLoop>();
    }

  }
}