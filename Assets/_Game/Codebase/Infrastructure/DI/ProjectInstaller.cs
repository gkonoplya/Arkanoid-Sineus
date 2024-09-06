using Infrastructure.FSM;
using Infrastructure.FSM.States;
using Infrastructure.SaveLoad;
using Zenject;

namespace Infrastructure.DI
{
    public class ProjectInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameFSM>().AsSingle();
            Container.Bind<SaveLoadService>().AsSingle();
            InstallStates();
        }

        private void InstallStates()
        {
            Container.BindInterfacesAndSelfTo<MainMenu>().AsSingle();
            Container.BindInterfacesAndSelfTo<StartNewLevel>().AsSingle();
            Container.BindInterfacesAndSelfTo<Level>().AsSingle();
        }
    }
}