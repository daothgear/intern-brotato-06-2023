using com.ootii.Messages;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
  public List<Transform> weaponPositions = new List<Transform>();
  private List<GameObject> collectedWeapons = new List<GameObject>();
  public GameObject weaponPrefab;
  private int nextAvailableWeaponIndex = 1;

  private const string CollectedWeaponsCountPrefsKey = "CollectedWeaponsCount";
  private const string CollectedWeaponsLevelsPrefsKey = "CollectedWeaponsLevels";

  private bool hasCreatedInitialWeapon = false;

  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_addWeapon , AddWeapon);
    MessageDispatcher.AddListener(Constants.Mess_playerDie , ResetWeapon);

    LoadCollectedWeapons();

    if (collectedWeapons == null) {
      if (!hasCreatedInitialWeapon && weaponPositions.Count > 0 && weaponPositions[0] != null) {
        CreateWeaponAtPosition(weaponPrefab , weaponPositions[0]);
        hasCreatedInitialWeapon = true;
        nextAvailableWeaponIndex++;
      }
    }
  }

  private void Update() {
    CheckAndMergeWeapons();
  }

  public void AddWeapon(IMessage msg) {
    if (nextAvailableWeaponIndex < weaponPositions.Count && weaponPositions[nextAvailableWeaponIndex] != null) {
      CreateWeaponAtPosition(weaponPrefab , weaponPositions[nextAvailableWeaponIndex]);
      nextAvailableWeaponIndex++;
    } else {
      ShowWarningForMissingWeaponPosition();
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
    Debug.Log("Weapon added at position: " + position.name);
  }

  private void ShowWarningForMissingWeaponPosition() {
    Debug.LogWarning("Not enough weapon positions or position missing in weaponPositions list!");
  }

  private void OnDestroy() {
    SaveCollectedWeapons();
  }

  private void SaveCollectedWeapons() {
    PlayerPrefs.SetInt(CollectedWeaponsCountPrefsKey , collectedWeapons.Count);

    string weaponLevelsData = "";
    foreach (GameObject weapon in collectedWeapons) {
      Weapon weaponComponent = weapon.GetComponent<Weapon>();
      weaponLevelsData += weaponComponent.currentWeaponLevel + ",";
    }
    PlayerPrefs.SetString(CollectedWeaponsLevelsPrefsKey , weaponLevelsData);

    PlayerPrefs.Save();
  }

  private void LoadCollectedWeapons() {
    if (PlayerPrefs.HasKey(CollectedWeaponsCountPrefsKey)) {
      int collectedCount = PlayerPrefs.GetInt(CollectedWeaponsCountPrefsKey);
      string weaponLevelsData = PlayerPrefs.GetString(CollectedWeaponsLevelsPrefsKey);
      string[] weaponLevels = weaponLevelsData.Split(',');

      for (int i = 0 ; i < collectedCount ; i++) {
        if (i < weaponPositions.Count && weaponPositions[i] != null) {
          CreateWeaponAtPosition(weaponPrefab , weaponPositions[i]);
          Weapon weaponComponent = collectedWeapons[i].GetComponent<Weapon>();
          weaponComponent.currentWeaponLevel = int.Parse(weaponLevels[i]);
          WeaponDataLoader.Ins.LoadWeaponInfo(weaponComponent.currentWeaponId , weaponComponent.currentWeaponLevel);
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
    }
  }

}
