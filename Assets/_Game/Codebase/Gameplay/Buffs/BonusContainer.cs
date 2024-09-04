using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Data;
using Infrastructure.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Buffs
{
    public class BonusContainer: MonoBehaviour
    {
        public List<BonusDescription> descriptions;
        public float lootProbability;
        private BonusFactory _bonusFactory;

        [Inject]
        public void Construct(BonusFactory bonusFactory)
        {
            _bonusFactory = bonusFactory;
        }

        private void OnDisable()
        {
            if (lootProbability < Random.value)
                return;
            
            float totalProbabilities = descriptions.Sum(desc => desc.Probability);

            float pos = Random.Range(0f, totalProbabilities);

            foreach (BonusDescription description in descriptions)
            {
                pos -= description.Probability;
                if (pos < Constants.Epsilon)
                {
                    _bonusFactory.CreateAt(description, transform.position);
                }
            }
        }
    }
}