using System;
using System.Security.Cryptography;
using UnityEngine;
using com.ootii.Messages;

public class PlayerMove : MonoBehaviour {
  private PlayerDataLoader playerLoader { get => PlayerDataLoader.Ins; }
  [SerializeField] private Animator animator;
  [SerializeField] private Joystick joystick;

  private bool isFacingRight = true;

  private Rigidbody2D rb;

  private void OnValidate() {
    if (animator == null) {
      animator = GetComponent<Animator>();
    }

    if (rb == null) {
      rb = GetComponent<Rigidbody2D>();
    }
  }
  
  private void Update() {
    Move();
  }

  private void Move() {
    Vector2 movement = new Vector2(joystick.Horizontal, joystick.Vertical);
    Vector2 newPosition = rb.position + movement * (playerLoader.speed * Time.fixedDeltaTime);
    rb.MovePosition(newPosition);

    if (movement.magnitude > 0) {
      animator.SetTrigger(Constants.Anim_PlayerWalk);
    }
    else {
      animator.SetTrigger(Constants.Anim_PlayerIdle);
    }

    if (joystick.Horizontal < 0 && isFacingRight) {
      Flip();
    }
    else if (joystick.Horizontal > 0 && !isFacingRight) {
      Flip();
    }
  }

  private void Flip() {
    isFacingRight = !isFacingRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
  }
}
