using UnityEngine;
using UnityEngine.UI;

public class TextWave : MonoBehaviour {
  [SerializeField] private Text waveText;
  [SerializeField] private Text subWaveText;
  [SerializeField] private Text countdownText;
  [SerializeField] private Text totalTimerText;

  public void UpdateText() {
    if (ReferenceHolder.Ins == null) {
      Debug.Log("Referrenceholder null");
      return;
    }
    if (ReferenceHolder.Ins.timeManager == null) {
      Debug.Log("Check null timemanage: ");
      return;
    }
    TimeManager timeManager = ReferenceHolder.Ins.timeManager;
    waveText.text = "WAVE " + WaveDataLoader.Ins.currentWave.ToString();
    //subWaveText.text = "Sub wave: " + timeManager.currentSubWave.ToString() + " / " + WaveDataLoader.Ins.numSubWaves.ToString();
    //countdownText.text = "Countdown: " + Mathf.Round(timeManager.timer).ToString() + "s";
    totalTimerText.text = Mathf.Round(timeManager.totalTimer).ToString();
    Debug.Log("1: " + Mathf.Round(timeManager.totalTimer));
    Debug.Log("2: " + timeManager.totalTimer);
  }
}
