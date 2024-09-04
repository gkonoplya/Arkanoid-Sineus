using Gameplay.Buffs;
using Gameplay.Data;
using Infrastructure.AssetProvider;
using Infrastructure.InputService;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public LevelData levelData;
    public AssetProvider assetProvider;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
        Container.Bind<BonusFactory>().AsSingle();
        Container.Bind<BuffFactory>().AsSingle();
        Container.BindInstance(levelData).AsSingle();
        Container.BindInstance(assetProvider).AsSingle();

    }
}