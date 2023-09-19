using System;
using UnityEngine;
using com.ootii.Messages;

public class Weapon : MonoBehaviour {
  public int currentWeaponId;
  public int currentWeaponLevel;
  public WeaponDataLoader weaponDataLoader {
    get => WeaponDataLoader.Ins;
  }

  private WeaponData.WeaponInfo weapon;
  public Transform attackPoint;
  private bool isFacingRight;
  public float fireTimer;
  private float currentRotation;
  private float targetRotation;
  private float speedRotation = 10f;
  void Update() {
    fireTimer += Time.deltaTime;
    if (fireTimer >= weapon.firerate) {
      fireTimer = 0f;

      FindAndFireAtTarget();
    }
    
    currentRotation = Mathf.LerpAngle(currentRotation, targetRotation, Time.deltaTime * speedRotation);
    attackPoint.rotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));
  }


  void Start() {
    weapon = weaponDataLoader.LoadWeaponInfo(currentWeaponId, currentWeaponLevel);
  }
  
  void FindAndFireAtTarget() {
    Transform nearestEnemy = GetNearestEnemy();
    if (nearestEnemy != null) {
      RotateWeaponTowardsEnemy(nearestEnemy);
      FireBulletTowardsEnemy(nearestEnemy);
    }
  }

  private Transform GetNearestEnemy() {
    Transform nearestEnemy = null;
    float minDistance = Mathf.Infinity;

    foreach (GameObject enemy in  ObjectPool.Ins.enemyList) {
      float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
      if (distanceToEnemy <= weapon.attackRange) {
        if (distanceToEnemy < minDistance) {
          nearestEnemy = enemy.transform;
          minDistance = distanceToEnemy;
        }
      }
    }

    return nearestEnemy;
  }

  private void RotateWeaponTowardsEnemy(Transform target) {
    Vector2 directionToTarget = target.position - attackPoint.position;
    targetRotation = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
  }

  void FireBulletTowardsEnemy(Transform targetEnemy) {
    GameObject bulletObject =
        ObjectPool.Ins.SpawnFromPool(Constants.Tag_Bullets, attackPoint.position, attackPoint.rotation);
    AudioManager.Ins.PlaySfx(SoundName.SfxShoot);
    Bullets bullet = bulletObject.GetComponent<Bullets>();
    bullet.SetTarget(targetEnemy, weapon.attackSpeed, weapon.damage);
  }

  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, weapon.attackRange);
  }

  public void UpdateInfo() {
    weapon = weaponDataLoader.LoadWeaponInfo(currentWeaponId, currentWeaponLevel);
  }
}