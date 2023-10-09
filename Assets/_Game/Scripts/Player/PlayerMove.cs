using UnityEngine;

public class PlayerMove : MonoBehaviour {
  [SerializeField] private Player player;

  private void OnValidate() {
    if (player == null) {
      player = GetComponent<Player>();
    }
  }

  private void Update() {
    Move();
  }

  private void Start() {
    player.speed = player.playerInfo.moveSpeed;
  }
  private void Move() {
    if (!ReferenceHolder.Ins.uicontroller.UiEndGame.activeSelf) {
      Vector2 movement = new Vector2(player.joystick.Horizontal, player.joystick.Vertical);
      Vector2 newPosition = player.rb.position + movement * (player.speed * Time.fixedDeltaTime);
      player.rb.MovePosition(newPosition);

      if (movement.magnitude > 0) {
        player.animator.SetTrigger(Constants.Anim_PlayerWalk);
      }
      else {
        player.animator.SetTrigger(Constants.Anim_PlayerIdle);
      }

      if (player.joystick.Horizontal < 0 && player.isFacingRight) {
        Flip();
      }
      else if (player.joystick.Horizontal > 0 && !player.isFacingRight) {
        Flip();
      }
    }
  }

  private void Flip() {
    player.isFacingRight = !player.isFacingRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
  }
}