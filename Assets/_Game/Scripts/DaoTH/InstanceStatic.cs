using System;

public class InstanceStatic<T> where T : InstanceStatic<T> {

  private static T instance;

  public static T Ins {
    get {
      if (instance == null) {
        instance = Activator.CreateInstance<T>();
        instance.Awake();
      }

      return instance;
    }
  }

  protected virtual void Awake() { }
}