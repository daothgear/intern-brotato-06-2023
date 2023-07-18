using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
  private Enemy enemy;
  public int enemyExp;
  public float maxHealth;
  [SerializeField] private float currentHealth;
  [SerializeField] private bool drop;
  [SerializeField] private GameObject coinItem;
  private PlayerExp playerExp;

  private void Start()
  {
    enemy = GetComponent<Enemy>();
    currentHealth = maxHealth;
    Debug.Log("max hp:" + maxHealth);
    playerExp = FindAnyObjectByType<PlayerExp>();
  }

  public void TakeDamage(float dame)
  {
    currentHealth -= dame;
    if (currentHealth <= 0)
    {
      makeDead();
    }
  }

  public void makeDead()
  {
    playerExp.AddExp(enemyExp);
    if (drop)
    {
      ObjectPool.Instance.SpawnFromPool("Coin", gameObject.transform.position , Quaternion.identity);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      enemy.currentState = Enemy.EnemyState.Dead;
      makeDead();
    }
  }
}
