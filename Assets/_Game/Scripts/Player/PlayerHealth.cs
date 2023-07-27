using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth> {
  public int currentHealth;
  [SerializeField] private bool die = true;
  [SerializeField] private GameObject UiEndGame;
  [SerializeField] private Slider playerHealthSlider;
  [SerializeField] private Text textHealth;
  private PlayerExp playerExp;
  private PlayerLoader playerLoader { get => PlayerLoader.Instance; }

  private void OnValidate() {
    if (playerExp == null) {
      playerExp = GetComponent<PlayerExp>();
    }
  }
  
  private void Start() {
    UiEndGame.SetActive(die);
    currentHealth = playerLoader.maxHealth;
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

  public void TakeDamage(int damage) {
    if (currentHealth > 0) {
      currentHealth -= damage;
      if (currentHealth <= 0) {
        currentHealth = 0;
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
}