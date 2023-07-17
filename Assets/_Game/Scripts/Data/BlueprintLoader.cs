using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BlueprintLoader : MonoBehaviour
{
  public CharacterLevelData characterLevelData;
  public WeaponLevelData weaponLevelData;
  public WaveData waveData;
  public EnemyData enemyData;

  [ContextMenu("OutputJson")]
  public void OutputJson()
  {
    string characterLevelJson = JsonUtility.ToJson(characterLevelData, true);
    string characterLevelPath = Path.Combine(Application.streamingAssetsPath, "CharacterLevelData.json");
    File.WriteAllText(characterLevelPath, characterLevelJson);

    string weaponLevelJson = JsonUtility.ToJson(weaponLevelData, true);
    string weaponLevelPath = Path.Combine(Application.streamingAssetsPath, "WeaponLevelData.json");
    File.WriteAllText(weaponLevelPath, weaponLevelJson);

    string waveDataJson = JsonUtility.ToJson(waveData, true);
    string waveDataPath = Path.Combine(Application.streamingAssetsPath, "WaveData.json");
    File.WriteAllText(waveDataPath, waveDataJson);

    string enemyDataJson = JsonUtility.ToJson(enemyData, true);
    string enemyDataPath = Path.Combine(Application.streamingAssetsPath, "EnemyData.json");
    File.WriteAllText(enemyDataPath, enemyDataJson);
  }

  public void LoadJson()
  {
    string characterLevelPath = Path.Combine(Application.streamingAssetsPath, "CharacterLevelData.json");
    if (File.Exists(characterLevelPath))
    {
      string characterLevelJson = File.ReadAllText(characterLevelPath);
      JsonUtility.FromJsonOverwrite(characterLevelJson, characterLevelData);
      Debug.Log(characterLevelJson);
      Debug.Log("Character level data loaded successfully.");
    }
    else
    {
      Debug.LogError("File not found: " + characterLevelPath);
    }
  }
}
