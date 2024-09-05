using System.Collections.Generic;
using System.Linq;
using Gameplay.Data;
using Infrastructure.AssetProvider;
using Infrastructure.Helpers;
using UnityEngine;
using Zenject;

namespace Gameplay.Level
{
    public class LevelConstructor: MonoBehaviour
    {
        public GameObject blocksParent;
        private IInstantiator _instantiator;
        private AssetProvider _assetProvider;

        [Inject]
        public void Construct(IInstantiator instantiator, AssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }
        
        public ConstructorData GetBlockData()
        {
            var data = new ConstructorData();
            foreach (Row rowData in blocksParent.GetComponentsInChildren<Row>().Where(x=> x.ActiveInHierarchy())) 
                data.Add(rowData.Data, rowData.GatherBlockData());
            return data;
        }

        public void BuildFromPrefab(string levelName) => 
            _instantiator.InstantiatePrefab(_assetProvider.GetPrefab(levelName));

        public void BuildFromConstructorData(ConstructorData data)
        {
            blocksParent = new GameObject()
            {
                name = "Blocks"
            };
            foreach (KeyValuePair<RowData,BlockDataSD> keyValuePair in data)
            {
                var row = _instantiator.InstantiatePrefabForComponent<Row>(
                    _assetProvider.GetComponentOnPrefab<Row>("Row"),
                    keyValuePair.Key.Position, Quaternion.identity, blocksParent.transform);
                row.gameObject.name = keyValuePair.Key.Name;

                foreach (Block block in row.GetComponentsInChildren<Block>())
                {
                    if (keyValuePair.Value.TryGetValue(block.SpawnPositionName, out BlockData blockData)) 
                        block.InitWith(blockData);
                    else
                        block.gameObject.SetActive(false);
                }
            }
        }
    }
}