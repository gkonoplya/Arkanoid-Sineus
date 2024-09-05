using System.Collections.Generic;
using Gameplay.Data;
using UnityEngine;

namespace Gameplay.Level
{
    public class Row : MonoBehaviour
    {
        public List<Block> ActiveBlocks = new();

        public RowData Data => new RowData()
        {
            Name = gameObject.name,
            Position = transform.localPosition
        };
        
        public void RemoveBlock(Block block)
        {
            if (!ActiveBlocks.Contains(block)) 
                return;
            
            ActiveBlocks.Remove(block);
        }

        public void AddBlock(Block block) => 
            ActiveBlocks.Add(block);

        public BlockDataSD GatherBlockData()
        {
            var data = new BlockDataSD();
            foreach (Block block in ActiveBlocks)
                data[block.SpawnPositionName] = block.GatherBlockData();
            return data;
        }
    }
}