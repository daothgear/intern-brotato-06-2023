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
    int bulletCount = ObjectPool.Ins.bulletList.Count;

    for (int i = bulletCount - 1; i >= 0; i--) {
      GameObject bulletObject = ObjectPool.Ins.bulletList[i];
      Bullets bullet = bulletObject.GetComponent<Bullets>();
      bullet.UpdateBullet();
    }
  }

  private void Update() {
    int enemyCount = ObjectPool.Ins.enemyList.Count;

    for (int i = enemyCount - 1; i >= 0; i--) {
      GameObject enemyObject = ObjectPool.Ins.enemyList[i];
      Enemy enemy = enemyObject.GetComponent<Enemy>();
      enemy.UpdateState();
    }
  }
}