using Infrastructure.StateMachine;
using UnityEngine;

namespace Infrastructure.FSM.States
{
    public class LevelMenu: IState
    {
        public void Exit()
        {
            Time.timeScale = 1f;
        }

       

        public void Enter()
        {
            Time.timeScale = 0f;
        }
    }
}