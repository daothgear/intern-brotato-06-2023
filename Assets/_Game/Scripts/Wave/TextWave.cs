using UnityEngine;
using UnityEngine.UI;

public class TextWave : MonoBehaviour {
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
