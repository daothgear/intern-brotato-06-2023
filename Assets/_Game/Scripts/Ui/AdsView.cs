using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdsView : MonoBehaviour {
  [SerializeField] private UiController uiController;
  [SerializeField] private TMP_Text texttimeViewAds;
  [SerializeField] private float timeViewAds = 10f;
  
  public void OnEnable() {
    StartCoroutine(AdsCountdown());
  }

  private IEnumerator AdsCountdown() {
    while (timeViewAds > 0) {
      texttimeViewAds.text = "View ads finish after " + timeViewAds.ToString("0") + " s";
      yield return new WaitForSeconds(1f);
      timeViewAds -= 1f;
    }

    texttimeViewAds.text = "Ads Finished!";
    ExitViewAds();
  }

  public void ExitViewAds() {
    uiController.UiEndGame.SetActive(false);
    uiController.Uiads.SetActive(false);
    ReferenceHolder.Ins.uiPlayAgain.Revival();
    timeViewAds = 10f;
  }
}