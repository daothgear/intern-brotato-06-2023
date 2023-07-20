using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerLoader : MonoBehaviour {
  private CharacterLevelData characterLevelData;
  private PlayerMove playerMove;
  private PlayerHealth playerHealth;
  private PlayerExp playerExp;

  private void Awake() {
    playerMove = GetComponent<PlayerMove>();
    playerHealth = GetComponent<PlayerHealth>();
    playerExp = GetComponent<PlayerExp>();
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
          playerMove.speed = currentCharacterInfo.moveSpeed;
          playerHealth.maxHealth = currentCharacterInfo.maxHP;
          playerExp.characterLevel = currentCharacterInfo.characterID;
          playerExp.maxExp = currentCharacterInfo.exp;
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