using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
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

	public List<WaveInfo> waveInfo = new List<WaveInfo>();
}
