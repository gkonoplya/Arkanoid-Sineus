using Gameplay.Buffs;
using UnityEngine;

namespace Gameplay.Level
{
    public class Block: MonoBehaviour
    {
        public BlockType Type;
        private RowData _rowData;
        private bool _inited;

        private void Start()
        {
            if (_inited)
                return;
            
            _rowData = GetComponentInParent<RowData>();
            _rowData.AddBlock(this);
            
            GetComponent<ColorSetter>()?.SetColor();
        }

        private void OnDisable()
        {
            _rowData.RemoveBlock(this);
        }

        public BlockData GatherBlockData()
        {
            var health = GetComponent<Health>();
            return new BlockData
            {
                type = Type,
                Color = GetComponent<SpriteRenderer>().color,
                Position = transform.parent.localPosition,
                currentHealth = health?.amount.Value,
                maxHealth = health?.MaxHealth ?? 0f
            };
        }
    }
}