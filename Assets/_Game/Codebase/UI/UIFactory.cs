using Infrastructure.AssetProvider;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly AssetProvider _assets;

        public UIFactory(IInstantiator instantiator, AssetProvider assets)
        {
            _instantiator = instantiator;
            _assets = assets;
        }

        public T Create<T>(Transform parent = null) where T : WindowBase =>
            _instantiator.InstantiatePrefabForComponent<T>(
                _assets.GetComponentOnPrefab<T>(typeof(T).Name), parent);
    }
}