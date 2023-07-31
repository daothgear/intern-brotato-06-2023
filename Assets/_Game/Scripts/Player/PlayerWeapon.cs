using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
  [SerializeField] private Transform[] weaponPositions = new Transform[6];
  private List<GameObject> collectedWeapons = new List<GameObject>();
  public GameObject defaultWeaponPrefab;
  public string desiredWeaponTag = "Piston";

  private int nextAvailableWeaponIndex = 0;

  void Start() {
    if (weaponPositions.Length > 0 && weaponPositions[0] != null) {
      GameObject defaultWeapon =
          Instantiate(defaultWeaponPrefab, weaponPositions[0].position, weaponPositions[0].rotation);
      defaultWeapon.transform.parent = weaponPositions[0];
      collectedWeapons.Add(defaultWeapon);
      nextAvailableWeaponIndex = 1;
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag(desiredWeaponTag)) {
      if (!collectedWeapons.Contains(other.gameObject)) {
        if (nextAvailableWeaponIndex < weaponPositions.Length && weaponPositions[nextAvailableWeaponIndex] != null) {
          collectedWeapons.Add(other.gameObject);
          other.transform.position = weaponPositions[nextAvailableWeaponIndex].position;
          other.transform.rotation = weaponPositions[nextAvailableWeaponIndex].rotation;
          other.transform.parent = weaponPositions[nextAvailableWeaponIndex];
          nextAvailableWeaponIndex++;

          Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();
          if (rb2D)
            rb2D.simulated = false;

          Collider2D coll2D = other.GetComponent<Collider2D>();
          if (coll2D)
            coll2D.enabled = false;
        }
        else {
          Debug.LogWarning("Not enough weapon positions or position missing in weaponPositions array!");
        }
      }
    }
  }
}