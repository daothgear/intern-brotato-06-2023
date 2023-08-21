using com.ootii.Messages;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
  public List<Transform> weaponPositions = new List<Transform>();
  private List<GameObject> collectedWeapons = new List<GameObject>();
  public GameObject weaponPrefab;
  private PlayerHealth playerHealth;
  private int nextAvailableWeaponIndex = 1;

  private bool hasCreatedInitialWeapon = false;

  private void OnValidate() {
    if (playerHealth == null) {
      playerHealth = GetComponent<PlayerHealth>();
    }
  }

  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_addWeapon , AddWeapon);
    MessageDispatcher.AddListener(Constants.Mess_playerDie , ResetWeapon);

    LoadCollectedWeapons();
  }

  private void Update() {
    CheckAndMergeWeapons();
  }

  public void AddWeapon(IMessage msg) {
    if (nextAvailableWeaponIndex < weaponPositions.Count && weaponPositions[nextAvailableWeaponIndex] != null) {
      CreateWeaponAtPosition(weaponPrefab , weaponPositions[nextAvailableWeaponIndex]);
      nextAvailableWeaponIndex++;
    }
  }

  private void CheckAndMergeWeapons() {
    for (int i = 0 ; i < collectedWeapons.Count ; i++) {
      for (int j = i + 1 ; j < collectedWeapons.Count ; j++) {
        GameObject weaponA = collectedWeapons[i];
        GameObject weaponB = collectedWeapons[j];
        Weapon weaponComponentA = weaponA.GetComponent<Weapon>();
        Weapon weaponComponentB = weaponB.GetComponent<Weapon>();
        if (weaponComponentA.currentWeaponId == weaponComponentB.currentWeaponId
            && weaponComponentA.currentWeaponLevel == weaponComponentB.currentWeaponLevel) {
          collectedWeapons.RemoveAt(j);
          Destroy(weaponB);
          weaponComponentA.currentWeaponLevel++;
          WeaponDataLoader.Ins.LoadWeaponInfo(weaponComponentA.currentWeaponId , weaponComponentA.currentWeaponLevel);
          nextAvailableWeaponIndex--;
        }
      }
    }
  }

  private void CreateWeaponAtPosition(GameObject weaponPrefab , Transform position) {
    GameObject newWeapon = Instantiate(weaponPrefab , position.position , position.rotation);
    newWeapon.transform.parent = position;
    collectedWeapons.Add(newWeapon);
  }
  
  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_addWeapon , AddWeapon);
    MessageDispatcher.RemoveListener(Constants.Mess_playerDie , ResetWeapon);
    SaveCollectedWeapons();
  }

  private void SaveCollectedWeapons() {
    PlayerPrefs.SetInt(Constants.PrefsKey_CollectedWeaponsCount , collectedWeapons.Count);

    string weaponLevelsData = "";
    foreach (GameObject weapon in collectedWeapons) {
      Weapon weaponComponent = weapon.GetComponent<Weapon>();
      weaponLevelsData += weaponComponent.currentWeaponLevel + ",";
    }
    PlayerPrefs.SetString(Constants.PrefsKey_CollectedWeaponsLevels , weaponLevelsData);

    PlayerPrefs.Save();
  }

  private void LoadCollectedWeapons() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_CollectedWeaponsCount)) {
      int collectedCount = PlayerPrefs.GetInt(Constants.PrefsKey_CollectedWeaponsCount);
      string weaponLevelsData = PlayerPrefs.GetString(Constants.PrefsKey_CollectedWeaponsLevels);
      string[] weaponLevels = weaponLevelsData.Split(',');

      for (int i = 0; i < collectedCount; i++) {
        if (i < weaponPositions.Count && weaponPositions[i] != null) {
          CreateWeaponAtPosition(weaponPrefab, weaponPositions[i]);
          Weapon weaponComponent = collectedWeapons[i].GetComponent<Weapon>();
          weaponComponent.currentWeaponLevel = int.Parse(weaponLevels[i]);
          WeaponDataLoader.Ins.LoadWeaponInfo(weaponComponent.currentWeaponId, weaponComponent.currentWeaponLevel);
          nextAvailableWeaponIndex++;
        }
      }
    }
  }
  private void ResetWeapon(IMessage msg) {
    foreach (GameObject weapon in collectedWeapons) {
      Destroy(weapon);
    }
    collectedWeapons.Clear();
    nextAvailableWeaponIndex = 1;
    hasCreatedInitialWeapon = false;

    if (weaponPositions.Count > 0 && weaponPositions[0] != null) {
      CreateWeaponAtPosition(weaponPrefab , weaponPositions[0]);
      hasCreatedInitialWeapon = true;
      nextAvailableWeaponIndex++;
      if (weaponPositions.Count > 1 && weaponPositions[1] != null) {
        GameObject newWeapon = Instantiate(weaponPrefab, weaponPositions[1].position, weaponPositions[1].rotation);
        newWeapon.transform.parent = weaponPositions[1];
        collectedWeapons.Add(newWeapon);

        Weapon weaponComponent = newWeapon.GetComponent<Weapon>();
        weaponComponent.currentWeaponLevel = 2;
        WeaponDataLoader.Ins.LoadWeaponInfo(weaponComponent.currentWeaponId, weaponComponent.currentWeaponLevel);
      }
    }
  }

}
