using UnityEngine;
using UnityEngine.UI;

public class TextWave : MonoBehaviour {
  [SerializeField] private Text waveText;
  [SerializeField] private Text totalTimerText;

  public void UpdateText() {
    TimeManager timeManager = ReferenceHolder.Ins.timeManager;
    waveText.text = "WAVE " + WaveDataLoader.Ins.currentWave.ToString();
    totalTimerText.text = Mathf.Round(timeManager.totalTimer).ToString();
  }
}
