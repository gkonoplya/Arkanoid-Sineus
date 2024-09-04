using System.Collections.Generic;
using Infrastructure.Utils;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorSetter : MonoBehaviour
    {
        public List<Color> colorList;

        public void SetColor(Color? color = null) => 
            GetComponent<SpriteRenderer>().color = color ?? colorList.Random();
    }
}