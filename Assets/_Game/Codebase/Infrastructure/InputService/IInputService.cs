using UniRx;
using UnityEngine;

namespace Infrastructure.InputService
{
  public interface IInputService
  {
    Vector2 Axis { get; }
    bool Active { get; set; }
    ReactiveProperty<bool> IsAttackButtonPressedReactive { get; }
    ReactiveProperty<bool> IsCancelPressedReactive { get; }
  }
}