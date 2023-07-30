using UnityEngine;
using com.ootii.Messages;

public class EnemyHealth : MonoBehaviour, IPooledObject {
  [SerializeField] private Enemy enemy;
  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Instance;
  }

  private WeaponDataLoader weaponDataLoader {
    get => WeaponDataLoader.Instance;
  }

  [SerializeField] private float currentHealth;

  [SerializeField] private GameObject combatTextPrefab;

  private void OnValidate() {
    if (enemy == null) {
      enemy = GetComponent<Enemy>();
    }

  }

  private void Start() {
    currentHealth = enemyLoader.maxHealth;
    MessageDispatcher.AddListener(Constants.Mess_enemyTakeDamage,TakeDamage);
  }

  public void MakeDead() {
    ObjectPool.Instance.ReturnToPool(Constants.Tag_Enemy,gameObject);
    Debug.Log("current state hien tai" + enemy.currentState);
    MessageDispatcher.SendMessage(Constants.Mess_addExp);
    ObjectPool.Instance.SpawnFromPool(Constants.Tag_Coin, transform.position, Quaternion.identity);
    ResetEnemy();
  }

  public void TakeDamage(IMessage img) {
    int damage = weaponDataLoader.weaponDamage;
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
    if (collision.CompareTag("Player")) {
      ResetEnemy();
    }
  }

  public void OnObjectSpawn() {
    ResetHealth();
    ResetEnemyState();
  }
}
