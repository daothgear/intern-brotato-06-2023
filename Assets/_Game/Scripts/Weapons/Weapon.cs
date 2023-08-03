using System;
using UnityEngine;
using com.ootii.Messages;

public class Weapon : MonoBehaviour {
  [SerializeField] private int currentWeaponId;
  [SerializeField] private int currentWeaponLevel;
  private WeaponDataLoader weaponDataLoader {
    get => WeaponDataLoader.Ins;
  }

  public Transform attackPoint;
  private bool isFacingRight = true;
  private float fireTimer;

  void Update() {
    fireTimer += Time.deltaTime;

    if (fireTimer >= weaponDataLoader.firerate) {
      fireTimer = 0f;

      FindAndFireAtTarget();
    }
  }

  void Start() {
    MessageDispatcher.AddListener(Constants.Mess_playerFlipRight, Flip);
    MessageDispatcher.AddListener(Constants.Mess_playerFlipLeft, Flip);
    weaponDataLoader.LoadWeaponInfo(currentWeaponId,currentWeaponLevel);
  }


  private void OnDestroy() {
    MessageDispatcher.AddListener(Constants.Mess_playerFlipRight, Flip);
    MessageDispatcher.AddListener(Constants.Mess_playerFlipLeft, Flip);
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
      if (distanceToEnemy <= weaponDataLoader.weaponAttackRange) {
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
    Bullets bullet = bulletObject.GetComponent<Bullets>();
    bullet.SetTarget(targetEnemy);
  }

  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, weaponDataLoader.weaponAttackRange);
  }

  private void Flip(IMessage img) {
    isFacingRight = !isFacingRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
  }
}