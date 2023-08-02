using System;
using System.Collections;
using System.Collections.Generic;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;

public class TextWave : MonoBehaviour {
  private TimeManager timeManger {
    get => TimeManager.Instance;
  }
  private WaveDataLoader waveDataLoader {
    get => WaveDataLoader.Instance;
  }
  [SerializeField] private Text waveText;
  //[SerializeField] private Text subWaveText;
  //[SerializeField] private Text countdownText;
  [SerializeField] private Text totalTimerText;

  public void UpdateText() {
    waveText.text = "WAVE " + timeManger.currentWave.ToString();
    //subWaveText.text = "Sub wave: " + timeManger.currentSubWave.ToString() + " / " + waveDataLoader.numSubWaves.ToString();
   //countdownText.text = "Countdown: " + Mathf.Round(timeManger.timer).ToString() + "s";
    totalTimerText.text = Mathf.Round(timeManger.totalTimer).ToString();
  }
}
