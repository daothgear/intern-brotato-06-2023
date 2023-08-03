using UnityEngine;

public class UINextLevel : MonoBehaviour {
  public void ClickNextButton() {
    ReferenceHolder.Ins.timeManager.CloseShopUI();
  }
}
