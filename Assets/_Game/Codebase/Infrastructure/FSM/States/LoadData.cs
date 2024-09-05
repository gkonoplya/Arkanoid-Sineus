using System.Collections.Generic;

using Zenject;

namespace Infrastructure.StateMachine.States
{
  public class LoadData : IState
  {
    private readonly LazyInject<LevelStateMachine> _gameStateMachine;

    public LoadData(LazyInject<LevelStateMachine> gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
      
    }

    public void Exit()
    {
      
    }

    public void Enter()
    {
      _gameStateMachine.Value.Enter<EpisodeStart>();
    }
    
  }
}