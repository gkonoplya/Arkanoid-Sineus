using System;
using Gameplay.Data;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;
using Zenject;

namespace Infrastructure.FSM.States
{
  public class EpisodeEnd: IPayloadedState<EpisodeEndPayload>
  {
    private readonly DataProvider _dataProvider;
    private readonly SaveLoadService _saveLoadService;
    private readonly LazyInject<GameFSM> _gameFsm;

    public EpisodeEnd(DataProvider dataProvider, SaveLoadService saveLoadService, LazyInject<GameFSM> gameFsm)
    {
      _dataProvider = dataProvider;
      _saveLoadService = saveLoadService;
      _gameFsm = gameFsm;
    }

    public void Enter(EpisodeEndPayload payload)
    {
      _dataProvider.playerData.LevelCompleted = payload.levelFinished;
      _saveLoadService.Save(_dataProvider.playerData);
      
      if (payload.levelFinished || payload.needRestart)
        _saveLoadService.Save<ConstructorData>(null);
      
      if (payload.needRestart)
        RestorePlayerDefaults();

      switch (payload.ending)
      {
        case EpisodeEndPayload.Endings.Restart:
        case EpisodeEndPayload.Endings.NextLevel:
          _gameFsm.Value.Enter<Level>();
          break;
        case EpisodeEndPayload.Endings.MainMenu:
          _gameFsm.Value.Enter<MainMenu>();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
    
    private void RestorePlayerDefaults()
    {
      var playerData = PlayerData.Default();
      playerData.currentLevelIndex = _dataProvider.playerData.currentLevelIndex;
      _saveLoadService.Save(playerData);
    }

    public void Exit()
    {
      
    }
  }
}