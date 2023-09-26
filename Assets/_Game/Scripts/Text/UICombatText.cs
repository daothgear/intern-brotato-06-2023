using System.Collections;
using UnityEngine;

public class UICombatText : MonoBehaviour {
  private void OnEnable() {
    StartCoroutine(ReturnToPoolAfterDelay(0.5f));
  }

  private IEnumerator ReturnToPoolAfterDelay(float delay) {
    yield return new WaitForSeconds(delay);
    ObjectPool.Ins.ReturnToPool(Constants.Tag_CombatText, gameObject);
  }
}