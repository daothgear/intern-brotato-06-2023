using System.IO;
using UnityEngine;

public class WaveDataLoader : Singleton<WaveDataLoader> {
  private WaveData waveData;
  public float[] subWaveTimes;
  public int numSubWaves;
  public int numEnemiesPerWave;
  public float spawnDelay;
  protected override void Awake() {
    base.Awake();
    LoadWaveData();
  }

  private void LoadWaveData() {
    string filePath = Path.Combine(Application.streamingAssetsPath, Constants.Data_Wave);

    if (File.Exists(filePath)) {
      string jsonData = File.ReadAllText(filePath);
      waveData = JsonUtility.FromJson<WaveData>(jsonData);
      subWaveTimes = waveData.subWaveTimes;
      numSubWaves = waveData.numSubWaves;
      numEnemiesPerWave = waveData.numEnemiesPerWave;
      spawnDelay = waveData.spawnDelay;
    }
    else {
      Debug.LogError("WaveData file not found at path: " + filePath);
    }
  }
}