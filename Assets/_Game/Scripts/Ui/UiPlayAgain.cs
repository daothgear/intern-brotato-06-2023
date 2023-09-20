using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiPlayAgain : MonoBehaviour
{
  public void PLayAgain() {
    SceneManager.LoadScene(Constants.Scene_StartGame);
  }
}
