using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICombatText : MonoBehaviour {
  [SerializeField] private TextMeshProUGUI damageText;
  [SerializeField] private Vector3 offset;
  [SerializeField] private float timeDestroy = 0.3f;
  private float startTime;
  private void Start() {
    startTime = Time.time;
  }

  public void SetUp(Vector3 position, string text, Color color) {
    transform.position = position + offset;
    damageText.text = text;
    damageText.color = color;
  }

  private void Update() {
    if (Time.time - startTime >= timeDestroy) {
      ObjectPool.Ins.ReturnToPool(Constants.Tag_CombatText, gameObject);
    }
  }
}
