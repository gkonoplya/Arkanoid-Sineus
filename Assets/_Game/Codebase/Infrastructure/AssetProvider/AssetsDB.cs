using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Infrastructure.AssetProvider
{
    [Serializable]
    public class AssetsDB : SerializableDictionaryBase<string, GameObject>
    {
    }
}