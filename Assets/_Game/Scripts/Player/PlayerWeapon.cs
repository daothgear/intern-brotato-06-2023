using com.ootii.Messages;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WeaponPositionInfo {
  public Transform positionWeapon;
  public int currentLevelWeapon;
}
public class PlayerWeapon : MonoBehaviour {
  public List<Transform> weaponPositions = new List<Transform>();
  public List<WeaponPositionInfo> weaponPositionInfo = new List<WeaponPositionInfo>();
  private List<GameObject> collectedWeapons = new List<GameObject>();
  public GameObject weaponPrefab;
  private PlayerHealth playerHealth;
  public Text[] weaponInfoTexts;
  public int nextAvailableWeaponIndex;
  public Button[] weaponInfoButtons;
  public Text weaponInfoText;
  private bool hasCreatedInitialWeapon = false;
  private PlayerData.PlayerInfo playerInfo;
  private WeaponData.WeaponInfo weaponinfo;
  public TextMeshProUGUI textLevel;
  private int randomLevel;
  private void OnValidate() {
    if (playerHealth == null) {
      playerHealth = GetComponent<PlayerHealth>();
    }
  }

  private void Start() {
    LoadCollectedWeapons();
    MessageDispatcher.AddListener(Constants.Mess_addWeapon, AddWeapon);
    MessageDispatcher.AddListener(Constants.Mess_playerDie, ResetWeapon);
    MessageDispatcher.AddListener(Constants.Mess_randomWeapon, RandomLevel);
    UpdateWeaponLevelTexts();
    for (int i = 0; i < weaponInfoButtons.Length; i++) {
      int position = i;
      weaponInfoButtons[i].onClick.AddListener(() => UpdateWeaponInfoTexts(position));
      AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    }
    CheckButtonWeapon();
  }

  private void Update() {
    CheckAndMergeWeapons();
  }

