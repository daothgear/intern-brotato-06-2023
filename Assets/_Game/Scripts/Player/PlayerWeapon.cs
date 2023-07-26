using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
  [SerializeField] private Transform[] weaponPositions = new Transform[6];
  private List<GameObject> collectedWeapons = new List<GameObject>();
  public GameObject defaultWeaponPrefab;
  public string desiredWeaponTag = "Pistol";

  void Start() {
    if (weaponPositions.Length > 0 && weaponPositions[0] != null) {
      GameObject defaultWeapon = Instantiate(defaultWeaponPrefab , weaponPositions[1].position , weaponPositions[0].rotation);
      defaultWeapon.transform.parent = weaponPositions[0];
      collectedWeapons.Add(defaultWeapon);
    }
  }

  public void AddWeapon(GameObject weaponPrefab) {
    if (collectedWeapons.Contains(weaponPrefab)) {
      Debug.LogWarning("You have already collected this weapon.");
      return;
    }

    if (collectedWeapons.Count >= weaponPositions.Length) {
      Debug.LogWarning("You have reached the maximum number of weapons.");
      return;
    }

    for (int i = 0 ; i < weaponPositions.Length ; i++) {
      if (weaponPositions[i] == null) {
        GameObject weaponInstance = Instantiate(weaponPrefab , weaponPositions[i].position , weaponPositions[i].rotation);
        weaponInstance.transform.parent = weaponPositions[i];
        collectedWeapons.Add(weaponInstance);
        return;
      }
    }

    Debug.LogWarning("You have no empty weapon positions to add the new weapon.");
  }

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag(desiredWeaponTag)) ;
    AddWeapon(other.gameObject);
    Destroy(other.gameObject);
  }
}
