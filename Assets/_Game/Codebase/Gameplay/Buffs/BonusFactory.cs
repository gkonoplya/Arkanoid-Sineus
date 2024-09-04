using Gameplay.Data;
using Infrastructure.AssetProvider;
using UnityEngine;
using Zenject;

namespace Gameplay.Buffs
{
    public class BonusFactory
    {
        private const string BonusId = "Bonus";
        private readonly IInstantiator _iinstantiator;
        private readonly AssetProvider _assetProvider;

        public BonusFactory(IInstantiator iinstantiator, AssetProvider assetProvider)
        {
            _iinstantiator = iinstantiator;
            _assetProvider = assetProvider;
        }

        public void CreateAt(BonusDescription description, Vector3 position)
        {
            var bonus = _iinstantiator.InstantiatePrefabForComponent<Bonus>(
                _assetProvider.GetComponentOnPrefab<Bonus>(BonusId), position, Quaternion.identity, null);
            
            bonus.description = description;
        }
    }
}