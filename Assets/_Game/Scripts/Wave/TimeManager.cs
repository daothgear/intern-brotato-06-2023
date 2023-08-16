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

    // Load PlayerPrefs data
    LoadPlayerPrefsData();

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

  private void StartWave() {
    if (currentSubWave == 0) {
      UIShop.SetActive(false);
    }

    if (currentSubWave >= waveDataLoader.numSubWaves) {
      ShowShop();
      return;
    }

    timer = waveDataLoader.subWaveTimes[currentSubWave];
    totalTimer = CalculateTotalTimer();
    SpawnEnemies();
    textWave.UpdateText();
  }
  private void StartNextSubWave() {
    currentSubWave++;
    if (currentSubWave >= waveDataLoader.numSubWaves) {
      ClearEnemies();
      ShowShop();
      return;
    }

    SpawnEnemies();
    timer = waveDataLoader.subWaveTimes[currentSubWave];
  }

  public void CloseShopUI() {
    waveDataLoader.currentWave++;
    currentSubWave = 0;
    totalTimer = CalculateTotalTimer();
    MessageDispatcher.SendMessage(Constants.Mess_resetHealth);
    textWave.UpdateText();
    StartWave();
    UIShop.SetActive(false);

    // Save PlayerPrefs data
    SavePlayerPrefsData();
  }

  private void SpawnEnemies() {
    int numEnemies = waveDataLoader.currentWave * waveDataLoader.numEnemiesPerWave * (currentSubWave + 1);
    StartCoroutine(SpawnEnemiesWithDelays(numEnemies));
  }

  private IEnumerator SpawnEnemiesWithDelays(int numEnemies) {
    float delayTime = waveDataLoader.spawnDelay;
    WaitForSeconds wait = new WaitForSeconds(delayTime);
    for (int i = 0 ; i < numEnemies ; i++) {
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
  }

  private void Stoptime(IMessage img) {
    UIShop.SetActive(false);
    isTimeStopped = true;
    waveDataLoader.currentWave = 1;
    currentSubWave = 0;
    SavePlayerPrefsData();
  }

  private void SavePlayerPrefsData() {
    PlayerPrefs.SetInt(Constants.PrefsKey_CurrentWave, waveDataLoader.currentWave);
    PlayerPrefs.SetInt(Constants.PrefsKey_CurrentSubWave, currentSubWave);
    PlayerPrefs.SetFloat(Constants.PrefsKey_TotalTimer, totalTimer);
    PlayerPrefs.SetInt(Constants.PrefsKey_ShopState, UIShop.activeSelf ? 1 : 0);
  }

  private void LoadPlayerPrefsData() {
    waveDataLoader.currentWave = PlayerPrefs.GetInt(Constants.PrefsKey_CurrentWave, 1);
    currentSubWave = PlayerPrefs.GetInt(Constants.PrefsKey_CurrentSubWave, 0);
    totalTimer = PlayerPrefs.GetFloat(Constants.PrefsKey_TotalTimer, CalculateTotalTimer());
    int shopState = PlayerPrefs.GetInt(Constants.PrefsKey_ShopState, 0);
    UIShop.SetActive(shopState == 1);
  }
}
