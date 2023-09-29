using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdsView : MonoBehaviour {
   [SerializeField] private UiController uiController;
   [SerializeField] private Button buttonExitAds;

   private void OnValidate() {
      if (uiController == null) {
         uiController = GetComponentInParent<UiController>();
      }
   }

   public void OnEnable() {
      StartCoroutine(AdsOff(10f));
   }

   private IEnumerator AdsOff(float delay) {
      yield return new WaitForSeconds(delay);
      buttonExitAds.gameObject.SetActive(true);
   }

   public void ExitViewAds() {
      uiController.UiEndGame.SetActive(false);
      uiController.ads.SetActive(false);
      buttonExitAds.gameObject.SetActive(false);
      uiController.Revival();
   }
}
