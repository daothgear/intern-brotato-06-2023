using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  public int maxHealth;
  public int currentHealth;

  [SerializeField] private Slider playerHealthSlider;
  [SerializeField] private Text textHealth;

  private void Start()
  {
    currentHealth = maxHealth;
    UpdateHealthUI();
  }

  private void UpdateHealthUI()
  {
    playerHealthSlider.maxValue = maxHealth;
    playerHealthSlider.value = currentHealth;
    textHealth.text = currentHealth + "/" + maxHealth;
  }

  public void TakeDamage( int damage )
  {
    if ( currentHealth > 0 )
    {
      currentHealth -= damage;
      if ( currentHealth <= 0 )
      {
        currentHealth = 0;
        Die();
      }
      UpdateHealthUI();
    }
  }

  private void Die()
  {
    Debug.Log("Player died!");
  }

  private void OnTriggerEnter2D( Collider2D collision )
  {
    if ( collision.CompareTag("Enemy") )
    {
      Enemy enemy = collision.GetComponent<Enemy>();
      if ( enemy != null )
      {
        enemy.currentState = Enemy.EnemyState.Attack;
        TakeDamage(enemy.damageEnemy);
      }
    }
  }
}
