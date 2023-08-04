
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WaveData {
  public List<WaveInfo> WaveInfos;

  [System.Serializable]
  public class WaveInfo {
    public int currentWave;
    public float[] subWaveTimes;
    public int numSubWaves;
    public int numEnemiesPerWave;
    public float spawnDelay;
  }
}
