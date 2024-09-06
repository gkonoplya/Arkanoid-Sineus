using UI;
using Zenject;

namespace Infrastructure.DI
{
    public class MenuInstaller: MonoInstaller
    {
        public AssetProvider.AssetProvider AssetProvider;
        public override void InstallBindings()
        {
            Container.BindInstance(AssetProvider);
            Container.Bind<UIFactory>().AsSingle();
        }
    }
}