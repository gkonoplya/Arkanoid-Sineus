using System.Collections.Generic;
using Infrastructure.Utils;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorSetter : MonoBehaviour
    {
        public List<Color> colorList;

        private void Start() => 
            GetComponent<SpriteRenderer>().color = colorList.Random();
    }
}