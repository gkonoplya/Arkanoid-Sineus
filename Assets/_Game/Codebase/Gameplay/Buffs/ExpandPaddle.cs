using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Buffs
{
    public class ExpandPaddle: TimedMonobeh
    {
        public float scaleFactor = 1.5f;
        private Vector3 _savedScale;

        private void OnEnable()
        {
            _savedScale = transform.localScale;
            transform
                .DOScale(new Vector3(_savedScale.x * scaleFactor, _savedScale.y, _savedScale.z)
                    , 0.2f);
            TimerCallback = () => Destroy(this);
        }

        private void OnDisable()
        {
            transform.DOScale(_savedScale, 0.2f);
        }
    }
}