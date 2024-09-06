using Gameplay.Buffs;
using Gameplay.Data;
using Gameplay.Level;
using Infrastructure.FSM.States;
using Infrastructure.InputService;
using UI;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure.DI
{
    public class LevelInstaller : MonoInstaller
    {
        [FormerlySerializedAs("levelData")] public DataProvider data;
        public AssetProvider.AssetProvider assetProvider;
        public LevelConstructor levelConstructor;
        public LevelPrefabs levelPrefabs;
        public LevelPresenter levelPresenter;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
            Container.Bind<LevelWatch>().AsTransient();
            InstallFactories();
            InstallInstances();
            InstallStates();
        }

        private void InstallInstances()
        {
            Container.BindInstance(levelConstructor).AsSingle();
            Container.BindInstance(data).AsSingle();
            Container.BindInstance(assetProvider).AsSingle();
            Container.BindInstance(levelPrefabs).AsSingle();
            Container.BindInstance(levelPresenter).AsSingle();
            
        }

        private void InstallFactories()
        {
            Container.Bind<BonusFactory>().AsSingle();
            Container.Bind<BuffFactory>().AsSingle();
            Container.Bind<ProjectileFactory>().AsSingle();
            Container.Bind<UIFactory>().AsSingle();
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