  public void AddWeapon(IMessage msg) {
    if (nextAvailableWeaponIndex < weaponPositions.Count && weaponPositions[nextAvailableWeaponIndex] != null) {
      CreateWeaponAtPosition(weaponPrefab, weaponPositions[nextAvailableWeaponIndex]);
      nextAvailableWeaponIndex++;
      UpdateWeaponLevelTexts();
      CheckButtonWeapon();
      SaveCollectedWeapons();
    } else {
      Weapon newWeaponComponent = weaponPrefab.GetComponent<Weapon>();
      newWeaponComponent.currentWeaponLevel = randomLevel;
      bool canMerge = false;

      foreach (GameObject weapon in collectedWeapons) {
        Weapon collectedWeaponComponent = weapon.GetComponent<Weapon>();

        if (collectedWeaponComponent.currentWeaponLevel == newWeaponComponent.currentWeaponLevel) {
          canMerge = true;
          collectedWeaponComponent.currentWeaponLevel++; 
          WeaponDataLoader.Ins.LoadWeaponInfo(collectedWeaponComponent.currentWeaponId, collectedWeaponComponent.currentWeaponLevel);
          break;
        }
      }

      if (canMerge) {
        for (int i = 0; i < weaponInfoButtons.Length; i++) {
          int position = i;
          weaponInfoButtons[i].onClick.AddListener(() => UpdateWeaponInfoTexts(position));
        }
        UpdateWeaponLevelTexts();
        CheckButtonWeapon();
        SaveCollectedWeapons();
        MessageDispatcher.SendMessage(Constants.Mess_UpdateDataWeapon);
      }
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
          MessageDispatcher.SendMessage(Constants.Mess_UpdateDataWeapon);
          nextAvailableWeaponIndex--;
          UpdateWeaponLevelTexts();
          CheckButtonWeapon();
        }
      }
    }
  }


  private void CreateWeaponAtPosition(GameObject weaponPrefab, Transform position) {
    GameObject newWeapon = Instantiate(weaponPrefab, position.position,
        position.rotation, position.parent);
    Weapon weaponComponent = newWeapon.GetComponent<Weapon>();
    weaponComponent.currentWeaponLevel = randomLevel;
    collectedWeapons.Add(newWeapon);
  }
  
  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_addWeapon, AddWeapon);
    MessageDispatcher.RemoveListener(Constants.Mess_playerDie, ResetWeapon);
    SaveCollectedWeapons();
  }

  private void SaveCollectedWeapons() {
    PlayerPrefs.SetInt(Constants.PrefsKey_CollectedWeaponsCount, collectedWeapons.Count);

    string weaponLevelsData = "";
    foreach (GameObject weapon in collectedWeapons) {
      Weapon weaponComponent = weapon.GetComponent<Weapon>();
      weaponLevelsData += weaponComponent.currentWeaponLevel + ",";
    }

    PlayerPrefs.SetString(Constants.PrefsKey_CollectedWeaponsLevels, weaponLevelsData);

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
    } else {
      if (weaponPositions.Count > 0 && weaponPositions[0] != null) {
        CreateWeaponAtPosition(weaponPrefab, weaponPositions[0]);
        Weapon weaponComponent = collectedWeapons[0].GetComponent<Weapon>();
        weaponComponent.currentWeaponLevel = 1;
        WeaponDataLoader.Ins.LoadWeaponInfo(weaponComponent.currentWeaponId, weaponComponent.currentWeaponLevel);
        nextAvailableWeaponIndex++;
      }
      if (weaponPositions.Count > 1 && weaponPositions[1] != null) {
        CreateWeaponAtPosition(weaponPrefab, weaponPositions[1]);
        Weapon weaponComponent = collectedWeapons[1].GetComponent<Weapon>();
        weaponComponent.currentWeaponLevel = 2;
        WeaponDataLoader.Ins.LoadWeaponInfo(weaponComponent.currentWeaponId, weaponComponent.currentWeaponLevel);
        nextAvailableWeaponIndex++;
      }
    }
  }
  private void ResetWeapon(IMessage msg) {
    foreach (GameObject weapon in collectedWeapons) {
      Destroy(weapon);
      CheckButtonWeapon();
    }

    collectedWeapons.Clear();
    nextAvailableWeaponIndex = 1;
    hasCreatedInitialWeapon = false;

    if (weaponPositions.Count > 0 && weaponPositions[0] != null) {
      foreach (WeaponPositionInfo weaponInfo in weaponPositionInfo) {
        if (weaponInfo.positionWeapon != null) {
          GameObject newWeapon = Instantiate(weaponPrefab, weaponInfo.positionWeapon.position,
              weaponInfo.positionWeapon.rotation, transform.parent);
          collectedWeapons.Add(newWeapon);

          Weapon weaponComponent = newWeapon.GetComponent<Weapon>();
          weaponComponent.currentWeaponLevel = weaponInfo.currentLevelWeapon;
          nextAvailableWeaponIndex++;
        }
      }
    }
  }

  private void UpdateWeaponInfoTexts(int position) {
    if (position < collectedWeapons.Count) {
      Weapon weaponComponent = collectedWeapons[position].GetComponent<Weapon>();
      WeaponDataLoader weaponDataLoader = WeaponDataLoader.Ins;
      weaponinfo = weaponDataLoader.LoadWeaponInfo(weaponComponent.currentWeaponId, weaponComponent.currentWeaponLevel);
      weaponInfoText.text = "Position: " + position +
                            "\nID: " + weaponComponent.currentWeaponId +
                            "\nLevel: " + (weaponComponent.currentWeaponLevel + 1) +
                            "\nDamage: " + weaponinfo.damage +
                            "\nRange: " + weaponinfo.attackRange +
                            "\nFirerate: " + weaponinfo.firerate +
                            "\nSpeed: " + weaponinfo.attackSpeed;
    }
    else {
      weaponInfoText.text = "You don't have any weapons in this position";
    }
  }
  
  private void CheckButtonWeapon() {
    for (int i = 0; i < weaponInfoButtons.Length; i++) {
      if (i < collectedWeapons.Count) {
        weaponInfoButtons[i].gameObject.SetActive(true);
      }
      else {
        weaponInfoButtons[i].gameObject.SetActive(false);
      }
    }
  }

  private void RandomLevel(IMessage img) {
    randomLevel = UnityEngine.Random.Range(1, 5);
    textLevel.text = "Level: " + (randomLevel + 1);
  }

  private void UpdateWeaponLevelTexts() {
    for (int i = 0; i < weaponInfoTexts.Length; i++) {
      if (i < collectedWeapons.Count) {
        Weapon weaponComponent = collectedWeapons[i].GetComponent<Weapon>();
        weaponInfoTexts[i].text = "Weapon Level: " + (weaponComponent.currentWeaponLevel + 1);
      } else {
        weaponInfoTexts[i].text = "";
      }
    }
  }
}