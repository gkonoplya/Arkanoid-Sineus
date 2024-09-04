using Zenject;

namespace Infrastructure.StateMachine
{
  public class StateMachineInstaller: MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<LevelStateMachine>().AsSingle();
    }
  }
}