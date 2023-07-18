using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  private void OnValidate()
  {
    enemy = GetComponent<Enemy>();
  }

  public int maxHealth;
  private int currentHealth;

  [SerializeField] private Slider playerHealthSlider;
  [SerializeField] private Text textHealth;

  public Enemy enemy;

  private void Start()
  {
    currentHealth = maxHealth;
    UpdateHealthUI();
  }

  private void Update()
  {
    UpdateHealthUI();
  }

  public void ReceiveDamage(int damage)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      MakeDead();
    }
  }

  private void UpdateHealthUI()
  {
    playerHealthSlider.maxValue = maxHealth;
    playerHealthSlider.value = currentHealth;
    textHealth.text = currentHealth + "/" + maxHealth;
  }

  private void MakeDead()
  {
    gameObject.SetActive(false);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Enemy")
    {
      ReceiveDamage(enemy.damageEnemy);
    }
  }
}
