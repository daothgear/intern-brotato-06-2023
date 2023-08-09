using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyDataLoader : InstanceStatic<EnemyDataLoader> {
  private EnemyData enemyData;
  public float speed;
  public int maxHealth;
  public int damageEnemy;
  public int enemyExp;

  protected override void Awake() {
    ReadData();
  }

  private void ReadData() {
    string enemyDataPath = Path.Combine(Application.streamingAssetsPath, Constants.Data_Enemy);
    if (File.Exists(enemyDataPath)) {
      string enemyDataJson = File.ReadAllText(enemyDataPath);
      enemyData = JsonConvert.DeserializeObject<EnemyData>(enemyDataJson);
    }
  }

  public void LoadEnemyInfo(int enemyID) {
    foreach (var enemyInfo in enemyData.enemyInfo) {
      if (enemyInfo.enemyID == enemyID) {
        EnemyData.EnemyInfo currentEnemyInfo = enemyInfo;
        speed = currentEnemyInfo.moveSpeed;
        maxHealth = currentEnemyInfo.maxHP;
        damageEnemy = currentEnemyInfo.damage;
        enemyExp = currentEnemyInfo.expEnemy;
        break;
      }
    }
  }
}