using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData {
  public List<PlayerInfo> playerInfo;

  [Serializable]
  public class PlayerInfo {
    public int characterID;
    public int exp;
    public int maxHP;
    public float moveSpeed;
  }
}