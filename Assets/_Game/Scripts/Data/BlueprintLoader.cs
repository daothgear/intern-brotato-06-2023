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

    public float refreshInterval = 5f;

    private Coroutine refreshCoroutine;

    [ContextMenu("OutputJson")]
    public void OutputJson()
    {
        string characterLevelJson = JsonUtility.ToJson(characterLevelData, true);
        File.WriteAllText(Application.dataPath + "/_Game/StreamingAssets/CharacterLevelData.json", characterLevelJson);

        string weaponLevelJson = JsonUtility.ToJson(weaponLevelData, true);
        File.WriteAllText(Application.dataPath + "/_Game/StreamingAssets/WeaponLevelData.json", weaponLevelJson);

        string waveDataJson = JsonUtility.ToJson(waveData, true);
        File.WriteAllText(Application.dataPath + "/_Game/StreamingAssets/WaveData.json", waveDataJson);

        string enemyDataJson = JsonUtility.ToJson(enemyData, true);
        File.WriteAllText(Application.dataPath + "/_Game/StreamingAssets/EnemyData.json", enemyDataJson);

        AssetDatabase.Refresh();
    }

    public void LoadJson()
    {
        string characterLevelPath = Application.dataPath + "/_Game/StreamingAssets/CharacterLevelData.json";
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