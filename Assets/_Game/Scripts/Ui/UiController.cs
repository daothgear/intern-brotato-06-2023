using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {
  public GameObject UiEndGame;
  public GameObject UIShop;
  public GameObject Uiads;

  private void Start() {
    UiEndGame.gameObject.SetActive(false);
  }
  
  public void ShowEndGame() {
    UiEndGame.SetActive(true);
    StartCoroutine(ReferenceHolder.Ins.uiPlayAgain.Countdown());
  }
}
