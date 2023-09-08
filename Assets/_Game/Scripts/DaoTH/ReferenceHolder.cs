using UnityEngine;

public class ReferenceHolder : MonoStatic<ReferenceHolder> {
  public PlayerCoin playerCoin;
  public Transform playerTran;
  public TimeManager timeManager;
  public UICombatTextManager combatTextManager;
  public PlayerExp playerExp;
  public PlayerWeapon playerWeapon;
  
  private void OnValidate() {
    // find and assign reference here
    if (playerCoin == null) { // example
      playerCoin = FindObjectOfType<PlayerCoin>();
      playerTran = playerCoin.gameObject.transform;
    }

    if (timeManager == null) {
      timeManager = FindObjectOfType<TimeManager>();
    }

    if (combatTextManager == null) {
      combatTextManager = FindObjectOfType<UICombatTextManager>();
    }

    if (playerExp == null) {
      playerExp = FindObjectOfType<PlayerExp>();
    }

    if (playerWeapon == null) {
      playerWeapon = FindObjectOfType<PlayerWeapon>();
    }
  }
}