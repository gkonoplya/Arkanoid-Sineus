using Gameplay.Buffs;
using UnityEngine;

namespace Gameplay.Level
{
    public class Block: MonoBehaviour
    {
        public BlockType Type;
        private Row _row;
        private bool _inited;
        public string SpawnPositionName => transform.parent.name; 

        private void Start()
        {
            _row = GetComponentInParent<Row>();
            _row.AddBlock(this);
            
            if (_inited)
                return;
            
            GetComponent<ColorSetter>()?.SetColor();
        }

        private void OnDisable()
        {
            _row?.RemoveBlock(this);
        }

        public BlockData GatherBlockData()
        {
            var health = GetComponent<Health>();
            return new BlockData
            {
                type = Type,
                Color = GetComponent<SpriteRenderer>().color,
                currentHealth = health?.amount.Value,
                maxHealth = health?.MaxHealth ?? 0f
            };
        }

        public void InitWith(BlockData blockData)
        {
            _inited = true;
            if (blockData.currentHealth != null)
            {
                Health health = GetComponent<Health>();
                health.amount.Value = blockData.currentHealth.Value;
                health.MaxHealth = blockData.maxHealth;
            }

            GetComponent<SpriteRenderer>().color = blockData.Color;
            Type = blockData.type;
        }
    }
}