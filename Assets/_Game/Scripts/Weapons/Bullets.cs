using System;
using System.Collections;
using com.ootii.Messages;
using UnityEngine;

public class Bullets : MonoBehaviour {
  public float bulletSpeed;
  public int bulletDamage;
  private Transform targetEnemy;

  public void SetTarget(Transform enemy, float speed, int damage) {
    targetEnemy = enemy;
    bulletSpeed = speed;
    bulletDamage = damage;
  }

  void FixedUpdate() {
    if (targetEnemy != null) {
      Vector3 direction = (targetEnemy.position - transform.position).normalized;
      transform.position += direction * bulletSpeed * Time.deltaTime;
      float distanceToTarget = Vector3.Distance(transform.position, targetEnemy.position);
      if (distanceToTarget < 0.5f) {
        ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullets, gameObject);
        CheckTargetEnemy();
        targetEnemy.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
      }
    }
  }

  private void CheckTargetEnemy() {
    if (!targetEnemy.gameObject.activeSelf) {
      ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullets, gameObject);
    }
  }
}