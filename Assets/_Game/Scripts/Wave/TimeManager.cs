using System.Collections;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TimeManager : MonoBehaviour {
  public float timer;
  public float totalTimer;
  public int currentSubWave;

  [SerializeField] private GameObject wallCheck;

  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Ins;
  }

  private WaveDataLoader waveDataLoader {
    get => WaveDataLoader.Ins;
  }

  public TextWave textWave;
  
  [SerializeField] private GameObject UIShop;

  // Flag to control time updates
  private bool isTimeStopped = false;

  private void OnValidate() {
    if (textWave == null) {
      textWave = GetComponent<TextWave>();
    }
  }

  private void Start() {
    waveDataLoader.LoadWaveInfo(1);
    MessageDispatcher.AddListener(Constants.Mess_playerDie, Stoptime);
    MessageDispatcher.AddListener(Constants.Mess_nextwave, UpWave);
    waveDataLoader.currentWave = 1;
    StartWave();
  }

  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_playerDie, Stoptime);
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
    for (int i = 0; i < waveDataLoader.currentWave - 1; i++) {
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
    waveDataLoader.currentWave++;
    currentSubWave = 0;
    totalTimer = CalculateTotalTimer();
    MessageDispatcher.SendMessage(Constants.Mess_resetHealth);
    textWave.UpdateText();
    StartWave();
    UIShop.SetActive(false);
  }

  private void SpawnEnemies() {
    int numEnemies = waveDataLoader.currentWave * waveDataLoader.numEnemiesPerWave * currentSubWave;
    StartCoroutine(SpawnEnemiesWithDelays(numEnemies));
  }

  private IEnumerator SpawnEnemiesWithDelays(int numEnemies)
  {
    float delayTime = waveDataLoader.spawnDelay;
    WaitForSeconds wait = new WaitForSeconds(delayTime);
    for (int i = 0; i < numEnemies; i++)
    {
      SpawnEnemyRandom();
      yield return wait;
    }
  }
  
  private void UpWave(IMessage img) {
    waveDataLoader.currentWave++;
  }
  private void SpawnEnemyRandom() {
    GameObject newEnemy =
        ObjectPool.Ins.SpawnFromPool(Constants.Tag_Enemy, GetRandomSpawnPosition(), Quaternion.identity);
    ObjectPool.Ins.enemyList.Add(newEnemy);
  }

  private Vector3 GetRandomSpawnPosition() {
    Collider2D wallCollider = wallCheck.GetComponent<Collider2D>();
    Vector3 wallSize = wallCollider.bounds.size;
    Vector3 spawnPosition = wallCheck.transform.position + new Vector3(
        Random.Range(-wallSize.x / 2f, wallSize.x / 2f),
        Random.Range(-wallSize.y / 2f, wallSize.y / 2f)
    );

    return spawnPosition;
  }

  private void ClearEnemies() {
    foreach (GameObject enemy in ObjectPool.Ins.enemyList) {
      ObjectPool.Ins.ReturnToPool(Constants.Tag_Enemy, enemy);
    }

    ObjectPool.Ins.enemyList.Clear();
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
    
    // Debug message to indicate that the shop is shown
    Debug.Log("Shop is shown!");
  }

  private void HideShop() {
    // Hide the UI shop
    UIShop.SetActive(false);
    
    // Debug message to indicate that the shop is hidden
    Debug.Log("Shop is hidden!");
  }

  private void Stoptime(IMessage img) {
    // Set the time stopped flag to true, which will stop further updates of timers.
    isTimeStopped = true;
  }
}