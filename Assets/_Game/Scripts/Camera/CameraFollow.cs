using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
  [SerializeField] private Transform target;
  [SerializeField] private float smoothSpeed = 16f;
  [SerializeField] private Vector3 offset;

  private void Update() {
    Vector3 desiredPosition = target.position + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;
  }
}