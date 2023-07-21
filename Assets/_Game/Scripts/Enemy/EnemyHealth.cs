using UnityEngine;

public class EnemyHealth : MonoBehaviour {
  private Enemy enemy;
  private Vector3 startPosition;
  public int enemyExp;
  public float maxHealth;
  public float currentHealth;
  private Animator animator;

  private void Start() {
    enemy = GetComponent<Enemy>();
    startPosition = transform.position;
    currentHealth = maxHealth;
  }

  public void MakeDead() {
    PlayerExp playerExp = FindObjectOfType<PlayerExp>();
    if (playerExp != null) {
      playerExp.AddExp(enemyExp);
    }

    ObjectPool.Instance.SpawnFromPool("Coin", transform.position, Quaternion.identity);
    ResetEnemy();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Bullet")) {
      Bullets bullet = collision.GetComponent<Bullets>();
      if (bullet != null) {
        TakeDamage(bullet.damage);
        Destroy(collision.gameObject); 
      }
    }
  }

  public void TakeDamage(int damage) {
    currentHealth -= damage;
    if (currentHealth <= 0) {
      MakeDead();
    }
  }

  public void ResetEnemy() {
    ResetHealth();
    ResetEnemyState();
    ObjectPool.Instance.ReturnToPool("Enemy", gameObject);
  }

  private void ResetEnemyState() {
    enemy.currentState = Enemy.EnemyState.Idle;
  }

  private void ResetHealth() {
    currentHealth = maxHealth;
  }
}