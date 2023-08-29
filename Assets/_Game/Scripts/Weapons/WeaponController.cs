using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
  public Transform player;
  private void Update() {
    FollowPlayer();
  }

  private void FollowPlayer() {
    if (player != null) {
      transform.position = player.position;
    }
  }
}
