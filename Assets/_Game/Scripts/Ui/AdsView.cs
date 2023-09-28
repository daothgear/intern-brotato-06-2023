using System.Collections;
using UnityEngine;

public class AdsView : MonoBehaviour
{
   public void OnEnable() {
      StartCoroutine(AdsOff(10f));
   }

   private IEnumerator AdsOff(float delay) {
      yield return new WaitForSeconds(delay);
      Destroy(gameObject);
      ReferenceHolder.Ins.uicontroller.UiEndGame.SetActive(false);
   }
}
