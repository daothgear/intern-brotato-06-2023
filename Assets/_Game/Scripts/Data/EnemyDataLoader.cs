using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyDataLoader : InstanceStatic<EnemyDataLoader> {
  private EnemyData enemyData;

  public void ReceiveData(string fileName, string jsonData)
  {
    if (fileName == Constants.Data_Enemy)
    {
      enemyData = JsonConvert.DeserializeObject<EnemyData>(jsonData); ;
    }
  }

  public EnemyData.EnemyInfo LoadEnemyInfo(int enemyID) {
    foreach (EnemyData.EnemyInfo enemyInfo in enemyData.enemyInfo) {
      if (enemyInfo.enemyID == enemyID) {
        return enemyInfo;
      }
    }
    return null;
  }
}