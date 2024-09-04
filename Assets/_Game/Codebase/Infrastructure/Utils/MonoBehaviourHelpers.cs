using UnityEngine;

namespace Infrastructure.Helpers
{
  public static class MonoBehaviourHelpers
  {
    public static void DestroyGO(this Object obj) => Object.Destroy(obj);
    public static void DestroyGO(this Object obj, float delay) => Object.Destroy(obj, delay);
    
    public static Vector3 WorldPosition(this MonoBehaviour obj) => obj.transform.position;
    public static Vector3 LocalPosition(this MonoBehaviour obj) => obj.transform.localPosition;

    public static bool ActiveInHierarchy(this MonoBehaviour obj) =>
      !obj.IsNullOrDestroyed() && obj.isActiveAndEnabled &&
      obj.gameObject.activeInHierarchy;

    public static bool IsNullOrDestroyed(this object obj) =>
      obj == null || obj.Equals(null);
  }
}