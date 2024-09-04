using UnityEngine;

namespace Infrastructure.AssetProvider
{
    [CreateAssetMenu(fileName = "Assets", menuName = "StaticData/Settings/Asset provider", order = 50)]
    public class AssetProvider : ScriptableObject
    {
        [SerializeField] private AssetsDB _assets;
        

        public GameObject GetPrefab(string id) => 
            _assets[id];

        public T GetComponentOnPrefab<T>(string id) where T : Component => 
            _assets[id].GetComponent<T>();
        
        
    }
}