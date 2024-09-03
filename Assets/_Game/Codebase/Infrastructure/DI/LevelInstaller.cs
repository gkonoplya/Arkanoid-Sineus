using Infrastructure.InputService;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
    }
}