using System.Collections.Generic;
using Infrastructure.AssetProvider;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LivesImageSpawner: MonoBehaviour
    {
        private const string Live = "Live";
        private AssetProvider _assetProvider;
        private Stack<GameObject> _liveViews = new();

        [Inject]
        public void Construct(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        private void CreateLive() => 
            _liveViews
                .Push(
                    Instantiate(_assetProvider.GetPrefab(Live),
                        transform));

        private void RemoveLive() => 
            Destroy(_liveViews.Pop());

        public void ResolveLives(int lives)
        {
            while (lives > _liveViews.Count)
                    CreateLive();
            
            while (lives < _liveViews.Count) 
                    RemoveLive();
        }
    }
}