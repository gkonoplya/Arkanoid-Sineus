using Gameplay.Data;
using UnityEngine;

namespace Gameplay
{
    public class ScoreWatch
    {
        private readonly DataProvider _dataProvider;

        public ScoreWatch(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        private const float DamageCoefficient = 1f;

        public void ReportDamage(float damage)
        {
            _dataProvider.playerData.scores.Value += Mathf.FloorToInt(damage * DamageCoefficient);
        }
    }
}