using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
  public EnemyInfo[] enemyInfo;
  [System.Serializable]
  public class EnemyInfo
  {
    public int enemyID;
    public float moveSpeed;
    public int maxHP;
    public int damage;
    public int expEnemy;
    // public int meleeAttackDamage;
    // public float meleeAttackRange;
    // public float meleeAttackSpeed;
    // public int gunAttackDamage;
    // public float gunAttackRange;
    // public float gunAttackSpeed;
    // public float laoAttackSpeed;
  }
}
