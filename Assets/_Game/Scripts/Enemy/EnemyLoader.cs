using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyLoader : MonoBehaviour
{
  private EnemyLoader enemyLoader;
  private EnemyData enemyData;
  private Enemy enemy;
  private EnemyHealth enemyHealth;
  private void Awake()
  {
    enemy = GetComponent<Enemy>();
    enemyHealth = GetComponent<EnemyHealth>();
    LoadEnemyInfo(1);
  }

  public void LoadEnemyInfo(int enemyID)
  {
    string enemyDataPath = Path.Combine(Application.streamingAssetsPath, "EnemyData.json");
    if (File.Exists(enemyDataPath))
    {
      string enemyDataJson = File.ReadAllText(enemyDataPath);
      enemyData = JsonConvert.DeserializeObject<EnemyData>(enemyDataJson);

      foreach (var enemyInfo in enemyData.enemyInfo)
      {
        if (enemyInfo.enemyID == enemyID)
        {
          EnemyData.EnemyInfo currentEnemyInfo = enemyInfo;
          enemy.speed = currentEnemyInfo.moveSpeed;
          enemyHealth.maxHealth = currentEnemyInfo.maxHP;
          enemy.damage = currentEnemyInfo.damage;
          Debug.Log("Enemy data loaded successfully.");
          // meleeAttackRange = currentEnemyInfo.meleeAttackRange;
          // meleeAttackSpeed = currentEnemyInfo.meleeAttackSpeed;
          // gunAttackDamage = currentEnemyInfo.gunAttackDamage;
          // gunAttackRange = currentEnemyInfo.gunAttackRange;
          // gunAttackSpeed = currentEnemyInfo.gunAttackSpeed;
          // laoAttackSpeed = currentEnemyInfo.laoAttackSpeed;
          break;
        }
      }

      if (enemyData == null)
      {
        Debug.LogError("Enemy info not found for enemy ID: " + enemyID);
      }
    }
    else
    {
      Debug.LogError("File not found: " + enemyDataPath);
    }
  }
}
