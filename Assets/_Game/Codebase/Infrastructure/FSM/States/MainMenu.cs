using Infrastructure.StateMachine;
using UnityEngine.SceneManagement;

namespace Infrastructure.FSM.States
{
    public class MainMenu: IState
    {
        public void Exit()
        {
            
        }

        public void Enter()
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}