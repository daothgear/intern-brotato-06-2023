using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Serialization;

public class BlueprintLoader : MonoBehaviour {
  public PlayerData playerData;
  public WeaponData weaponData;
  public WaveData waveData;
  public EnemyData enemyData;

  [ContextMenu("OutputJson")]
  public void OutputJson() {
    string characterLevelJson = JsonUtility.ToJson(playerData, true);
    string characterLevelPath = Path.Combine(Application.streamingAssetsPath,Constants.Data_Player);
    File.WriteAllText(characterLevelPath, characterLevelJson);

    string weaponLevelJson = JsonUtility.ToJson(weaponData, true);
    string weaponLevelPath = Path.Combine(Application.streamingAssetsPath, Constants.Data_Weapon);
    File.WriteAllText(weaponLevelPath, weaponLevelJson);

    string waveDataJson = JsonUtility.ToJson(waveData, true);
    string waveDataPath = Path.Combine(Application.streamingAssetsPath, Constants.Data_Wave);
    File.WriteAllText(waveDataPath, waveDataJson);

    string enemyDataJson = JsonUtility.ToJson(enemyData, true);
    string enemyDataPath = Path.Combine(Application.streamingAssetsPath, Constants.Data_Enemy);
    File.WriteAllText(enemyDataPath, enemyDataJson);
  }
}