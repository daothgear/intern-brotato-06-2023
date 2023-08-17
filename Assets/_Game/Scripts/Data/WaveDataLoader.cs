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

  public void ReceiveData(string fileName, string jsonData)
  {
    if (fileName == Constants.Data_Wave)
    {
      waveData = JsonConvert.DeserializeObject<WaveData>(jsonData);
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