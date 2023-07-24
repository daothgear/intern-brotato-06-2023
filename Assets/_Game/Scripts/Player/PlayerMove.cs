using UnityEngine;

public class PlayerMove : MonoBehaviour {
  private PlayerLoader playerLoader;
  [SerializeField] private Animator animator;
  [SerializeField] private Joystick joystick;
  private bool isFacingRight = true;

  private void Awake() {
    playerLoader = PlayerLoader.Instance;
  }

  private void Start() {
    animator = GetComponent<Animator>();
  }

  private void Update() {
    Move();
  }

  private void Move() {
    Vector3 movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0) * Time.deltaTime * playerLoader.speed;
    transform.position += movement;
    if (movement.magnitude > 0) {
      animator.SetTrigger("PlayerWalk");
    }
    else {
      animator.SetTrigger("PlayerIdle");
    }

    if (ShouldFlip()) {
      Flip();
    }
  }

  private bool ShouldFlip() {
    if (joystick.Horizontal < 0 && isFacingRight) {
      return true;
    }
    else if (joystick.Horizontal > 0 && !isFacingRight) {
      return true;
    }

    return false;
  }

  private void Flip() {
    isFacingRight = !isFacingRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
  }
}