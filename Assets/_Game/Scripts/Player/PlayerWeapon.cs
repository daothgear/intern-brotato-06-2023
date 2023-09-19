using com.ootii.Messages;
using System;
using UnityEngine;

[Serializable]
public class WeaponPositionInfo {
  public Transform positionWeapon;
  public int currentLevelWeapon;
}

public class PlayerWeapon : MonoBehaviour {
  private Player player;
  private PlayerUi playerUi;

  private void OnValidate() {
    if (player == null) {
      player = GetComponent<Player>();
    }

    if (playerUi == null) {
      playerUi = GetComponent<PlayerUi>();
    }
  }

  private void Start() {
    LoadCollectedWeapons();
    MessageDispatcher.AddListener(Constants.Mess_addWeapon, AddWeapon);
    MessageDispatcher.AddListener(Constants.Mess_playerDie, ResetWeapon);
    MessageDispatcher.AddListener(Constants.Mess_UpdateTextCoin, CheckCoinStart);
    MessageDispatcher.AddListener(Constants.Mess_LevelWeapon, SetDataLevel);
    playerUi.UpdateWeaponLevelTexts();
    for (int i = 0; i < player.weaponInfoButtons.Length; i++) {
      int position = i;
      player.weaponInfoButtons[i].onClick.AddListener(() => playerUi.UpdateWeaponInfoTexts(position));
      AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    }

    playerUi.CheckButtonWeapon();
  }

  private void Update() {
    CheckAndMergeWeapons();
  }

  private void SetDataLevel(IMessage msg) {
    player.levelrandom = (int)msg.Data;
  }

  public void AddWeapon(IMessage msg) {
    if (player.isBuydone == true) {
      if (player.nextAvailableWeaponIndex < player.weaponPositions.Count &&
          player.weaponPositions[player.nextAvailableWeaponIndex] != null) {
        CreateWeaponAtPosition(player.weaponPrefab, player.weaponPositions[player.nextAvailableWeaponIndex]);
        player.nextAvailableWeaponIndex++;
        playerUi.UpdateWeaponLevelTexts();
        playerUi.CheckButtonWeapon();
        SaveCollectedWeapons();
      }
      else {
        Weapon newWeaponComponent = player.weaponPrefab.GetComponent<Weapon>();
        newWeaponComponent.currentWeaponLevel = player.levelrandom;
        bool canMerge = false;

        foreach (GameObject weapon in player.collectedWeapons) {
          Weapon collectedWeaponComponent = weapon.GetComponent<Weapon>();

          if (collectedWeaponComponent.currentWeaponLevel == newWeaponComponent.currentWeaponLevel) {
            canMerge = true;
            collectedWeaponComponent.currentWeaponLevel++;
            if (canMerge) {
              playerUi.UpdateWeaponLevelTexts();
              playerUi.CheckButtonWeapon();
              SaveCollectedWeapons();
              MessageDispatcher.SendMessage(Constants.Mess_UpdateDataWeapon);
            }

            return;
          }
        }
      }
    }
  }

  public void CheckCoinStart(IMessage img) {
    if (player.nextAvailableWeaponIndex == player.weaponPositions.Count) {
      Weapon newWeaponComponent = player.weaponPrefab.GetComponent<Weapon>();
      newWeaponComponent.currentWeaponLevel = player.levelrandom;
      foreach (GameObject weapon in player.collectedWeapons) {
        Weapon collectedWeaponComponent = weapon.GetComponent<Weapon>();
        if (collectedWeaponComponent.currentWeaponLevel == newWeaponComponent.currentWeaponLevel) {
          player.isBuydone = true;
          return;
        }

        player.isBuydone = false;
      }
    }
    else {
      player.isBuydone = true;
    }
  }

