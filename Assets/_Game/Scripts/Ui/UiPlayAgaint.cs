using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiPlayAgaint : MonoBehaviour
{
  public void PLayAgaint() {
    SceneManager.LoadScene("StartGame");
  }
}
