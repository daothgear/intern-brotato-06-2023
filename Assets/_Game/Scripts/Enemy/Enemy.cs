using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyLoader
{
  private EnemyLoader enemyLoader;
  public float speed;
  [SerializeField] private EnemyState currentState;
  [SerializeField] private GameObject player;

  private bool isFacingRight = true;
  private enum EnemyState
  {
    Idle,
    Walk,
    Attack,
    Dead
  }
  private void Start()
  {
    enemyLoader = GetComponent<EnemyLoader>();
    currentState = EnemyState.Idle;
  }

  private void Update()
  {
    switch (currentState)
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

  private void Flip()
  {
    isFacingRight = !isFacingRight;
    transform.Rotate(0f, 180f, 0f);
  }
  private void Idle()
  {
    currentState = EnemyState.Walk;
  }

  private void Walk()
  {
    speed = enemyLoader.moveSpeed;
    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

    if (transform.position.x > player.transform.position.x && isFacingRight)
    {
      Flip();
    }
    else if (transform.position.x < player.transform.position.x && !isFacingRight)
    {
      Flip();
    }
  }

  private void Attack()
  {

  }

  private void Dead()
  {

  }
}
