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

  public void ReceiveData(string fileName, string jsonData)
  {
    if (fileName == Constants.Data_Enemy)
    {
      enemyData = JsonConvert.DeserializeObject<EnemyData>(jsonData); ;
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