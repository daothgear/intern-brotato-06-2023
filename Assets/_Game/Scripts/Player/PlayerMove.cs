using System;
using System.Security.Cryptography;
using UnityEngine;
using com.ootii.Messages;

public class PlayerMove : MonoBehaviour {
  private PlayerDataLoader playerLoader { get => PlayerDataLoader.Ins; }
  [SerializeField] private Animator animator;
  [SerializeField] private Joystick joystick;

  [SerializeField] private Transform pointLeft;
  [SerializeField] private Transform pointRight;
  [SerializeField] private Transform pointTop;
  [SerializeField] private Transform pointDown;

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
    Vector3 newPosition = transform.position + movement;

    newPosition.x = Mathf.Clamp(newPosition.x, pointLeft.position.x, pointRight.position.x);
    newPosition.y = Mathf.Clamp(newPosition.y, pointDown.position.y, pointTop.position.y);

    transform.position = newPosition;

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