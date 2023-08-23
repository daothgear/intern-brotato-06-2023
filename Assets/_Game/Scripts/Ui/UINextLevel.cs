using UnityEngine;

public class UINextLevel : MonoBehaviour {
  [SerializeField] private GameObject showWeapon;
  public void ClickNextButton() {
    ReferenceHolder.Ins.timeManager.CloseShopUI();
  }

  public void OpenShowShop() {
    showWeapon.SetActive(true);
  }

  public void CloseShowShop() {
    showWeapon.SetActive(false);
  }
}
