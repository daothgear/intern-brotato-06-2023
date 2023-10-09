using UnityEngine;

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
    currentHealth = enemy.enemyInfo.maxHP;
  }

  public void MakeDead() {
    ResetEnemy();
    ObjectPool.Ins.ReturnToPool(Constants.Tag_Enemy, gameObject);
    ObjectPool.Ins.enemyList.Remove(gameObject);
  }

  public void TakeDamage(int weaponDamage) {
    currentHealth -= weaponDamage;
    ShowDamage(weaponDamage.ToString());
    if (currentHealth <= 0) {
      if (isAdd == true) {
        ReferenceHolder.Ins.playerExp.AddExp(enemy.enemyInfo.expEnemy);
        ObjectPool.Ins.SpawnFromPool(Constants.Tag_Coin, transform.position, Quaternion.identity);
        isAdd = false;
      }

      enemy.currentState = Enemy.EnemyState.Dead;
      Invoke("MakeDead", 0.5f);
    }
  }

  public void ShowDamage(string text) {
    Vector3 combatTextPosition = transform.position + new Vector3(0f,2f,0f);
    GameObject combattext = ObjectPool.Ins.SpawnFromPool(Constants.Tag_CombatText, combatTextPosition, Quaternion.identity);
    combattext.GetComponent<TextMesh>().text = text;
  }

  public void ResetEnemy() {
    ResetHealth();
    ResetEnemyState();
  }

  private void ResetEnemyState() {
    enemy.currentState = Enemy.EnemyState.Walk;
  }

  private void ResetHealth() {
    currentHealth = enemy.enemyInfo.maxHP;
  }

  public void OnObjectSpawn() {
    ResetHealth();
    ResetEnemyState();
  }
}
