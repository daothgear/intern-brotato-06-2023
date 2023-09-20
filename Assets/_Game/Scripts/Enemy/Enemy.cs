using UnityEngine;

public class Enemy : MonoBehaviour {
  public enum EnemyState {
    Walk,
    Dead
  }

  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Ins;
  }

  public EnemyData.EnemyInfo enemyInfo;
  public EnemyState currentState;

  [SerializeField] private Animator animator;

  private bool isFacingRight;
  private float damageInterval = 1f;
  private float lastDamageTime = 0.0f;
  private bool isCollidingWithPlayer = false;

  private void OnValidate() {
    if (animator == null) {
      animator = GetComponentInChildren<Animator>();
    }
  }

  private void Awake() {
    enemyInfo = enemyLoader.LoadEnemyInfo(1);
  }
  private void Update() {
    switch (currentState) {
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

  public void Walk() {
    if (ReferenceHolder.Ins.playerTran != null) {
      animator.SetTrigger(Constants.Anim_Walk);
      transform.position =
          Vector3.MoveTowards(transform.position, ReferenceHolder.Ins.playerTran.position,
              enemyInfo.moveSpeed * Time.deltaTime);
      if (transform.position.x > ReferenceHolder.Ins.playerTran.position.x && isFacingRight) {
        Flip();
      }
      else if (transform.position.x < ReferenceHolder.Ins.playerTran.position.x && !isFacingRight) {
        Flip();
      }
    }
  }

  private void Dead() {
    animator.SetBool(Constants.Anim_Die, true);
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag(Constants.Tag_Player)) {
      if (currentState == EnemyState.Walk) {
        isCollidingWithPlayer = true;
        ReferenceHolder.Ins.playerHealth.TakeDamage(enemyInfo.damage);
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collision) {
    if (collision.CompareTag(Constants.Tag_Player)) {
      isCollidingWithPlayer = false;
    }
  }

  private void FixedUpdate() {
    if (isCollidingWithPlayer) {
      lastDamageTime += Time.fixedDeltaTime;
      if (lastDamageTime >= damageInterval) {
        ReferenceHolder.Ins.playerHealth.TakeDamage(enemyInfo.damage);
        lastDamageTime = 0f;
      }
    }
  }
}
