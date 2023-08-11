using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ProgressLoadingBar : MonoBehaviour {
  public Slider loadingSlider;
  public float waitTimeBetweenFiles = 1f;

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

      float targetValue = progress;
      float currentValue = loadingSlider.value;

      while (currentValue < targetValue) {
        currentValue += Time.deltaTime * (1.0f / waitTimeBetweenFiles);
        loadingSlider.value = currentValue;
        yield return null;
      }
    }
    SceneManager.LoadScene(Constants.Scene_Menu);
  }
}