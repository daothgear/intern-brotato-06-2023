using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatText : MonoBehaviour {

  private void Start() {
    Invoke("DestroyText" ,0.3f);
  }

  private void DestroyText() {
    ObjectPool.Instance.ReturnToPool("CombatText" , gameObject);
    Debug.Log("Return text to pool");
  }
}
