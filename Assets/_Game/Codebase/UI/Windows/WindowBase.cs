using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI 
{
  public abstract class WindowBase : MonoBehaviour
  {
    [SerializeField] private Button closeButton;

    private void Awake() => 
      OnAwake();

    private void Start()
    {
      Initialize();
      SubscribeUpdates();
    }

    protected virtual void OnAwake()
    {
      closeButton.OnClickAsObservable().First()
        .Subscribe(_ => Cleanup()).AddTo(this);
    }
    protected virtual void Initialize(){}
    protected virtual void SubscribeUpdates(){}
    protected virtual void Cleanup() => Destroy(gameObject);
  }
}