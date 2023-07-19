using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  private Enemy enemy;
  private Vector3 startPosition;
  private Quaternion startRotation;
  public int enemyExp;
  public float maxHealth;
  public float currentHealth;
  private Animator animator;

  private void Start()
  {
    enemy = GetComponent<Enemy>();
    startPosition = transform.position;
    startRotation = transform.rotation;
    currentHealth = maxHealth;
  }

  private void MakeDead()
  {
    currentHealth = 0;
    Debug.Log("mau ve 0 " + currentHealth);
    PlayerExp playerExp = FindObjectOfType<PlayerExp>();
    if ( playerExp != null )
    {
      playerExp.AddExp(enemyExp);
    }

    ObjectPool.Instance.SpawnFromPool("Coin" , transform.position , Quaternion.identity);
    ResetEnemy();
  }

  private void OnTriggerEnter2D( Collider2D collision )
  {
    if ( collision.CompareTag("Player") )
    {
      MakeDead();
    }
  }

  public void ResetEnemy()
  {
    ResetHealth();
    ResetEnemyState();
    ObjectPool.Instance.ReturnToPool("Enemy" , gameObject);
  }

  private void ResetEnemyState()
  {
    enemy.currentState = Enemy.EnemyState.Idle;
  }

  private void ResetHealth()
  {
    currentHealth = maxHealth;
  }
}
