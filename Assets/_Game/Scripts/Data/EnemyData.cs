using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
  [System.Serializable]
  public class EnemyInfo
  {
    public int enemyID;
    public float moveSpeed;
    public int maxHP;
    public int meleeAttackDamage;
    public float meleeAttackRange;
    public float meleeAttackSpeed;
    public int gunAttackDamage;
    public float gunAttackRange;
    public float gunAttackSpeed;
    public float laoAttackSpeed;
  }

  public List<EnemyInfo> enemyInfo = new List<EnemyInfo>();
}
