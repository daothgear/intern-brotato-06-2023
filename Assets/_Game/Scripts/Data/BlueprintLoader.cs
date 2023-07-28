using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BlueprintLoader : MonoBehaviour {
  public CharacterLevelData characterLevelData;
  public WeaponLevelData weaponLevelData;
  public WaveData waveData;
  public EnemyData enemyData;

  [ContextMenu("OutputJson")]
  public void OutputJson() {
    string characterLevelJson = JsonUtility.ToJson(characterLevelData, true);
    string characterLevelPath = Path.Combine(Application.streamingAssetsPath,Constants.Data_Player);
    File.WriteAllText(characterLevelPath, characterLevelJson);

    string weaponLevelJson = JsonUtility.ToJson(weaponLevelData, true);
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