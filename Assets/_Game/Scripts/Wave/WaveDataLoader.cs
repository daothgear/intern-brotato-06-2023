using System.IO;
using UnityEngine;

public class WaveDataLoader : MonoBehaviour {
  public WaveData waveData;

  private void Awake() {
    LoadWaveData();
  }

  private void LoadWaveData() {
    string filePath = Path.Combine(Application.streamingAssetsPath, "waveData.json");

    if (File.Exists(filePath)) {
      string jsonData = File.ReadAllText(filePath);
      waveData = JsonUtility.FromJson<WaveData>(jsonData);
    }
    else {
      Debug.LogError("WaveData file not found at path: " + filePath);
    }
  }
}