using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyLoader : MonoBehaviour
{
  private EnemyLoader enemyLoader;
  private EnemyData enemyData;

  //Data
  public float moveSpeed;
  [SerializeField] private int maxHP;
  [SerializeField] private int meleeAttackDamage;
  [SerializeField] private float meleeAttackRange;
  [SerializeField] private float meleeAttackSpeed;
  [SerializeField] private int gunAttackDamage;
  [SerializeField] private float gunAttackRange;
  [SerializeField] private float gunAttackSpeed;
  [SerializeField] private float laoAttackSpeed;
  [SerializeField] private int currentLevel = 1;

  private void Awake()
  {
    LoadEnemyInfo(currentLevel);
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
          Debug.Log("Enemy data loaded successfully.");
          moveSpeed = currentEnemyInfo.moveSpeed;
          maxHP = currentEnemyInfo.maxHP;
          meleeAttackDamage = currentEnemyInfo.meleeAttackDamage;
          meleeAttackRange = currentEnemyInfo.meleeAttackRange;
          meleeAttackSpeed = currentEnemyInfo.meleeAttackSpeed;
          gunAttackDamage = currentEnemyInfo.gunAttackDamage;
          gunAttackRange = currentEnemyInfo.gunAttackRange;
          gunAttackSpeed = currentEnemyInfo.gunAttackSpeed;
          laoAttackSpeed = currentEnemyInfo.laoAttackSpeed;
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
