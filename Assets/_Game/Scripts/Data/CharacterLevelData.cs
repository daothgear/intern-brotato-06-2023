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

    public int CalculateMaxHealth( int level )
    {
      return maxHP + level - 1;
    }

    public int CalculateMaxExp( int level )
    {
      return exp + 10 * level;
    }

    public float CalculateSpeed( int level )
    {
      return moveSpeed + 0.25f * level;
    }
  }
}
