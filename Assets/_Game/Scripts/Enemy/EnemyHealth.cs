using UnityEngine;
using com.ootii.Messages;
using TMPro;

public class EnemyHealth : MonoBehaviour, IPooledObject {
  [SerializeField] private Enemy enemy;
  private bool isAdd = true;

  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Ins;
  }

  private WeaponDataLoader weaponDataLoader {
    get => WeaponDataLoader.Ins;
  }
  
  [SerializeField] private float currentHealth;
  
  private void OnValidate() {
    if (enemy == null) {
      enemy = GetComponent<Enemy>();
    }
  }

  private void Start() {
    currentHealth = enemyLoader.maxHealth;
  }

  public void MakeDead() {
    ObjectPool.Ins.ReturnToPool(Constants.Tag_Enemy, gameObject);
    ObjectPool.Ins.enemyList.Remove(gameObject);
    ResetEnemy();
  }

  public void TakeDamage() {
    int damage = weaponDataLoader.weaponDamage;
    currentHealth -= damage;
    ReferenceHolder.Ins.combatTextManager.CreateUICombatText(transform.position, $"-{damage}", Color.black);
    // var go = ObjectPool.Instance.SpawnFromPool(Constants.Tag_CombatText, gameObject.transform.position , Quaternion.identity);
    // go.GetComponent<TextMeshProUGUI>().text = weaponDataLoader.weaponDamage.ToString();
    if (currentHealth <= 0) {
      if (isAdd == true) {
        MessageDispatcher.SendMessage(Constants.Mess_addExp);
        ObjectPool.Ins.SpawnFromPool(Constants.Tag_Coin, transform.position, Quaternion.identity);
        isAdd = false;
      }

      enemy.currentState = Enemy.EnemyState.Dead;
      Invoke("MakeDead", 1f);
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
    if (collision.CompareTag(Constants.Tag_Bullets)) {
      TakeDamage();
    }
  }

  public void OnObjectSpawn() {
    ResetHealth();
    ResetEnemyState();
  }
}