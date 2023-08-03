using UnityEngine;

public class MonoStatic<T> : MonoBehaviour where T : MonoStatic<T> {
  public static T Ins;

  protected virtual void Awake() {
    Ins = this as T;
  }
}