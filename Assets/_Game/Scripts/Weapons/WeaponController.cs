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
    Vector3 playerScale = ReferenceHolder.Ins.playerTran.localScale;
    transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
  }
}
