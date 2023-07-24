using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
  private Enemy enemy;
  private EnemyLoader enemyLoader;
  private Vector3 startPosition;
  public float currentHealth;
  private Animator animator;

  private void Awake() {
    enemyLoader = EnemyLoader.Instance;
  }

  private void Start() {
    Debug.Log(enemyLoader.maxHealth);
    enemy = GetComponent<Enemy>();
    startPosition = transform.position;
    currentHealth = enemyLoader.maxHealth;
    Debug.Log(enemyLoader.maxHealth);
  }

  public void MakeDead() {
    PlayerExp playerExp = FindObjectOfType<PlayerExp>();
    if (playerExp != null) {
      playerExp.AddExp(enemyLoader.enemyExp);
    }

    ObjectPool.Instance.SpawnFromPool("Coin", transform.position, Quaternion.identity);
    ResetEnemy();
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
    currentHealth = enemyLoader.maxHealth;
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
}