using Gameplay.Buffs;
using Gameplay.Data;
using Infrastructure.InputService;
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
            Container.BindInstance(levelData).AsSingle();
            Container.BindInstance(assetProvider).AsSingle();

        }
    }
}