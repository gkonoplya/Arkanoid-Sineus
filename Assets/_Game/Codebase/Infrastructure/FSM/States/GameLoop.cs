using System.Threading;
using Zenject;

namespace Infrastructure.StateMachine.States
{
  public class GameLoop: IState
  {
    private readonly LazyInject<LevelStateMachine> _gameStateMachine;

    public GameLoop(LazyInject<LevelStateMachine> gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }
    
    public void Enter()
    {
      
    }

    public void Exit()
    {
     
    }
  }
}