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
    ReferenceHolder.Ins.playerExp.SaveLevel(ReferenceHolder.Ins.player.characterLevel);

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