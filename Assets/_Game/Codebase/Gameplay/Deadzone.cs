using Gameplay.Data;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class Deadzone : MonoBehaviour
    {
        private DataProvider _dataProvider;

        [Inject]
        public void Construct(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        
        public void RemoveLife() => 
            _dataProvider.playerData.lives.Value--;
    }
}