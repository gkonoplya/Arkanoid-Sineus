using Gameplay.Data;
using Infrastructure.AssetProvider;
using Zenject;
using UniRx;
using UnityEngine;

namespace Gameplay.Paddle
{
    public class PlayerWatch: MonoBehaviour
    {
        private const string Paddle = "Paddle";
        private const string Ball = "Ball";
        private DataProvider _dataProvider;
        private int _lastLives;
        private IInstantiator _instantiator;
        private AssetProvider _assetProvider;
        
        [Inject]
        public void Construct(DataProvider dataProvider, IInstantiator instantiator, AssetProvider assetProvider)
        {
            _dataProvider = dataProvider;
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }
        

        public void Start()
        {
            _lastLives = _dataProvider.playerData.lives.Value;
            _dataProvider.playerData.lives
                .Subscribe(CheckNeedSpawn)
                .AddTo(this);
        }

        private void CheckNeedSpawn(int i)
        {
            if (i >= _lastLives)
            {
                _lastLives = i;
                return;
            }

            _lastLives = i;
            Destroy(_dataProvider.levelData.Paddle);
            Destroy(_dataProvider.levelData.Ball);
            _dataProvider.levelData.Paddle =
                _instantiator.InstantiatePrefab(_assetProvider.GetPrefab(Paddle), transform);
            _dataProvider.levelData.Ball =
                _instantiator.InstantiatePrefab(_assetProvider.GetPrefab(Ball), transform);
        }
    }
}