using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {
  public GameObject UiEndGame;
  public GameObject UIShop;
  public GameObject ads;
  public TMP_Text textViewAds;

  [SerializeField] private ButtonViewAds btnViewAds;
  public float time = 5f;

  private void OnValidate() {
    if (btnViewAds == null) {
      btnViewAds = GetComponentInChildren<ButtonViewAds>();
    }
  }

  private void Start() {
    UiEndGame.SetActive(false);
  }
  
  public void PLayAgain() {
    ReferenceHolder.Ins.timeManager.ResetWave();
    ReferenceHolder.Ins.playerCoin.ResetData();
    ReferenceHolder.Ins.playerExp.ResetLevel();
    ReferenceHolder.Ins.playerWeapon.ResetWeapon();
    SceneManager.LoadScene(Constants.Scene_StartGame);
  }

  private IEnumerator Countdown() {
    if (time > 0) {
      yield return new WaitForSeconds(1f);
      time--;
      textViewAds.text = Mathf.RoundToInt(time).ToString();
      StartCoroutine(Countdown());
    } else {
      textViewAds.text = "Can replay";
      textViewAds.fontSize = 35;
      btnViewAds.ChangeColor();
    }
  }


  public void ShowEndGame() {
    UiEndGame.SetActive(true);
    StartCoroutine(Countdown());
  }

  public void Revival() {
    time = 5f;
    textViewAds.fontSize = 160;
    textViewAds.text = Mathf.RoundToInt(time).ToString();
    btnViewAds.ChangeColor();
    ReferenceHolder.Ins.timeManager.isSpawnEnemy = true;
    ReferenceHolder.Ins.timeManager.isTimeStopped = false;
    ReferenceHolder.Ins.player.currentHealth = ReferenceHolder.Ins.player.maxHealth;
    ReferenceHolder.Ins.playerUi.UpdateHealthUI();
    UiEndGame.SetActive(false);
  }

  public void ViewAds() {
    if (time > 0) {
      ads.SetActive(true);
    }
  }
}
