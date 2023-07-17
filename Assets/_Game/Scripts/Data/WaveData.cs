[System.Serializable]
public class WaveData
{
  public WaveInfo[] waveInfo;

  [System.Serializable]
  public class WaveInfo
  {
    public int waveID;
    public float totalTime;
    public float weaponBuyableAfterFinish;
    public SubWaveInfo[] subWaves;
  }

  [System.Serializable]
  public class SubWaveInfo
  {
    public float duration;
    public int numEnemies;
  }
}
