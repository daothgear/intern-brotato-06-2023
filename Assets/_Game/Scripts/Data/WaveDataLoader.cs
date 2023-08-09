using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class WaveDataLoader : InstanceStatic<WaveDataLoader> {
  private WaveData waveData;
  public int currentWave;
  public float[] subWaveTimes;
  public int numSubWaves;
  public int numEnemiesPerWave;
  public float spawnDelay;

  protected override void Awake() {
    ReadData();
  }

  public void ReadData() {
    string waveLevelPath = Path.Combine(Application.streamingAssetsPath, Constants.Data_Wave);
    if (File.Exists(waveLevelPath)) {
      string waveDataJson = File.ReadAllText(waveLevelPath);
      waveData = JsonConvert.DeserializeObject<WaveData>(waveDataJson);
    }
  }

  public void LoadWaveInfo(int currenwave) {
    foreach (var waveInfo in waveData.WaveInfos) {
      if (waveInfo.currentWave == currenwave) {
        WaveData.WaveInfo currentwaveInfo = waveInfo;
        currentWave = currentwaveInfo.currentWave;
        subWaveTimes = currentwaveInfo.subWaveTimes;
        numSubWaves = currentwaveInfo.numSubWaves;
        numEnemiesPerWave = currentwaveInfo.numEnemiesPerWave;
        spawnDelay = currentwaveInfo.spawnDelay;
        break;
      }
    }
  }
}