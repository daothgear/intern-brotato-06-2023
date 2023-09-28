using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimeManager : MonoBehaviour {
  public float timer;
  public float totalTimer;
  public int currentSubWave;
  public WaveData.WaveInfo waveInfo;
  [SerializeField] private GameObject wallCheck;

  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Ins;
  }

  private WaveDataLoader waveDataLoader {
    get => WaveDataLoader.Ins;
  }

  [SerializeField] private TextWave textWave;

  // Flag to control time updates
  public bool isTimeStopped = false;
  public bool isSpawnEnemy;
  private void OnValidate() {
    if (textWave == null) {
      textWave = GetComponent<TextWave>();
    }
  }

  private void Start() {
    waveInfo = waveDataLoader.LoadWaveInfo(1);
    isSpawnEnemy = true;
    // Load PlayerPrefs data
    LoadPlayerPrefsData();

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

  private void StartWave() {
    if (currentSubWave == 0) {
      ReferenceHolder.Ins.uicontroller.UIShop.SetActive(false);
      isSpawnEnemy = true;
    }

    if (currentSubWave >= waveInfo.numSubWaves) {
      ShowShop();
      return;
    }

    timer = waveInfo.subWaveTimes[currentSubWave];
    totalTimer = CalculateTotalTimer();
    SpawnEnemies();
    textWave.UpdateText();
  }

  private void StartNextSubWave() {
    currentSubWave++;
    if (currentSubWave >= waveInfo.numSubWaves) {
      ClearEnemies();
      ShowShop();
      return;
    }

    SpawnEnemies();
    timer = waveInfo.subWaveTimes[currentSubWave];
  }

  public void CloseShopUI() {
    waveInfo.currentWave++;
    currentSubWave = 0;
    totalTimer = CalculateTotalTimer();
    ReferenceHolder.Ins.playerHealth.ResetHealth(ReferenceHolder.Ins.player.maxHealth);
    textWave.UpdateText();
    StartWave();
    ReferenceHolder.Ins.uicontroller.UIShop.SetActive(false);

    // Save PlayerPrefs data
    SavePlayerPrefsData();
  }

  private void SpawnEnemies() {
    if (isSpawnEnemy == true) {
      int numEnemies = waveInfo.currentWave * waveInfo.numEnemiesPerWave * (currentSubWave + 1);
      StartCoroutine(SpawnEnemiesWithDelays(numEnemies)); 
    }
  }

  private IEnumerator SpawnEnemiesWithDelays(int numEnemies) {
    float delayTime = waveInfo.spawnDelay;
    WaitForSeconds wait = new WaitForSeconds(delayTime);
    for (int i = 0; i < numEnemies; i++) {
      SpawnEnemyRandom();
      yield return wait;
    }
  }

  public void UpWave() {
    waveInfo.currentWave++;
  }

  private void SpawnEnemyRandom() {
    if (isSpawnEnemy == true) {
      GameObject newEnemy =
          ObjectPool.Ins.SpawnFromPool(Constants.Tag_Enemy, GetRandomSpawnPosition(), Quaternion.identity);
      ObjectPool.Ins.enemyList.Add(newEnemy);
    }
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
    for (int i = 0; i < waveInfo.numSubWaves; i++) {
      total += waveInfo.subWaveTimes[i];
    }

    return total;
  }

  private void ShowShop() {
    // Show the UI shop
    ReferenceHolder.Ins.uicontroller.UIShop.SetActive(true);
    isSpawnEnemy = false;
    ReferenceHolder.Ins.card.RandomLevel();
  }

  public void Stoptime() {
    ReferenceHolder.Ins.uicontroller.UIShop.SetActive(false);
    isSpawnEnemy = false;
    isTimeStopped = true;
    ClearEnemies();
    waveInfo.currentWave = 1;
    currentSubWave = 0;
    SavePlayerPrefsData();
  }

  private void SavePlayerPrefsData() {
    PlayerPrefs.SetInt(Constants.PrefsKey_CurrentWave, waveInfo.currentWave);
    PlayerPrefs.SetInt(Constants.PrefsKey_CurrentSubWave, currentSubWave);
    PlayerPrefs.SetFloat(Constants.PrefsKey_TotalTimer, totalTimer);
    PlayerPrefs.SetInt(Constants.PrefsKey_ShopState, ReferenceHolder.Ins.uicontroller.UIShop.activeSelf ? 1 : 0);
  }

  private void LoadPlayerPrefsData() {
    waveInfo.currentWave = PlayerPrefs.GetInt(Constants.PrefsKey_CurrentWave, 1);
    currentSubWave = PlayerPrefs.GetInt(Constants.PrefsKey_CurrentSubWave, 0);
    totalTimer = PlayerPrefs.GetFloat(Constants.PrefsKey_TotalTimer, CalculateTotalTimer());
    int shopState = PlayerPrefs.GetInt(Constants.PrefsKey_ShopState, 0);
    ReferenceHolder.Ins.uicontroller.UIShop.SetActive(shopState == 1);
  }
}
