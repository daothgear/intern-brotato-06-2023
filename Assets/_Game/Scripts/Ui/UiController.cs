using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {
  public GameObject UiEndGame;
  public GameObject UIShop;
  private void Start() {
    UiEndGame.SetActive(false);
  }
  
  public void PLayAgain() {
    SceneManager.LoadScene(Constants.Scene_StartGame);
  }
}
