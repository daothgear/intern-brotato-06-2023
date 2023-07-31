using System;
using System.Security.Cryptography;
using UnityEngine;
using com.ootii.Messages;

public class PlayerMove : MonoBehaviour {
  private PlayerDataLoader playerLoader { get => PlayerDataLoader.Instance; }
  [SerializeField] private Animator animator;
  [SerializeField] private Joystick joystick;
  private bool isFacingRight = true;

  private void OnValidate() {
    if (animator == null) {
      animator = GetComponent<Animator>();
    }
  }
  
  private void Update() {
    Move();
  }

  private void Move() {
    Vector3 movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0) * Time.deltaTime * playerLoader.speed;
    transform.position += movement;
    if (movement.magnitude > 0) {
      animator.SetTrigger(Constants.Anim_PlayerWalk);
    }
    else {
      animator.SetTrigger(Constants.Anim_PlayerIdle);
    }

    if (ShouldFlip()) {
      Flip();
    }
  }

  private bool ShouldFlip() {
    if (joystick.Horizontal < 0 && isFacingRight) {
      MessageDispatcher.SendMessage(Constants.Mess_playerFlipRight);
      return true;
    }
    else if (joystick.Horizontal > 0 && !isFacingRight) {
      MessageDispatcher.SendMessage(Constants.Mess_playerFlipRight);
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