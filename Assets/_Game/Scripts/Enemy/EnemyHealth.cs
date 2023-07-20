using System.Runtime.ConstrainedExecution;
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

  private void MakeDead() {
    PlayerExp playerExp = FindObjectOfType<PlayerExp>();
    if (playerExp != null) {
      playerExp.AddExp(enemyExp);
    }

    ObjectPool.Instance.SpawnFromPool("Coin", transform.position, Quaternion.identity);
    ResetEnemy();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Player")) {
      Debug.Log("Enemy hit player");
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