using UnityEngine;
using UnityEngine.UI;

public class TextWave : MonoBehaviour {
  [SerializeField] private Text waveText;
  [SerializeField] private Text subWaveText;
  [SerializeField] private Text countdownText;
  [SerializeField] private Text totalTimerText;

  public void UpdateText() {
    TimeManager timeManager = ReferenceHolder.Ins.timeManager;
    waveText.text = "WAVE " + timeManager.currentWave.ToString();
    subWaveText.text = "Sub wave: " + timeManager.currentSubWave.ToString() + " / " + WaveDataLoader.Ins.numSubWaves.ToString();
    countdownText.text = "Countdown: " + Mathf.Round(timeManager.timer).ToString() + "s";
    totalTimerText.text = Mathf.Round(timeManager.totalTimer).ToString();
  }
}
