using UnityEngine;

public class Enemy : MonoBehaviour {
  public enum EnemyState {
    Idle,
    Walk,
    Attack,
    Dead
  }

  private EnemyLoader enemyLoader;
  public EnemyState currentState;

  [SerializeField] private PlayerHealth playerHealth;
  [SerializeField] private Animator animator;
  
  private bool isFacingRight;

  private void OnValidate() {
    if (animator == null) {
      animator = GetComponentInChildren<Animator>();
    }
  }

  private void Awake() {
    enemyLoader = EnemyLoader.Instance;
    playerHealth = PlayerHealth.Instance;
  }

  private void Start() {
    currentState = EnemyState.Idle;
  }

  private void Update() {
    switch (currentState) {
      case EnemyState.Idle:
        Idle();
        break;
      case EnemyState.Walk:
        Walk();
        break;
      case EnemyState.Dead:
        Dead();
        break;
    }
  }

  public void Flip() {
    isFacingRight = !isFacingRight;
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
  }

  private void Idle() {
    animator.SetTrigger("Walk");
    Invoke("TransitionToWalk", 0.5f);
  }

  private void TransitionToWalk() {
    currentState = EnemyState.Walk;
  }

  public void Walk() {
    if (playerHealth != null) {
      transform.position =
          Vector3.MoveTowards(transform.position, playerHealth.transform.position, enemyLoader.speed * Time.deltaTime);
      if (transform.position.x > playerHealth.transform.position.x && isFacingRight) {
        Flip();
      }
      else if (transform.position.x < playerHealth.transform.position.x && !isFacingRight) {
        Flip();
      }

      float distanceToPlayer = Vector3.Distance(transform.position, playerHealth.transform.position);
      if (distanceToPlayer <= 1f) {
        currentState = EnemyState.Attack;
      }
    }
  }

  private void Dead() {
    if (animator != null) {
      animator.SetBool("Die", true);
      ObjectPool.Instance.ReturnToPool("Enemy", gameObject);
      Debug.Log("Return done");
    }
  }
}