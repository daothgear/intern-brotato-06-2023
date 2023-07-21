using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
  private Enemy enemy;
  private Vector3 startPosition;
  public int enemyExp;
  public float maxHealth;
  public float currentHealth;
  private Animator animator;
  private Bullets bullet;

  private void Awake() {
    bullet = FindObjectOfType<Bullets>();
  }

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
    if (collision.CompareTag("Bullet")) {
      if (bullet == null) {
        Debug.Log("Null");
      }
    }

    if (collision.CompareTag("Player")) {
      MakeDead();
    }
  }

  public void TakeDamage(float damage) {
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