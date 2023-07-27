using com.ootii.Messages;
using UnityEngine;

public class Enemy : MonoBehaviour {
  public enum EnemyState {
    Idle,
    Walk,
    Attack,
    Dead
  }

  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Instance;
  }

  private PlayerFollow playerFollow {
    get => PlayerFollow.Instance;
  }
  public EnemyState currentState;

  [SerializeField] private Animator animator;

  private bool isFacingRight;
  public bool isTrigger;
  
  private void OnValidate() {
    if (animator == null) {
      animator = GetComponentInChildren<Animator>();
    }
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
    if (playerFollow.player != null) {
      transform.position =
          Vector3.MoveTowards(transform.position, playerFollow.player.transform.position, enemyLoader.speed * Time.deltaTime);
      if (transform.position.x > playerFollow.player.transform.position.x && isFacingRight) {
        Flip();
      }
      else if (transform.position.x < playerFollow.player.transform.position.x && !isFacingRight) {
        Flip();
      }

      float distanceToPlayer = Vector3.Distance(transform.position, playerFollow.player.transform.position);
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