using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		public int[] subWaveTimeArray;
		public int[] subWave1stSpawnEnemyData;
		public int subWaveSpawnIncreasePercentage;
	}
}
