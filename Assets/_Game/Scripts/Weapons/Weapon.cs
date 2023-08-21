using System;
using UnityEngine;
using com.ootii.Messages;

public class Weapon : MonoBehaviour {
  public int currentWeaponId;
  public int currentWeaponLevel;
  public WeaponDataLoader weaponDataLoader {
    get => WeaponDataLoader.Ins;
  }

  public Transform attackPoint;
  private bool isFacingRight;
  private float fireTimer;
  private float weaponAttackRange;
  [SerializeField] private float weaponFireRate;
  [SerializeField] private int weaponDamage;
  void Update() {
    fireTimer += Time.deltaTime;
    weaponFireRate = weaponDataLoader.GetWeaponFirerate(currentWeaponId, currentWeaponLevel);
    if (fireTimer >= weaponFireRate) {
      fireTimer = 0f;

      FindAndFireAtTarget();
    }
    RotateWeaponBasedOnPlayerDirection();
  }

  void Start() {
    weaponDataLoader.LoadWeaponInfo(currentWeaponId, currentWeaponLevel);
  }
  
  void FindAndFireAtTarget() {
    Transform nearestEnemy = GetNearestEnemy();

    if (nearestEnemy != null) {
      RotateWeaponTowardsEnemy(nearestEnemy);
      weaponDamage = weaponDataLoader.GetWeaponDamage(currentWeaponId, currentWeaponLevel);
      nearestEnemy.GetComponent<EnemyHealth>().TakeDamage(weaponDamage);

      FireBulletTowardsEnemy(nearestEnemy);
    }
  }

  private Transform GetNearestEnemy() {
    Transform nearestEnemy = null;
    float minDistance = Mathf.Infinity;

    foreach (GameObject enemy in  ObjectPool.Ins.enemyList) {
      float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
      weaponAttackRange = weaponDataLoader.GetWeaponRange(currentWeaponId, currentWeaponLevel);
      if (distanceToEnemy <= weaponAttackRange) {
        if (distanceToEnemy < minDistance) {
          nearestEnemy = enemy.transform;
          minDistance = distanceToEnemy;
        }
      }
    }

    return nearestEnemy;
  }

  private void RotateWeaponTowardsEnemy(Transform targetEnemy) {
    Vector2 directionToEnemy = targetEnemy.position - attackPoint.position;
    float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
    attackPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
  }

  void FireBulletTowardsEnemy(Transform targetEnemy) {
    GameObject bulletObject =
        ObjectPool.Ins.SpawnFromPool(Constants.Tag_Bullets, attackPoint.position, attackPoint.rotation);
    AudioManager.Ins.PlaySfx(SoundName.SfxShoot);
    Bullets bullet = bulletObject.GetComponent<Bullets>();
    bullet.SetTarget(targetEnemy);
  }
  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, weaponAttackRange);
  }

  private void RotateWeaponBasedOnPlayerDirection() {
    isFacingRight = true;
    Vector3 scale = transform.localScale;
    scale.x = Mathf.Abs(scale.x);
    transform.localScale = scale;
  }
}