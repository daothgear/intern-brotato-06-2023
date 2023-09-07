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
    get => EnemyDataLoader.Ins;
  }


  public EnemyState currentState;

  [SerializeField] private Animator animator;

  private bool isFacingRight;
  private float damageInterval = 0.5f;
  private float lastDamageTime = 0.0f;

  private void OnValidate() {
    if (animator == null) {
      animator = GetComponentInChildren<Animator>();
    }
  }

  private void Start() {
    enemyLoader.LoadEnemyInfo(1);
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
    animator.SetTrigger(Constants.Anim_Idle);
    Invoke("TransitionToWalk", 0.5f);
  }

  private void TransitionToWalk() {
    animator.SetTrigger(Constants.Anim_Walk);
    currentState = EnemyState.Walk;
  }

  public void Walk() {
    if (ReferenceHolder.Ins.playerTran != null) {
      transform.position =
          Vector3.MoveTowards(transform.position, ReferenceHolder.Ins.playerTran.position,
              enemyLoader.speed * Time.deltaTime);
      if (transform.position.x > ReferenceHolder.Ins.playerTran.position.x && isFacingRight) {
        Flip();
      }
      else if (transform.position.x < ReferenceHolder.Ins.playerTran.position.x && !isFacingRight) {
        Flip();
      }

      float distanceToPlayer = Vector3.Distance(transform.position, ReferenceHolder.Ins.playerTran.position);
      if (distanceToPlayer <= 1f) {
        currentState = EnemyState.Walk;
      }
    }
  }

  private void Dead() {
    animator.SetBool(Constants.Anim_Die, true);
    ObjectPool.Ins.enemyList.Remove(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag(Constants.Tag_Player)) {
      MessageDispatcher.SendMessage(Constants.Mess_playerTakeDamage);
    }
  }

  private void OnTriggerStay2D(Collider2D other) {
    if (other.CompareTag(Constants.Tag_Player)) {
      lastDamageTime += Time.deltaTime;
      if (lastDamageTime >= damageInterval) {
        MessageDispatcher.SendMessage(Constants.Mess_playerTakeDamage);
        lastDamageTime = 0f;
      }
    }
  }
}