using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdsView : MonoBehaviour {
  [SerializeField] private UiController uiController;
  [SerializeField] private TMP_Text texttimeViewAds;
  [SerializeField] private float timeViewAds = 10f;
  [SerializeField] private float currenttimeViewAds;

  private void Awake() {
    currenttimeViewAds = timeViewAds;
  }

  public void OnEnable() {
    StartCoroutine(AdsCountdown());
  }

  private IEnumerator AdsCountdown() {
    while (currenttimeViewAds > 0) {
      texttimeViewAds.text = "View ads finish after " + currenttimeViewAds.ToString("0") + " s";
      yield return new WaitForSeconds(1f);
      currenttimeViewAds -= 1f;
    }

    texttimeViewAds.text = "Ads Finished!";
    ExitViewAds();
  }

  public void ExitViewAds() {
    uiController.UiEndGame.SetActive(false);
    uiController.Uiads.SetActive(false);
    ReferenceHolder.Ins.uiPlayAgain.Revival();
    currenttimeViewAds = timeViewAds;
  }
}