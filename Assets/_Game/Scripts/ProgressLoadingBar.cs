using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ProgressLoadingBar : MonoBehaviour {
  public Slider loadingSlider;

  private void Start() {
    StartCoroutine(LoadData());
  }

  private IEnumerator LoadData() {
    string[] dataFiles = { Constants.Data_Enemy, Constants.Data_Player, Constants.Data_Wave, Constants.Data_Weapon };
    float totalFiles = dataFiles.Length;
    float loadedFiles = 0;

    foreach (string fileName in dataFiles) {
      string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

      if (filePath.Contains("://")) {
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();
      }
      else {
        string jsonData = File.ReadAllText(filePath);
      }

      loadedFiles++;
      float progress = loadedFiles / totalFiles;
      loadingSlider.value = progress;
      Invoke("LoadSceneGame", 2f);
    }
  }

  private void LoadSceneGame() {
    SceneManager.LoadScene(Constants.Scene_Menu);
  }
}