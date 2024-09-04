using System.Collections.Generic;
using Infrastructure.StateMachine.States;
using Zenject;

namespace Infrastructure.StateMachine
{
  public class LevelStateMachine: StateMachineBase, IInitializable
  {
    private IExitableState _pausedState;
    public LevelStateMachine(IEnumerable<IExitableState> states) : base(states) { }
    public void Initialize() => Enter<LoadData>();
  }
}