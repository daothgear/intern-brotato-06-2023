using UnityEngine;

public class UICombatTextManager : MonoBehaviour {
  [SerializeField] private Transform canvasText;
  public void CreateUICombatText(Vector3 position, string text, Color color) {
    GameObject uiCombatTextObject = ObjectPool.Ins.SpawnFromPool(Constants.Tag_CombatText, position, Quaternion.identity);
    UICombatText uiCombatText = uiCombatTextObject.GetComponent<UICombatText>();
    uiCombatText.transform.SetParent(canvasText);
    uiCombatText.transform.localScale = Vector3.one;
    uiCombatText.SetUp(position, text, color);
  }
}
