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

  private float waveTimer;
  private float subWaveTimer;
  private int currentWave;
  private int currentSubWave;
  private bool isFirstWave;

  private void Start()
  {
    StartWave();
  }

  private void Update()
  {
    if (waveTimer > 0f)
    {
      waveTimer -= Time.deltaTime;
      subWaveTimer -= Time.deltaTime;

      if (subWaveTimer <= 0f)
      {
        StartNextSubWave();
      }

      if (waveTimer <= 0f)
      {
        StartNextWave();
      }

      UpdateText();
    }
  }

  private void StartWave()
  {
    currentWave = 1;
    currentSubWave = 1;
    waveTimer = waveDuration;
    subWaveTimer = waveDuration / numSubWaves;
    SpawnEnemies();
    isFirstWave = true;
    UpdateText();
  }

  private void StartNextSubWave()
  {
    currentSubWave++;
    if (currentSubWave > numSubWaves)
    {
      ClearEnemies();
      StartNextWave();
    }
    else
    {
      subWaveTimer = waveDuration / numSubWaves;
      SpawnEnemies();
    }
  }

  private void StartNextWave()
  {
    currentWave++;
    currentSubWave = 1;
    waveTimer = waveDuration;
    SpawnEnemies();
    isFirstWave = false;
  }

  private void SpawnEnemies()
  {
    int numEnemies = currentWave * numEnemiesPerWave * currentSubWave;
    Debug.Log("Spawned " + numEnemies + " enemies.");
  }

  private void ClearEnemies()
  {
    if (currentSubWave > 0)
    {
      Debug.Log("Cleared enemies of wave " + currentWave);
    }
  }


  private void UpdateText()
  {
    waveText.text = "Wave: " + currentWave.ToString();
    subWaveText.text = "Sub wave: " + currentSubWave.ToString() + " / " + numSubWaves.ToString();
    countdownText.text = "Countdown: " + Mathf.Round(waveTimer).ToString() + "s";
  }
}
