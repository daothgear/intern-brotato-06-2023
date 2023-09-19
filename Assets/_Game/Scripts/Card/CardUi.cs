using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUi : MonoBehaviour
{
  public Text ButtonUplevel;
  public Text ButtonNextWave;
  public Text ButtonAddWeapon;
  public Text ButtonDoubleCoin;
  public TextMeshProUGUI textLevel;
  private Card card;

  private void OnValidate() {
    if (card == null) {
      card = GetComponent<Card>();
    }
  }

  public void Start() {
    textLevel.text = "Level: " + (card.randomLevel + 1);
    ButtonNextWave.text = (card.nextWave * 10).ToString();
    ButtonUplevel.text = (card.uplevel * 10).ToString();
    ButtonDoubleCoin.text = (card.doubleCoin * 10).ToString();
    ButtonAddWeapon.text = (card.addWeapon * 10).ToString();
  }
}
