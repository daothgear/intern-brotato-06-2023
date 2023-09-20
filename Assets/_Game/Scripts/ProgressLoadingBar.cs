using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ProgressLoadingBar : MonoBehaviour {
  public Slider loadingSlider;
  public float waitTimeBetweenFiles = 0.375f;
  private void Start() {
    StartCoroutine(LoadData());
  }

  private IEnumerator LoadData() {
    string[] dataFiles = { Constants.Data_Enemy, Constants.Data_Player, Constants.Data_Wave, Constants.Data_Weapon };
    float totalFiles = dataFiles.Length;
    float loadedFiles = 0;

    foreach (string fileName in dataFiles) {
      string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
      string jsonData = "";

      if (filePath.Contains("://")) {
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success) {
          jsonData = www.downloadHandler.text;
        }
      }
      else {
        jsonData = File.ReadAllText(filePath);
      }

      WeaponDataLoader.Ins.ReceiveData(fileName, jsonData);
      WaveDataLoader.Ins.ReceiveData(fileName, jsonData);
      PlayerDataLoader.Ins.ReceiveData(fileName, jsonData);
      EnemyDataLoader.Ins.ReceiveData(fileName, jsonData);
      loadedFiles++;
      float progress = loadedFiles / totalFiles;
      loadingSlider.value = progress;

      yield return new WaitForSeconds(waitTimeBetweenFiles);
    }
    SceneManager.LoadScene(Constants.Scene_Menu);
  }
}