using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponLevelData
{
  [System.Serializable]
  public class WeaponInfo
  {
    public int weaponID;
    public int cost;
    public int damage;
    public float attackRange;
    public float attackSpeed;
    public int pierce;
    public float pierceDamageReduce;
  }

  public List<WeaponInfo> weaponInfo = new List<WeaponInfo>();
}
