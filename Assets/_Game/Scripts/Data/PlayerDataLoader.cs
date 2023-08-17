using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerDataLoader : InstanceStatic<PlayerDataLoader> {
  private PlayerData characterData;
  public float speed;
  public int maxHealth;
  public int characterLevel;
  public int maxExp;

  public void ReceiveData(string fileName, string jsonData)
  {
    if (fileName == Constants.Data_Player)
    {
      characterData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
    }
  }

  public void LoadCharacterInfo(int currentLevel) {
    foreach (var characterInfo in characterData.playerInfo) {
      if (characterInfo.characterID == currentLevel) {
        PlayerData.PlayerInfo currentPlayerInfo = characterInfo;
        speed = currentPlayerInfo.moveSpeed;
        maxHealth = currentPlayerInfo.maxHP;
        characterLevel = currentPlayerInfo.characterID;
        maxExp = currentPlayerInfo.exp;
        break;
      }
    }
  }
}