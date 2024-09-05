using Zenject;

namespace Infrastructure.StateMachine.States
{
  public class EpisodeStart : IState
  {
    private readonly LazyInject<LevelStateMachine> _gameStateMachine;


    public EpisodeStart(LazyInject<LevelStateMachine> gameStateMachine)
    {
      
      _gameStateMachine = gameStateMachine;
    }

    public void Exit()
    {
      
    }

    public void Enter()
    {
      _gameStateMachine.Value.Enter<GameLoop>();
    }

  }
}