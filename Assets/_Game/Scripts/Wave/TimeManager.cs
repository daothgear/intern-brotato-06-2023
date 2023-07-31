using System;
using System.Collections;
using System.Collections.Generic;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TimeManager : Singleton<TimeManager> {
  public float timer;
  public float totalTimer;
  public int currentWave;
  public int currentSubWave;

  [SerializeField] private GameObject wallCheck;

  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Instance;
  }

  private WaveDataLoader waveDataLoader {
    get => WaveDataLoader.Instance;
  }

  private TextWave textWave;

  private bool isShowingShop = false;
  [SerializeField] private GameObject UIShop;
  [SerializeField] private Button ButtonNextLevel;

  // Flag to control time updates
  private bool isTimeStopped = false;
  public List<GameObject> enemyList = new List<GameObject>();

  private void OnValidate() {
    if (textWave == null) {
      textWave = GetComponent<TextWave>();
    }
  }

  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_playerDie, Stoptime);
    currentWave = 1;
    StartWave();
  }

  private void Update() {
    if (!isTimeStopped && timer > 0f) {
      timer -= Time.deltaTime;
      totalTimer -= Time.deltaTime;
      if (timer <= 0f) {
        StartNextSubWave();
      }

      textWave.UpdateText();
    }
  }

  private int CalculateTotalSubWaves() {
    int totalSubWaves = 0;
    for (int i = 0; i < currentWave - 1; i++) {
      totalSubWaves += waveDataLoader.numSubWaves;
    }

    totalSubWaves += currentSubWave;
    return totalSubWaves;
  }

  private void NextLevelButtonClicked() {
    HideShop();
    StartNextSubWave();
  }

  private void StartWave() {
    currentSubWave++;
    if (currentSubWave > waveDataLoader.numSubWaves) {
      ShowShop();
      return;
    }

    timer = waveDataLoader.subWaveTimes[currentSubWave - 1];
    totalTimer = CalculateTotalTimer();
    SpawnEnemies();
    textWave.UpdateText();
  }

  private void StartNextSubWave() {
    currentSubWave++;
    if (currentSubWave > waveDataLoader.numSubWaves) {
      ClearEnemies();
      ShowShop();
      return;
    }

    SpawnEnemies();
    timer = waveDataLoader.subWaveTimes[currentSubWave - 1];
  }

  public void CloseShopUI() {
    currentWave++;
    currentSubWave = 0;
    totalTimer = CalculateTotalTimer();
    MessageDispatcher.SendMessage(Constants.Mess_resetHealth);
    textWave.UpdateText();
    StartWave();
    UIShop.SetActive(false);
  }

  private void SpawnEnemies() {
    int numEnemies = currentWave * waveDataLoader.numEnemiesPerWave * currentSubWave;
    for (int i = 0; i < numEnemies; i++) {
      float delayTime = i * waveDataLoader.spawnDelay;
      StartCoroutine(SpawnEnemyRandomWithDelay(delayTime));
    }
  }

  private IEnumerator SpawnEnemyRandomWithDelay(float delayTime) {
    yield return new WaitForSeconds(delayTime);
    SpawnEnemyRandom();
  }

  private void SpawnEnemyRandom() {
    GameObject newEnemy =
        ObjectPool.Instance.SpawnFromPool(Constants.Tag_Enemy, GetRandomSpawnPosition(), Quaternion.identity);
    enemyList.Add(newEnemy);
  }

  private Vector3 GetRandomSpawnPosition() {
    Collider2D wallCollider = wallCheck.GetComponent<Collider2D>();
    Vector3 wallSize = wallCollider.bounds.size;
    Vector3 spawnPosition = wallCheck.transform.position + new Vector3(
        Random.Range(-wallSize.x / 2f, wallSize.x / 2f),
        Random.Range(-wallSize.y / 2f, wallSize.y / 2f),
        Random.Range(0, 0)
    );

    return spawnPosition;
  }

  private void ClearEnemies() {
    foreach (GameObject enemy in enemyList) {
      ObjectPool.Instance.ReturnToPool(Constants.Tag_Enemy, enemy);
    }

    enemyList.Clear();
  }

  private float CalculateTotalTimer() {
    float total = 0f;
    for (int i = 0; i < waveDataLoader.numSubWaves; i++) {
      total += waveDataLoader.subWaveTimes[i];
    }

    return total;
  }

  private void ShowShop() {
    // Show the UI shop
    UIShop.SetActive(true);

    // Set the flag to true
    isShowingShop = true;

    // Debug message to indicate that the shop is shown
    Debug.Log("Shop is shown!");
  }

  private void HideShop() {
    // Hide the UI shop
    UIShop.SetActive(false);

    // Set the flag to false
    isShowingShop = false;

    // Debug message to indicate that the shop is hidden
    Debug.Log("Shop is hidden!");
  }

  private void Stoptime(IMessage img) {
    // Set the time stopped flag to true, which will stop further updates of timers.
    isTimeStopped = true;
  }
}