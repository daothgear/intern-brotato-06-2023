using System;
using UnityEngine;

public class UiController : MonoBehaviour {
  public GameObject UiEndGame;
  public GameObject UIShop;
  private void Start() {
    UiEndGame.SetActive(false);
  }
}
