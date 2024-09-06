using Gameplay.Data;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;
using Zenject;

namespace Infrastructure.FSM.States
{
    public class StartNewLevel: IState
    {
        private readonly LazyInject<GameFSM> _gameFSM;
        private readonly SaveLoadService _saveloadService;

        public StartNewLevel(LazyInject<GameFSM> gameFsm, SaveLoadService saveloadService)
        {
            _gameFSM = gameFsm;
            _saveloadService = saveloadService;
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
            ClearPlayerData();
            _gameFSM.Value.Enter<Level>();
        }

        private void ClearPlayerData()
        {
            _saveloadService.Save(PlayerData.Default());
            _saveloadService.Save<ConstructorData>(null);
        }
    }
}