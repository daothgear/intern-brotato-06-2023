using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] private EnemyState enemyState;
  [SerializeField] private float speed;
  private enum EnemyState
  {
    Idle,
    Walk,
    Attack,
    Dead
  }
  private void Start()
  {
    enemyState = EnemyState.Idle;
  }

  private void Update()
  {
    switch (enemyState)
    {
      case EnemyState.Idle:
        Idle();
        break;
      case EnemyState.Walk:
        Walk();
        break;
      case EnemyState.Attack:
        Attack();
        break;
      case EnemyState.Dead:
        Dead();
        break;
    }
  }

  private void Idle()
  {

  }

  private void Walk()
  {

  }

  private void Attack()
  {

  }

  private void Dead()
  {

  }
}
