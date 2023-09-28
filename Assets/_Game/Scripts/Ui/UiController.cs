using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {
  public GameObject UiEndGame;
  public GameObject UIShop;
  public TMP_Text textViewAds;

  public float time = 5f;
  public bool isCountingDown = false;

  private void Start() {
    UiEndGame.SetActive(false);
  }
  
  public void PLayAgain() {
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
    }
  }


  public void ShowEndGame() {
    UiEndGame.SetActive(true);
    isCountingDown = true;
    StartCoroutine(Countdown());
  }
}
