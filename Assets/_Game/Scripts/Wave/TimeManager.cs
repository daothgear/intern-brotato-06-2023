using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
  public float[] subWaveTimes; // Mảng chứa thời gian của các subwave

  public int numSubWaves = 3;
  public int numEnemiesPerWave = 10;
  public float spawnDelay = 0.5f; // Thời gian trễ giữa việc spawn các enemy

  public Text waveText;
  public Text subWaveText;
  public Text countdownText;
  public Text totalTimerText;

  private float timer;
  private float totalTimer;
  private int currentWave;
  private int currentSubWave;

  [SerializeField] private GameObject wallCheck;
  [SerializeField] private GameObject enemyPrefab;

  private void Start()
  {
    StartWave();
  }

  private void Update()
  {
    if (timer > 0f)
    {
      timer -= Time.deltaTime;
      totalTimer -= Time.deltaTime;

      if (timer <= 0f)
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
    timer = subWaveTimes[currentSubWave - 1]; // Sử dụng giá trị time tương ứng cho subwave hiện tại
    totalTimer = CalculateTotalTimer();
    SpawnEnemies();
    UpdateText();
  }

  private void StartNextSubWave()
  {
    currentSubWave++;
    if (currentSubWave > numSubWaves)
    {
      ClearEnemies();
      currentWave++;
      currentSubWave = 1;
      totalTimer = CalculateTotalTimer(); // Reset totalTimer at the start of a new wave
    }
    SpawnEnemies();
    timer = subWaveTimes[currentSubWave - 1]; // Sử dụng giá trị time tương ứng cho subwave hiện tại
  }

  private void SpawnEnemies()
  {
    int numEnemies = currentWave * numEnemiesPerWave * currentSubWave;
    for (int i = 0; i < numEnemies; i++)
    {
      float delayTime = i * spawnDelay; // Điều chỉnh độ trễ giữa các enemy spawn
      StartCoroutine(SpawnEnemyRandomWithDelay(delayTime));
    }
    Debug.Log("Spawned " + numEnemies + " enemies.");
  }

  private IEnumerator SpawnEnemyRandomWithDelay(float delayTime)
  {
    yield return new WaitForSeconds(delayTime);
    SpawnEnemyRandom();
  }

  private void SpawnEnemyRandom()
  {
    Vector3 spawnPosition = GetRandomSpawnPosition();
    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    // Cập nhật các thông tin của enemy (ví dụ: loại enemy, hướng di chuyển, ...)
  }

  private Vector3 GetRandomSpawnPosition()
  {
    Collider2D wallCollider = wallCheck.GetComponent<Collider2D>();
    Vector3 wallSize = wallCollider.bounds.size;
    Vector3 spawnPosition = wallCheck.transform.position + new Vector3(
        Random.Range(-wallSize.x / 2f, wallSize.x / 2f),
        Random.Range(-wallSize.y / 2f, wallSize.y / 2f),
        Random.Range(-wallSize.z / 2f, wallSize.z / 2f)
    );
    return spawnPosition;
  }

  private void ClearEnemies()
  {
    Debug.Log("Cleared enemies of wave " + (currentWave - 1));
  }

  private void UpdateText()
  {
    waveText.text = "Wave: " + currentWave.ToString();
    subWaveText.text = "Sub wave: " + currentSubWave.ToString() + " / " + numSubWaves.ToString();
    countdownText.text = "Countdown: " + Mathf.Round(timer).ToString() + "s";
    totalTimerText.text = "Total Timer: " + Mathf.Round(totalTimer).ToString() + "s";
  }

  private float CalculateTotalTimer()
  {
    float total = 0f;
    for (int i = 0; i < numSubWaves; i++)
    {
      total += subWaveTimes[i];
    }
    return total;
  }
}
