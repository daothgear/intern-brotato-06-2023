using UnityEngine;

public class ReferenceHolder : MonoStatic<ReferenceHolder> {
  public PlayerCoin playerCoin;
  public Transform playerTran;
  public TimeManager timeManager;
  public PlayerExp playerExp;
  public PlayerHealth playerHealth;
  public PlayerWeapon playerWeapon;
  public Card card;
  public UiController uicontroller;
  public Player player;
  private void OnValidate() {
    // find and assign reference here
    if (playerCoin == null) { // example
      playerCoin = FindObjectOfType<PlayerCoin>();
      playerTran = playerCoin.gameObject.transform;
    }

    if (timeManager == null) {
      timeManager = FindObjectOfType<TimeManager>();
    }

    if (playerExp == null) {
      playerExp = FindObjectOfType<PlayerExp>();
    }

    if (playerWeapon == null) {
      playerWeapon = FindObjectOfType<PlayerWeapon>();
    }
    
    if (card == null) {
      card = FindObjectOfType<Card>();
    }

    if (uicontroller == null) {
      uicontroller = FindObjectOfType<UiController>();
    }

    if (player == null) {
      player = FindObjectOfType<Player>();
    }

    if (playerHealth == null) {
      playerHealth = FindObjectOfType<PlayerHealth>();
    }
  }
}