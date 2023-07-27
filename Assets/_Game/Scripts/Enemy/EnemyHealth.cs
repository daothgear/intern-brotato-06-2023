using com.ootii.Messages;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
  [SerializeField] private Enemy enemy;
  
  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Instance;
  }
  private Vector3 startPosition;
  
  [SerializeField] private float currentHealth;
  private Animator animator;
  

  private void OnValidate() {
    if ( enemy == null) {
      enemy = GetComponent<Enemy>();
    }
  }
  
  private void Start() {
    startPosition = transform.position;
    currentHealth = enemyLoader.maxHealth;
    Debug.Log(enemyLoader.maxHealth);
  }

  public void MakeDead() {
    ObjectPool.Instance.ReturnToPool(Constants.Tag_Enemy,gameObject);
    Debug.Log("current state hien tai" + enemy.currentState);
    MessageDispatcher.SendMessage(Constants.Mess_addExp);
    ObjectPool.Instance.SpawnFromPool(Constants.Tag_Coin, transform.position, Quaternion.identity);
    ResetEnemy();
  }
  
  public void TakeDamage(int damage) {
    currentHealth -= damage;
    if (currentHealth <= 0) {
      enemy.currentState = Enemy.EnemyState.Dead;
      MakeDead();
    }
  }

  public void ResetEnemy() {
    ResetHealth();
    ResetEnemyState();
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
      }
    }

    if (collision.CompareTag("Player")) {
      ResetEnemy();
    }
  }
}