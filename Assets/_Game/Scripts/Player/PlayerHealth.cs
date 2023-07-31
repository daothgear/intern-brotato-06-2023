using System;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
  public int currentHealth;
  [SerializeField] private bool die = true;
  [SerializeField] private GameObject UiEndGame;
  [SerializeField] private Slider playerHealthSlider;
  [SerializeField] private Text textHealth;
  private PlayerExp playerExp;

  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Instance;
  }

  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Instance;
  }

  private void OnValidate() {
    if (playerExp == null) {
      playerExp = GetComponent<PlayerExp>();
    }
  }

  private void Start() {
    UiEndGame.SetActive(die);
    currentHealth = playerLoader.maxHealth;
    MessageDispatcher.AddListener(Constants.Mess_playerTakeDamage, TakeDamage);
    MessageDispatcher.AddListener(Constants.Mess_resetHealth, ResetHealth);
  }

  private void FixUpdate() {
    playerLoader.maxHealth = playerLoader.characterLevel + playerLoader.maxHealth;
  }

  private void Update() {
    UpdateHealthUI();
  }

  public void UpdateHealthUI() {
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
        Die();
      }

      UpdateHealthUI();
    }
  }

  private void Die() {
    Debug.Log("Player died!");
    die = true;
    UiEndGame.SetActive(die);
  }

  private void ResetHealth(IMessage img) {
    currentHealth = playerLoader.maxHealth;
    UpdateHealthUI();
  }
}