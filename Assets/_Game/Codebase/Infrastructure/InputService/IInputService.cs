using UnityEngine;

namespace Infrastructure.InputService
{
  public interface IInputService
  {
    Vector2 Axis { get; }
    bool Active { get; set; }
    bool IsAttackButtonPressed();
    bool IsCancelPressed();
  }
}