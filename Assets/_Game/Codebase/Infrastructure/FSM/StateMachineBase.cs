using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.StateMachine
{
  public abstract class StateMachineBase
  {
    private readonly Dictionary<Type, IExitableState> _states;

    protected StateMachineBase(IEnumerable<IExitableState> states)
    {
      _states = states.ToDictionary(x => x.GetType(), x => x);
    }
    
    public IExitableState ActiveState { get; protected set; }

    public void Enter<TState>() where TState : class, IState => 
      ChangeState<TState>().Enter();

    public void Enter(IState newState)
    {
      ActiveState?.Exit();
      UnityEngine.Debug.Log($"Exiting state {ActiveState}");
      ActiveState = newState;
      UnityEngine.Debug.Log($"Entering state {newState}");
      newState.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> => 
      ChangeState<TState>().Enter(payload);

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      ActiveState?.Exit();
      UnityEngine.Debug.Log($"Exiting state {ActiveState}");
      var state = GetState<TState>();
      ActiveState = state;
      UnityEngine.Debug.Log($"Entering state {state}");
      return state;
    }
    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }
}