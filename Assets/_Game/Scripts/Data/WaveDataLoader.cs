using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class WaveDataLoader : InstanceStatic<WaveDataLoader> {
  private WaveData waveData;

  public void ReceiveData(string fileName, string jsonData)
  {
    if (fileName == Constants.Data_Wave)
    {
      waveData = JsonConvert.DeserializeObject<WaveData>(jsonData);
    }
  }

  public WaveData.WaveInfo LoadWaveInfo(int currenwave) {
    foreach (WaveData.WaveInfo waveInfo in waveData.WaveInfos) {
      if (waveInfo.currentWave == currenwave) {
        return waveInfo;
      }
    }
    return null;
  }
}
