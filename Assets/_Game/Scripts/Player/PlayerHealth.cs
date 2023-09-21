using UnityEngine;

public class PlayerHealth : MonoBehaviour {
  private Player player;
  private PlayerUi playerUi;

  private void OnValidate() {
    if (player == null) {
      player = GetComponent<Player>();
    }

    if (playerUi == null) {
      playerUi = GetComponent<PlayerUi>();
    }
  }

  private void Start() {
    ReferenceHolder.Ins.uicontroller.UiEndGame.SetActive(player.die);
    player.UpdateData();
    player.currentHealth = player.maxHealth;
    playerUi.UpdateHealthUI();
  }
  
  public void TakeDamage(int damage) {
    if (player.currentHealth > 0) {
      //player.currentHealth -= player.enemyLoader.damageEnemy;
      player.currentHealth -= damage;
      if (player.currentHealth <= 0) {
        player.currentHealth = 0;
        ReferenceHolder.Ins.playerCoin.ResetData();
        ReferenceHolder.Ins.playerExp.ResetLevel();
        ReferenceHolder.Ins.playerWeapon.ResetWeapon();
        ReferenceHolder.Ins.timeManager.Stoptime();
        Destroy(gameObject);
        Die();
      }

      playerUi.UpdateHealthUI();
    }
  }

  private void Die() {
    player.die = true;
    ReferenceHolder.Ins.uicontroller.UiEndGame.SetActive(player.die);
  }

  public void ResetHealth(int maxHealth) {
    player.currentHealth = maxHealth;
    playerUi.UpdateHealthUI();
  }
}