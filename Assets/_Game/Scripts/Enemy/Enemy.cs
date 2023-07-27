using com.ootii.Messages;
using UnityEngine;

public class Enemy : MonoBehaviour {
  public enum EnemyState {
    Idle,
    Walk,
    Attack,
    Dead
  }

  private EnemyDataLoader enemyLoader { get => EnemyDataLoader.Instance;}
  public EnemyState currentState;
  
  [SerializeField] private Animator animator;
  
  private bool isFacingRight;
  public bool isTrigger;
  
  public GameObject player;
  private void OnValidate() {
    if (animator == null) {
      animator = GetComponentInChildren<Animator>();
    }
  }

  private void Awake() {
    if (player == null) {
      player = GameObject.FindGameObjectWithTag(Constants.Tag_Player);
    }
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
    animator.SetTrigger(Constants.Anim_Walk);
    Invoke("TransitionToWalk", 0.5f);
  }

  private void TransitionToWalk() {
    currentState = EnemyState.Walk;
    isTrigger = true;
  }

  public void Walk() {
    isTrigger = true;
    Debug.Log(isTrigger);
    if (player != null) {
      transform.position =
          Vector3.MoveTowards(transform.position, player.transform.position, enemyLoader.speed * Time.deltaTime);
      if (transform.position.x > player.transform.position.x && isFacingRight) {
        Flip();
      }
      else if (transform.position.x < player.transform.position.x && !isFacingRight) {
        Flip();
      }

      float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
      if (distanceToPlayer <= 1f) {
        currentState = EnemyState.Attack;
      }
    }
  }

  private void Dead() {
    isTrigger = false;
    animator.SetBool(Constants.Anim_Die, true);
  }
  
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag(Constants.Tag_Player)) {
      if (isTrigger == true) {
        MessageDispatcher.SendMessage(Constants.Mess_playerTakeDamage);
      }
    }
  }
}