using UnityEngine;
using com.ootii.Messages;

public class Weapon : MonoBehaviour {
  public GameObject bulletPrefab;
  public Transform attackPoint;
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

  void Start() {
    MessageDispatcher.AddListener("Right" , Flip);
    MessageDispatcher.AddListener("Left" , Flip);
  }


  void FindAndFireAtTarget() {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tag_Enemy);

    Transform nearestEnemy = null;
    float minDistance = Mathf.Infinity;

    foreach (GameObject enemy in enemies) {
      float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
      if (distanceToEnemy <= shootingRange) {
        if (distanceToEnemy < minDistance) {
          nearestEnemy = enemy.transform;
          minDistance = distanceToEnemy;
        }
      }
    }

    if (nearestEnemy != null) {
      RotateWeaponTowardsEnemy(nearestEnemy);
      FireBulletTowardsEnemy(nearestEnemy);
    }
  }

  private void RotateWeaponTowardsEnemy(Transform targetEnemy) {
    Vector2 directionToEnemy = targetEnemy.position - attackPoint.position;
    float angle = Mathf.Atan2(directionToEnemy.y , directionToEnemy.x) * Mathf.Rad2Deg;
    attackPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
  }

  void FireBulletTowardsEnemy(Transform targetEnemy) {
    GameObject bulletObject = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
    Bullets bulletController = bulletObject.GetComponent<Bullets>();
    bulletController.SetTarget(targetEnemy);
  }

  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, shootingRange);
  }


  private void Flip(IMessage img) {
    isFacingRight = !isFacingRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
  }

}
