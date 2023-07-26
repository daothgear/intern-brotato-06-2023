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
}
