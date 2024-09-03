using Infrastructure.InputService;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
    }
}