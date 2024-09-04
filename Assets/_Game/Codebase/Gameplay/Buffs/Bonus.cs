using System;
using Gameplay.Data;
using UnityEngine;
using Zenject;

namespace Gameplay.Buffs
{
    public class Bonus: MonoBehaviour
    {
        private BuffFactory _buffFactory;
        public BonusDescription description;
        public float fallSpeed;

        [Inject]
        public void Construct(BuffFactory buffFactory)
        {
            _buffFactory = buffFactory;
        }

        private void Update()
        {
            Vector3 lastPos = transform.position;
            transform.position = new Vector3(lastPos.x, lastPos.y - fallSpeed * Time.deltaTime, lastPos.z);
        }

        public void ApplyBonus()
        {
            _buffFactory.AddBuff(description);
        }
    }
}