  private void CheckAndMergeWeapons() {
    for (int i = 0; i < player.collectedWeapons.Count; i++) {
      for (int j = i + 1; j < player.collectedWeapons.Count; j++) {
        GameObject weaponA = player.collectedWeapons[i];
        GameObject weaponB = player.collectedWeapons[j];
        Weapon weaponComponentA = weaponA.GetComponent<Weapon>();
        Weapon weaponComponentB = weaponB.GetComponent<Weapon>();
        if (weaponComponentA.currentWeaponId == weaponComponentB.currentWeaponId
            && weaponComponentA.currentWeaponLevel == weaponComponentB.currentWeaponLevel) {
          player.collectedWeapons.RemoveAt(j);
          Destroy(weaponB);
          weaponComponentA.currentWeaponLevel++;
          MessageDispatcher.SendMessage(Constants.Mess_UpdateDataWeapon);
          player.nextAvailableWeaponIndex--;
          playerUi.UpdateWeaponLevelTexts();
          playerUi.CheckButtonWeapon();
        }
      }
    }
  }


  private void CreateWeaponAtPosition(GameObject weaponPrefab, Transform position) {
    GameObject newWeapon = Instantiate(weaponPrefab, position.position,
        position.rotation, position.parent);
    Weapon weaponComponent = newWeapon.GetComponent<Weapon>();
    weaponComponent.currentWeaponLevel = player.levelrandom;
    player.collectedWeapons.Add(newWeapon);
  }

  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_addWeapon, AddWeapon);
    MessageDispatcher.RemoveListener(Constants.Mess_playerDie, ResetWeapon);
    MessageDispatcher.RemoveListener(Constants.Mess_LevelWeapon, SetDataLevel);
  }

  private void SaveCollectedWeapons() {
    PlayerPrefs.SetInt(Constants.PrefsKey_CollectedWeaponsCount, player.collectedWeapons.Count);

    string weaponLevelsData = "";
    foreach (GameObject weapon in player.collectedWeapons) {
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
        if (i < player.weaponPositions.Count && player.weaponPositions[i] != null) {
          CreateWeaponAtPosition(player.weaponPrefab, player.weaponPositions[i]);
          Weapon weaponComponent = player.collectedWeapons[i].GetComponent<Weapon>();
          weaponComponent.currentWeaponLevel = int.Parse(weaponLevels[i]);
          WeaponDataLoader.Ins.LoadWeaponInfo(weaponComponent.currentWeaponId, weaponComponent.currentWeaponLevel);
          player.nextAvailableWeaponIndex++;
        }
      }
    }
    else {
      if (player.weaponPositions.Count > 0 && player.weaponPositions[0] != null) {
        CreateWeaponAtPosition(player.weaponPrefab, player.weaponPositions[0]);
        Weapon weaponComponent = player.collectedWeapons[0].GetComponent<Weapon>();
        weaponComponent.currentWeaponLevel = 1;
        player.nextAvailableWeaponIndex++;
      }

      if (player.weaponPositions.Count > 1 && player.weaponPositions[1] != null) {
        CreateWeaponAtPosition(player.weaponPrefab, player.weaponPositions[1]);
        Weapon weaponComponent = player.collectedWeapons[1].GetComponent<Weapon>();
        weaponComponent.currentWeaponLevel = 2;
        player.nextAvailableWeaponIndex++;
      }
    }
  }

  private void ResetWeapon(IMessage msg) {
    foreach (GameObject weapon in player.collectedWeapons) {
      Destroy(weapon);
      playerUi.CheckButtonWeapon();
    }

    player.collectedWeapons.Clear();
    player.nextAvailableWeaponIndex = 1;

    if (player.weaponPositions.Count > 0 && player.weaponPositions[0] != null) {
      foreach (WeaponPositionInfo weaponInfo in player.weaponPositionInfo) {
        if (weaponInfo.positionWeapon != null) {
          GameObject newWeapon = Instantiate(player.weaponPrefab, weaponInfo.positionWeapon.position,
              weaponInfo.positionWeapon.rotation, transform.parent);
          player.collectedWeapons.Add(newWeapon);

          Weapon weaponComponent = newWeapon.GetComponent<Weapon>();
          weaponComponent.currentWeaponLevel = weaponInfo.currentLevelWeapon;
          player.nextAvailableWeaponIndex++;
        }
      }
    }

    SaveCollectedWeapons();
  }
}