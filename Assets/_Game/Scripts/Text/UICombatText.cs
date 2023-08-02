using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICombatText : MonoBehaviour {
  [SerializeField] private TextMeshProUGUI damageText;
  [SerializeField] private Vector3 offset;
  [SerializeField] private float timeDestroy = 0.3f;
  public void SetUp(Vector3 position , string text , Color color) {
    transform.position = position + offset;
    damageText.text = text;
    damageText.color = color;
    Destroy(gameObject,timeDestroy);
  }
}