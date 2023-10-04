using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiPlayAgain : MonoBehaviour {
  [SerializeField] private TMP_Text textViewAds;
  [SerializeField] private ButtonViewAds btnViewAds;
  public float currenttimeViewAds;
  public float currentDelayTime = 5f;

  private void Awake() {
    currenttimeViewAds = currentDelayTime;
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

    textViewAds.text = "Can replay";
    textViewAds.fontSize = 35;
    btnViewAds.ChangeColor();
  }
  

  public void Revival() {
    currenttimeViewAds = currentDelayTime;
    textViewAds.fontSize = 160;
    textViewAds.text = Mathf.RoundToInt(currenttimeViewAds).ToString();
    btnViewAds.ChangeColor();
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