using System;

[Serializable]
public class CharacterLevelData
{
  public CharacterInfo[] characterInfo;

  [Serializable]
  public class CharacterInfo
  {
    public int characterID;
    public int exp;
    public int maxHP;
    public float moveSpeed;
  }
}
