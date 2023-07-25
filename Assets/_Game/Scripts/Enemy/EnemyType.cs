using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour {
  private enum Type {
    TankEnemy,
    SuicideEnemy,
    RangedEnemy
  }
  private Type currentType;
}