using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public float totalTime;
    public bool weaponBuyableAfterFinish;
    public int[] subWaveTimeArray;
    public int[] subWave1stSpawnEnemyData;
    public float subWaveSpawnIncreasePercentage;
}
