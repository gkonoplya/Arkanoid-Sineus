using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Gameplay.Data;
using Infrastructure.Utils;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Buffs
{
    public class BonusContainer: MonoBehaviour
    {
        [SerializeField]
        private List<BonusDescriptionSO> descriptionsSO = new();

        private List<BonusDescription> _descriptions;
        public float lootProbability;
        private BonusFactory _bonusFactory;

        [Inject]
        public void Construct(BonusFactory bonusFactory)
        {
            _bonusFactory = bonusFactory;
        }

        private void Start()
        {
            if (_descriptions != null)
                return;
            _descriptions = new List<BonusDescription>();
            foreach (var descriptionSo in descriptionsSO) 
                _descriptions.Add(descriptionSo.description);

            GetComponent<Health>()?.amount
                .First(val => val < Constants.Epsilon)
                .Subscribe(_ => SpawnBonus())
                .AddTo(this);
        }

        public void SetDescriptions(List<BonusDescription> descriptions) => 
            _descriptions = descriptions;

        private void SpawnBonus()
        {
            if (lootProbability < Random.value)
                return;
            
            float totalProbabilities = _descriptions.Sum(desc => desc.Probability);

            float pos = Random.Range(0f, totalProbabilities);

            foreach (BonusDescription descriptionSo in _descriptions)
            {
                pos -= descriptionSo.Probability;
                if (pos < Constants.Epsilon)
                {
                    _bonusFactory.CreateAt(descriptionSo, transform.position);
                }
            }
        }
    }
}