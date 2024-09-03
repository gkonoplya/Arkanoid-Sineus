using UnityEngine;

namespace Infrastructure.InputService
{
  public class StandaloneInputService : IInputService
  {
    private const string Horizontal = "Horizontal";
    private const string Fire = "Fire1";
    private const string Cancel = "Cancel";

    public Vector2 Axis => MoveAxis();
    public bool Active { get; set; } = true;
    public bool IsAttackButtonPressed() => 
      Input.GetButton(Fire);
    public bool IsCancelPressed() =>
      Input.GetButton(Cancel);

    private static Vector2 MoveAxis() => 
      new(Input.GetAxis(Horizontal),0f);
  }
}