using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyDataLoader : Singleton<EnemyDataLoader> {
  private EnemyData enemyData;
  public float speed;
  public int maxHealth;
  public int damageEnemy;
  public int enemyExp;

  protected override void Awake() {
    base.Awake();
    LoadEnemyInfo(1);
  }
  
  public void LoadEnemyInfo(int enemyID) {
    string enemyDataPath = Path.Combine(Application.streamingAssetsPath, "EnemyData.json");
    if (File.Exists(enemyDataPath)) {
      string enemyDataJson = File.ReadAllText(enemyDataPath);
      enemyData = JsonConvert.DeserializeObject<EnemyData>(enemyDataJson);
      foreach (var enemyInfo in enemyData.enemyInfo) {
        if (enemyInfo.enemyID == enemyID) {
          EnemyData.EnemyInfo currentEnemyInfo = enemyInfo;
          speed = currentEnemyInfo.moveSpeed;
          maxHealth = currentEnemyInfo.maxHP;
          damageEnemy = currentEnemyInfo.damage;
          enemyExp = currentEnemyInfo.expEnemy;
          //Debug.Log("Enemy data loaded successfully.");
          break;
        }
      }
    }
    else {
      Debug.LogError("File not found: " + enemyDataPath);
    }
  }
}