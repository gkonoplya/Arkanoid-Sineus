using Infrastructure.StateMachine;
using UnityEngine.SceneManagement;

namespace Infrastructure.FSM.States
{
    public class Level: IState
    {
        public void Exit()
        {
            
        }

        public void Enter()
        {
            SceneManager.LoadScene("LevelScene");
        }
    }
}