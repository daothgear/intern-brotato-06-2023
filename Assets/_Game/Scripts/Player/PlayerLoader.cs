using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerLoader : Singleton<PlayerLoader> {
  public CharacterLevelData characterLevelData;
  public float speed;
  public int maxHealth;
  public int characterLevel;
  public int maxExp;

  protected override void Awake() {
    base.Awake();
    LoadCharacterInfo(1);
  }

  public void LoadCharacterInfo(int currentLevel) {
    string characterLevelPath = Path.Combine(Application.streamingAssetsPath, "CharacterLevelData.json");
    if (File.Exists(characterLevelPath)) {
      string characterLevelJson = File.ReadAllText(characterLevelPath);
      characterLevelData = JsonConvert.DeserializeObject<CharacterLevelData>(characterLevelJson);
      foreach (var characterInfo in characterLevelData.characterInfo) {
        if (characterInfo.characterID == currentLevel) {
          CharacterLevelData.CharacterInfo currentCharacterInfo = characterInfo;
          Debug.Log("Character level data loaded successfully.");
          speed = currentCharacterInfo.moveSpeed;
          maxHealth = currentCharacterInfo.maxHP;
          characterLevel = currentCharacterInfo.characterID;
          maxExp = currentCharacterInfo.exp;
          break;
        }
      }

      if (characterLevelData == null) {
        Debug.LogError("Character info not found for level: " + currentLevel);
      }
    }
    else {
      Debug.LogError("File not found: " + characterLevelPath);
    }
  }
}