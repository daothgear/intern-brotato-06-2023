using UnityEngine;

public class Enemy : MonoBehaviour
{
  private EnemyLoader enemyLoader;
  public float speed;
  public int damageEnemy;

  private PlayerHealth playerHealth;
  private Animator animator;
  public bool isFacingRight = true;

  public enum EnemyState
  {
    Idle,
    Walk,
    Attack,
    Dead
  }

  public EnemyState currentState;

  private void Start()
  {
    animator = GetComponentInChildren<Animator>();
    enemyLoader = GetComponent<EnemyLoader>();
    currentState = EnemyState.Idle;
    playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
  }

  private void Update()
  {
    switch ( currentState )
    {
      case EnemyState.Idle:
        Idle();
        break;
      case EnemyState.Walk:
        Walk();
        animator.SetTrigger("Walk");
        break;
      case EnemyState.Attack:
        Attack();
        break;
      case EnemyState.Dead:
        Dead();
        break;
    }
  }

  public void Flip()
  {
    isFacingRight = !isFacingRight;
    transform.Rotate(0f , 180f , 0f);
  }

  private void Idle()
  {
    currentState = EnemyState.Walk;
    if ( animator != null )
    {
      animator.SetTrigger("Walk");
    }
  }

  private void Walk()
  {
    if ( playerHealth != null )
    {
      transform.position = Vector3.MoveTowards(transform.position , playerHealth.transform.position , speed * Time.deltaTime);

      if ( transform.position.x > playerHealth.transform.position.x && isFacingRight )
      {
        Flip();
      }
      else if ( transform.position.x < playerHealth.transform.position.x && !isFacingRight )
      {
        Flip();
      }

      float distanceToPlayer = Vector3.Distance(transform.position , playerHealth.transform.position);
      if ( distanceToPlayer <= 1f )
      {
        currentState = EnemyState.Attack;
      }
    }
  }

  private void Attack()
  {
    if ( playerHealth != null )
    {
      playerHealth.TakeDamage(damageEnemy);
      currentState = EnemyState.Walk;
    }
  }

  private void Dead()
  {
    if ( animator != null )
    {
      animator.SetBool("Die" , true);
      ObjectPool.Instance.ReturnToPool("Enemy" , gameObject);
      Debug.Log("Return done");
    }
  }
}
