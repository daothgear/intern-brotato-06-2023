using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    private Type currentType;
    private enum Type
    {
        TankEnemy,
        SuicideEnemy,
        RangedEnemy
    }
}
