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
  
  public void UpdateBullet() {
    if (targetEnemy != null) {
      Vector3 direction = (targetEnemy.position - transform.position).normalized;
      transform.position += direction * bulletSpeed * Time.deltaTime;
      float distanceToTarget = Vector3.Distance(transform.position, targetEnemy.position);

      if (distanceToTarget < 0.5f) {
        ObjectPool.Ins.ReturnToPool(Constants.Tag_Bullets, gameObject);
        if (targetEnemy.GetComponent<Enemy>().currentState == Enemy.EnemyState.Walk && targetEnemy.gameObject.activeSelf) {
          targetEnemy.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
        }
      }
    }
  }
}
