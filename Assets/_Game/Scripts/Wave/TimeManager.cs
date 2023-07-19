using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
  public float[] subWaveTimes;

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
  [SerializeField] private GameObject spawnPointPrefab;

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
    timer = subWaveTimes[currentSubWave - 1];
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
      totalTimer = CalculateTotalTimer();
    }
    SpawnEnemies();
    timer = subWaveTimes[currentSubWave - 1];
  }

  private void SpawnEnemies()
  {
    int numEnemies = currentWave * numEnemiesPerWave * currentSubWave;
    for (int i = 0; i < numEnemies; i++)
    {
      float delayTime = i * spawnDelay;
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
    GameObject spawnPoint = Instantiate(spawnPointPrefab, spawnPosition, Quaternion.identity);
    StartCoroutine(SpawnEnemyWithDelay(spawnPoint));
  }

  private IEnumerator SpawnEnemyWithDelay(GameObject spawnPoint)
  {
    yield return new WaitForSeconds(spawnDelay);

    // Spawn enemy
    ObjectPool.Instance.SpawnFromPool("Enemy", spawnPoint.transform.position, Quaternion.identity);

    // Destroy SpawnPoint
    Destroy(spawnPoint);
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
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    foreach (GameObject enemy in enemies)
    {
      ObjectPool.Instance.ReturnToPool("Enemy", enemy);
    }
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
