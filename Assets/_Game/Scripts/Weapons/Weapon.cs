using UnityEngine;

public class Weapon : MonoBehaviour {
  public GameObject bulletPrefab;
  public float fireRate = 0.5f;
  public float shootingRange = 10f;
  private bool isFacingRight = true;
  private float fireTimer;

  void Update() {
    fireTimer += Time.deltaTime;

    if (fireTimer >= fireRate) {
      fireTimer = 0f;

      FindAndFireAtTarget();
    }
  }

  void FindAndFireAtTarget() {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);

    Transform nearestEnemy = null;
    float minDistance = Mathf.Infinity;

    foreach (GameObject enemy in enemies) {
      float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
      if (distanceToEnemy <= shootingRange) {
        if (distanceToEnemy < minDistance) {
          nearestEnemy = enemy.transform;
          minDistance = distanceToEnemy;
        }
      }
    }

    if (nearestEnemy != null) {
      FireBulletTowardsEnemy(nearestEnemy);
    }
  }

  void FireBulletTowardsEnemy(Transform targetEnemy) {
    GameObject bulletObject = Instantiate(bulletPrefab, transform.position, transform.rotation);
    Bullets bulletController = bulletObject.GetComponent<Bullets>();
    bulletController.SetTarget(targetEnemy);
  }

  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, shootingRange);
  }
}