using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiPlayAgain : MonoBehaviour {
  [SerializeField] private TMP_Text textViewAds;
  [SerializeField] private GameObject btnViewAds;
  [SerializeField] private GameObject UiCountDown;
  public float currenttimeViewAds;

  public float delaytime = 5f;

  private void Awake() {
    currenttimeViewAds = delaytime;
  }

  private void OnEnable() {
    StartCoroutine(Countdown());
  }

  public void PLayAgain() {
    ReferenceHolder.Ins.timeManager.ResetWave();
    ReferenceHolder.Ins.playerCoin.ResetData();
    ReferenceHolder.Ins.playerExp.ResetLevel();
    ReferenceHolder.Ins.playerWeapon.ResetWeapon();
    SceneManager.LoadScene(Constants.Scene_StartGame);
  }

  public IEnumerator Countdown() {
    while (currenttimeViewAds > 0) {
      yield return new WaitForSeconds(1f);
      currenttimeViewAds--;
      textViewAds.text = Mathf.RoundToInt(currenttimeViewAds).ToString();
    }
    
    UiCountDown.SetActive(false);
    btnViewAds.SetActive(false);
  }
  

  public void Revival() {
    ReferenceHolder.Ins.player.die = false;
    btnViewAds.SetActive(true);
    UiCountDown.SetActive(true);
    currenttimeViewAds = delaytime;
    textViewAds.text = Mathf.RoundToInt(currenttimeViewAds).ToString();
    //level
    ReferenceHolder.Ins.playerExp.SaveLevel(ReferenceHolder.Ins.player.playerInfo.characterID);

    //time
    ReferenceHolder.Ins.timeManager.SavePlayerPrefsData(ReferenceHolder.Ins.timeManager.currentWave);
    
    //coin
    ReferenceHolder.Ins.playerCoin.SaveCoinAmount(ReferenceHolder.Ins.player.lastCoin);
    
    //weapon
    ReferenceHolder.Ins.player.nextAvailableWeaponIndex = ReferenceHolder.Ins.player.lastnextAvailableWeaponIndex;
    foreach (GameObject weaponObject in ReferenceHolder.Ins.player.collectedWeapons) {
      Destroy(weaponObject);
    }

    ReferenceHolder.Ins.player.collectedWeapons.Clear();

    for (int i = 0; i < ReferenceHolder.Ins.player.lastWeapon.Count; i++) {
      if (i < ReferenceHolder.Ins.player.weaponPositions.Count && ReferenceHolder.Ins.player.weaponPositions[i] != null) {
        Transform weaponObject = Instantiate(ReferenceHolder.Ins.player.weaponPrefab, ReferenceHolder.Ins.player.weaponPositions[i].position, ReferenceHolder.Ins.player.weaponPositions[i].rotation).transform;
        weaponObject.SetParent(ReferenceHolder.Ins.player.weaponParent);
        Weapon weaponComponent = weaponObject.GetComponent<Weapon>();
        weaponComponent.currentWeaponId = ReferenceHolder.Ins.player.lastWeapon[i].weaponID;
        weaponComponent.currentWeaponLevel = ReferenceHolder.Ins.player.lastWeapon[i].currentlevel;
        weaponComponent.UpdateInfo();
        ReferenceHolder.Ins.player.collectedWeapons.Add(weaponObject.gameObject);
      }
    }

    ReferenceHolder.Ins.playerWeapon.SaveCollectedWeapons();

    ReferenceHolder.Ins.timeManager.isSpawnEnemy = true;
    ReferenceHolder.Ins.timeManager.isTimeStopped = false;
    ReferenceHolder.Ins.player.currentHealth = ReferenceHolder.Ins.player.maxHealth;
    ReferenceHolder.Ins.playerUi.UpdateHealthUI();
    ReferenceHolder.Ins.uicontroller.UiEndGame.SetActive(false);
  }

  public void ViewAds() {
    if (currenttimeViewAds > 0) {
      ReferenceHolder.Ins.uicontroller.Uiads.SetActive(true);
    }
  }
}