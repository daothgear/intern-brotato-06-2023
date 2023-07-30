using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
  [SerializeField] private Transform player;
  [SerializeField] private Vector2 offset;
  [SerializeField] private float smoothSpeed = 0.125f;
  [SerializeField] private float leftLimit = -10f;
  [SerializeField] private float rightLimit = 22f;
  [SerializeField] private float topLimit = 21f;
  [SerializeField] private float bottomLimit = -23f;

  private void LateUpdate() {
    if (player == null) {
      player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    if (player != null) {
      Vector3 desiredPosition = new Vector3(player.position.x + offset.x , player.position.y + offset.y , transform.position.z);
      Vector3 smoothedPosition = Vector3.Lerp(transform.position , desiredPosition , smoothSpeed);
      smoothedPosition.x = Mathf.Clamp(smoothedPosition.x , leftLimit , rightLimit);
      smoothedPosition.y = Mathf.Clamp(smoothedPosition.y , bottomLimit , topLimit);
      transform.position = smoothedPosition;
    }
  }
}
