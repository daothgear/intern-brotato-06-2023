using UnityEngine;

public class EnemyHealth : MonoBehaviour {
  [SerializeField] private Enemy enemy;
  [SerializeField] private PlayerExp playerExp;
  
  private EnemyLoader enemyLoader;
  private Vector3 startPosition;
  
  [SerializeField] private float currentHealth;
  private Animator animator;
  

  private void OnValidate() {
    if ( enemy == null) {
      enemy = GetComponent<Enemy>();
    }
  }

  private void Awake() {
    enemyLoader = EnemyLoader.Instance;
    playerExp = PlayerExp.Instance;
  }

  private void Start() {
    startPosition = transform.position;
    currentHealth = enemyLoader.maxHealth;
    Debug.Log(enemyLoader.maxHealth);
  }

  public void MakeDead() {
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

    if (collision.CompareTag("Player")) {
      ResetEnemy();
    }
  }
}