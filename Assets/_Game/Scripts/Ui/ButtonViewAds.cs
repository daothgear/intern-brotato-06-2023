using UnityEngine;
using UnityEngine.UI;

public class ButtonViewAds : MonoBehaviour {
  [SerializeField] private Image image;

  private void OnValidate() {
    if (image == null) {
      image = GetComponent<Image>();
    }
  }
  
  public void ChangeColor() {
    if (ReferenceHolder.Ins.uiPlayAgain.currenttimeViewAds <= 0) {
      image.color = Color.gray;
    }
    else {
      image.color = Color.white;
    }
  }
}
