using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Level
{
    public class RowData : MonoBehaviour
    {
        public List<Block> ActiveBlocks = new();
        
        public void RemoveBlock(Block block)
        {
            if (!ActiveBlocks.Contains(block)) 
                return;
            
            ActiveBlocks.Remove(block);
        }

        public void AddBlock(Block block) => 
            ActiveBlocks.Add(block);

        public List<BlockData> GatherBlockData() =>
            ActiveBlocks.Select(block => block.GatherBlockData()).ToList();
    }
}