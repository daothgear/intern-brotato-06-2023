using System;
using com.ootii.Messages;
using UnityEngine;

public class Bullets : MonoBehaviour {
  public float bulletSpeed = 10f;
  private Transform targetEnemy;

  public void SetTarget(Transform enemy) {
    targetEnemy = enemy;
  }

  void FixedUpdate() {
    if (targetEnemy != null) {
      Vector3 direction = (targetEnemy.position - transform.position).normalized;
      transform.position += direction * bulletSpeed * Time.deltaTime;
      float distanceToTarget = Vector3.Distance(transform.position, targetEnemy.position);
      if (distanceToTarget < 0.1f) {
        ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullets, gameObject);
      }
    }
  }
}