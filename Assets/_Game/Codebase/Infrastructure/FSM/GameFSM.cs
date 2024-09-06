using System.Collections.Generic;
using Infrastructure.FSM.States;
using Infrastructure.StateMachine;
using Zenject;

namespace Infrastructure.FSM
{
    public class GameFSM: StateMachineBase, IInitializable
    {
        public GameFSM(IEnumerable<IExitableState> states) : base(states)
        {
        }

        public void Initialize() => Enter<MainMenu>();
    }
}