using com.ootii.Messages;
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
    player.currentHealth = player.playerLoader.maxHealth;
    playerUi.UpdateHealthUI();
    MessageDispatcher.AddListener(Constants.Mess_playerTakeDamage, TakeDamage);
    MessageDispatcher.AddListener(Constants.Mess_resetHealth, ResetHealth);
  }

  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_playerTakeDamage, TakeDamage);
    MessageDispatcher.RemoveListener(Constants.Mess_resetHealth, ResetHealth);
  }

  public void TakeDamage(IMessage img) {
    if (player.currentHealth > 0) {
      player.currentHealth -= player.enemyLoader.damageEnemy;
      if (player.currentHealth <= 0) {
        player.currentHealth = 0;
        MessageDispatcher.SendMessage(Constants.Mess_playerDie);
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

  private void ResetHealth(IMessage img) {
    player.currentHealth = player.playerLoader.maxHealth;
    playerUi.UpdateHealthUI();
  }
}