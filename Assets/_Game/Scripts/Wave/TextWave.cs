using UnityEngine;
using UnityEngine.UI;

public class TextWave : MonoBehaviour {
  [SerializeField] private Text waveText;
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
    totalTimerText.text = Mathf.Round(timeManager.totalTimer).ToString();
  }
}
