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


    public EpisodeStart(LazyInject<LevelStateMachine> gameStateMachine, LevelConstructor levelConstructor, DataProvider dataProvider)
    {
      _gameStateMachine = gameStateMachine;
      _levelConstructor = levelConstructor;
      _dataProvider = dataProvider;
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
      
      
      _gameStateMachine.Value.Enter<GameLoop>();
    }

  }
}