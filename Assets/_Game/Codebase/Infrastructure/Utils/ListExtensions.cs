using System.Collections.Generic;

namespace Infrastructure.Utils
{
    public static class ListExtensions
    {
        public static T Random<T>(this IList<T> list) => 
            list[UnityEngine.Random.Range(0, list.Count)];
    }
}