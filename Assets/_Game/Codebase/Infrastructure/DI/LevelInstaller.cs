using Gameplay.Buffs;
using Gameplay.Data;
using Infrastructure.FSM.States;
using Infrastructure.InputService;
using Infrastructure.StateMachine.States;
using Zenject;

namespace Infrastructure.DI
{
    public class LevelInstaller : MonoInstaller
    {
        public LevelDataProvider levelData;
        public AssetProvider.AssetProvider assetProvider;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
            Container.Bind<BonusFactory>().AsSingle();
            Container.Bind<BuffFactory>().AsSingle();
            Container.Bind<ProjectileFactory>().AsSingle();
            Container.BindInstance(levelData).AsSingle();
            Container.BindInstance(assetProvider).AsSingle();
            InstallStates();
        }

        private void InstallStates()
        {
            Container.BindInterfacesAndSelfTo<LoadData>().AsSingle();
            Container.BindInterfacesAndSelfTo<EpisodeEnd>().AsSingle();
            Container.BindInterfacesAndSelfTo<EpisodeStart>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoop>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelMenu>().AsSingle();
        }
    }
}