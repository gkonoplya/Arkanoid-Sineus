using System;
using Gameplay.Level;
using RotaryHeart.Lib.SerializableDictionary;

namespace Gameplay.Data
{
    [Serializable]
    public class ConstructorData: SerializableDictionaryBase<RowData, BlockDataSD>
    {
        
    }

    public static class ConstructorDataExtensions
    {
        public static bool IsValid(this ConstructorData data) =>
            data is { Count: > 0 };
    }
}