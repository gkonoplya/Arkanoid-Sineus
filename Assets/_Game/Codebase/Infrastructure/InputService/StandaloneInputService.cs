using UniRx;
using UnityEngine;
using Zenject;

namespace Infrastructure.InputService
{
  public class StandaloneInputService : IInputService, ITickable, IInitializable
  {
    private const string Horizontal = "Horizontal";
    private const string Fire = "Fire1";
    private const string Cancel = "Cancel";

    public Vector2 Axis => MoveAxis();
    public bool Active { get; set; } = false;
    public ReactiveProperty<bool> IsAttackButtonPressedReactive { get; } = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsCancelPressedReactive { get; } = new ReactiveProperty<bool>(false);

    private bool IsAttackButtonPressed() => 
      Input.GetButton(Fire);

    private bool IsCancelPressed() =>
      Input.GetButton(Cancel);

    private static Vector2 MoveAxis() => 
      new(Input.GetAxis(Horizontal),0f);

    public void Tick()
    {
      IsAttackButtonPressedReactive.Value = IsAttackButtonPressed();
      IsCancelPressedReactive.Value = IsCancelPressed();
    }

    public void Initialize()
    {
      Active = true;
    }
  }
}