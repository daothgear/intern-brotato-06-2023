using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerDataLoader : InstanceStatic<PlayerDataLoader> {
  private PlayerData characterData;

  public void ReceiveData(string fileName, string jsonData)
  {
    if (fileName == Constants.Data_Player)
    {
      characterData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
    }
  }

  public PlayerData.PlayerInfo LoadCharacterInfo(int currentLevel) {
    foreach (PlayerData.PlayerInfo characterInfo in characterData.playerInfo) {
      if (characterInfo.characterID == currentLevel) {
        return characterInfo;
      }
    }
    return null;
  }

  public int GetLastPlayerID() {
    return characterData.playerInfo[characterData.playerInfo.Count - 1].characterID;
  }
}
