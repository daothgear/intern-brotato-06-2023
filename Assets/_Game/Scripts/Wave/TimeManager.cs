using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
  public float waveDuration;
  public int numSubWaves = 3;
  public int numEnemiesPerWave = 10;

  public Text waveText;
  public Text subWaveText;
  public Text countdownText;
  public Text totalTimerText;

  private float timer;
  private float totalTimer;
  private int currentWave;
  private int currentSubWave;

  private void Start()
  {
    StartWave();
  }

  private void Update()
  {
    if ( timer > 0f )
    {
      timer -= Time.deltaTime;
      totalTimer -= Time.deltaTime;

      if ( timer <= 0f )
      {
        StartNextSubWave();
      }

      UpdateText();
    }
  }

  private void StartWave()
  {
    currentWave = 1;
    currentSubWave = 1;
    timer = waveDuration;
    totalTimer = waveDuration * numSubWaves;
    SpawnEnemies();
    UpdateText();
  }

  private void StartNextSubWave()
  {
    currentSubWave++;
    if ( currentSubWave > numSubWaves )
    {
      ClearEnemies();
      currentWave++;
      currentSubWave = 1;
      totalTimer = waveDuration * numSubWaves;// Reset totalTimer at the start of a new wave
    }
    SpawnEnemies();
    timer = waveDuration;
  }

  private void SpawnEnemies()
  {
    int numEnemies = currentWave * numEnemiesPerWave * currentSubWave;
    Debug.Log("Spawned " + numEnemies + " enemies.");
  }

  private void ClearEnemies()
  {
    Debug.Log("Cleared enemies of wave " + ( currentWave - 1 ));
  }

  private void UpdateText()
  {
    waveText.text = "Wave: " + currentWave.ToString();
    subWaveText.text = "Sub wave: " + currentSubWave.ToString() + " / " + numSubWaves.ToString();
    countdownText.text = "Countdown: " + Mathf.Round(timer).ToString() + "s";
    totalTimerText.text = "Total Timer: " + Mathf.Round(totalTimer).ToString() + "s";
  }
}
