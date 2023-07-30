using UnityEngine;
using com.ootii.Messages;

public class EnemyHealth : MonoBehaviour, IPooledObject {
  [SerializeField] private Enemy enemy;
  private bool isAdd = true;
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
  }

  public void MakeDead() {
    ObjectPool.Instance.ReturnToPool(Constants.Tag_Enemy,gameObject);
    ResetEnemy();
  }

  public void TakeDamage() {
    int damage = weaponDataLoader.weaponDamage;
    currentHealth -= damage;
    if (currentHealth <= 0) {
      if(isAdd == true) {
        MessageDispatcher.SendMessage(Constants.Mess_addExp);
        ObjectPool.Instance.SpawnFromPool(Constants.Tag_Coin, transform.position, Quaternion.identity);
        isAdd = false;
      }
      enemy.currentState = Enemy.EnemyState.Dead;
      Invoke("MakeDead",1f);

     //MakeDead();
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
    if (collision.CompareTag(Constants.Tag_Bullets)){
      TakeDamage();
    }

    if (collision.CompareTag("Player")) {
      ResetEnemy();
    }
  }

  public void OnObjectSpawn() {
    ResetHealth();
    ResetEnemyState();
  }
}
