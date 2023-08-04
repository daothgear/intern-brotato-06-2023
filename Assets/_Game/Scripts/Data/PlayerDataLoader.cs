using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerDataLoader : InstanceStatic<PlayerDataLoader> {
  private PlayerData characterData;
  public float speed;
  public int maxHealth;
  public int characterLevel;
  public int maxExp;

  protected override void Awake() {
    ReadData();
  }

  public void ReadData() {
    string characterLevelPath = Path.Combine(Application.streamingAssetsPath, Constants.Data_Player);
    if (File.Exists(characterLevelPath)) {
      string characterLevelJson = File.ReadAllText(characterLevelPath);
      characterData = JsonConvert.DeserializeObject<PlayerData>(characterLevelJson);
    }
  }

  public void LoadCharacterInfo(int currentLevel) {
    foreach (var characterInfo in characterData.playerInfo) {
      if (characterInfo.characterID == currentLevel) {
        PlayerData.PlayerInfo currentPlayerInfo = characterInfo;
        Debug.Log("Character level data loaded successfully.");
        speed = currentPlayerInfo.moveSpeed;
        maxHealth = currentPlayerInfo.maxHP;
        characterLevel = currentPlayerInfo.characterID;
        maxExp = currentPlayerInfo.exp;
        break;
      }
    }
  }
}