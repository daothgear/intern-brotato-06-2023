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
  public PlayerUi playerUi;
  public UiPlayAgain uiPlayAgain;
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

    if (playerUi == null) {
      playerUi = FindObjectOfType<PlayerUi>();
    }

    if (uiPlayAgain == null) {
      uiPlayAgain = FindObjectOfType<UiPlayAgain>();
    }
  }

  private void FixedUpdate() {
    foreach (GameObject bulletObject in ObjectPool.Ins.bulletList) {
      Bullets bullet = bulletObject.GetComponent<Bullets>();
      bullet.UpdateBullet();
    }
  }

  private void Update() {
    foreach (GameObject enemyObject in ObjectPool.Ins.enemyList) {
      Enemy enemy = enemyObject.GetComponent<Enemy>();
      enemy.UpdateState();
    }
  }
}