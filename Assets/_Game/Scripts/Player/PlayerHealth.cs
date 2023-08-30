using System;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
  public int currentHealth;
  public bool die = true;
  [SerializeField] private GameObject UiEndGame;
  [SerializeField] private Slider playerHealthSlider;
  [SerializeField] private Text textHealth;
  private PlayerExp playerExp;

  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Ins;
  }

  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Ins;
  }

  private void OnValidate() {
    if (playerExp == null) {
      playerExp = GetComponent<PlayerExp>();
    }
  }

  private void Start() {
    PlayerDataLoader.Ins.LoadCharacterInfo(playerLoader.characterLevel);
    currentHealth = playerLoader.maxHealth;
    UpdateHealthUI(null);
    UiEndGame.SetActive(die);
    MessageDispatcher.AddListener(Constants.Mess_playerTakeDamage, TakeDamage);
    MessageDispatcher.AddListener(Constants.Mess_resetHealth, ResetHealth);
    MessageDispatcher.AddListener(Constants.Mess_UpdateUIHealth, UpdateHealthUI);
  }
  
  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_playerTakeDamage, TakeDamage);
    MessageDispatcher.RemoveListener(Constants.Mess_resetHealth, ResetHealth);
  }
  
  public void UpdateHealthUI(IMessage img) {
    playerHealthSlider.maxValue = playerLoader.maxHealth;
    playerHealthSlider.value = currentHealth;
    textHealth.text = currentHealth + "/" + playerLoader.maxHealth;
  }

  public void TakeDamage(IMessage img) {
    if (currentHealth > 0) {
      currentHealth -= enemyLoader.damageEnemy;
      if (currentHealth <= 0) {
        currentHealth = 0;
        MessageDispatcher.SendMessage(Constants.Mess_playerDie);
        Destroy(gameObject);
        Die();
      }
      MessageDispatcher.SendMessage(Constants.Mess_UpdateUIHealth);
    }
  }

  private void Die() {
    die = true;
    UiEndGame.SetActive(die);
    ReferenceHolder.Ins.timeManager.ClearEnemies();
  }

  private void ResetHealth(IMessage img) {
    currentHealth = playerLoader.maxHealth;
    MessageDispatcher.SendMessage(Constants.Mess_UpdateUIHealth);
  }
}