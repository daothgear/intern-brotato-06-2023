using com.ootii.Messages;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
  [SerializeField] private Transform[] weaponPositions = new Transform[6];
  private List<GameObject> collectedWeapons = new List<GameObject>();
  public GameObject weaponPrefab;
  private int nextAvailableWeaponIndex = 1;

  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_addWeapon, AddWeapon);

    if (weaponPositions.Length > 0 && weaponPositions[0] != null) {
      CreateWeaponAtPosition(weaponPrefab, weaponPositions[0]);
    }
  }

  public void AddWeapon(IMessage msg) { 
    if (nextAvailableWeaponIndex < weaponPositions.Length && weaponPositions[nextAvailableWeaponIndex] != null) {
      CreateWeaponAtPosition(weaponPrefab, weaponPositions[nextAvailableWeaponIndex]);
      nextAvailableWeaponIndex++;
      CheckAndMergeWeapons();
    } else {
      ShowWarningForMissingWeaponPosition();
    }
  }

  private void CheckAndMergeWeapons() {
    for (int i = 0; i < collectedWeapons.Count; i++) {
      for (int j = i + 1; j < collectedWeapons.Count; j++) {
        GameObject weaponA = collectedWeapons[i];
        GameObject weaponB = collectedWeapons[j];
        Weapon weaponComponentA = weaponA.GetComponent<Weapon>();
        Weapon weaponComponentB = weaponB.GetComponent<Weapon>();
        if (weaponComponentA.currentWeaponId == weaponComponentB.currentWeaponId
            && weaponComponentA.currentWeaponLevel == weaponComponentB.currentWeaponLevel) {
          collectedWeapons.RemoveAt(j);
          Destroy(weaponB);
          weaponComponentA.currentWeaponLevel++;
          WeaponDataLoader.Ins.LoadWeaponInfo(weaponComponentA.currentWeaponId, weaponComponentA.currentWeaponLevel);
        }
      }
    }
  }

  private void CreateWeaponAtPosition(GameObject weaponPrefab, Transform position) {
    GameObject newWeapon = Instantiate(weaponPrefab, position.position, position.rotation);
    newWeapon.transform.parent = position;
    collectedWeapons.Add(newWeapon);
    Debug.Log("Weapon added at position: " + position.name);
  }

  private void ShowWarningForMissingWeaponPosition() {
    Debug.LogWarning("Not enough weapon positions or position missing in weaponPositions array!");
  }
}