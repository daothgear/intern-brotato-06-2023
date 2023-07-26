using UnityEngine;

public class Enemy : Singleton<Enemy> {
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
  public bool isTrigger;
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
    Debug.Log(isTrigger);
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
    isTrigger = true;
  }

  public void Walk() {
    isTrigger = true;
    Debug.Log(isTrigger);
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
    isTrigger = false;
    animator.SetBool("Die", true);
  }
  
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Player")) {
      if (isTrigger == true) {
        playerHealth.TakeDamage(enemyLoader.damageEnemy);
      }
    }
  }
}