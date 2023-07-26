﻿using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using com.ootii.Messages;

public class TimeManager : MonoBehaviour {
  private WaveDataLoader waveDataLoader;
  [SerializeField] private Text waveText;
  [SerializeField] private Text subWaveText;
  [SerializeField] private Text countdownText;
  [SerializeField] private Text totalTimerText;

  private float timer;
  private float totalTimer;
  private int currentWave;
  private int currentSubWave;
  private bool isFinishWave;
  private bool isUiShop;

  [SerializeField] private GameObject wallCheck;
  [SerializeField] private GameObject spawnPointPrefab;
  public GameObject uiShop;

  private PlayerHealth playerHealth;
  private PlayerLoader playerLoader;

  private void OnValidate() {
    if (waveDataLoader == null) {
      waveDataLoader = FindObjectOfType<WaveDataLoader>();
    }

    if (playerHealth == null) {
      playerHealth = FindObjectOfType<PlayerHealth>();
    }
  }

  private void Awake() {
    playerLoader = PlayerLoader.Instance;
    waveDataLoader = WaveDataLoader.Instance;
  }

  private void Start() {
    //MessageDispatcher.AddListener("isFinishWave" , StartNextSubWave);
    uiShop.SetActive(isUiShop);
    StartWave();
  }

  private void Update() {
    if (timer > 0f) {
      timer -= Time.deltaTime;
      totalTimer -= Time.deltaTime;
      if (timer <= 0f) {
        StartNextSubWave();
      }
      UpdateText();
    }
  }

  private void StartWave() {
    currentWave = 1;
    currentSubWave = 1;
    timer = waveDataLoader.subWaveTimes[currentSubWave - 1];
    totalTimer = CalculateTotalTimer();
    SpawnEnemies();
    UpdateText();
  }

  private void StartNextSubWave() {  //IMessage img
    currentSubWave++;
    if (currentSubWave > waveDataLoader.numSubWaves) {
      ClearEnemies();
      isUiShop = true;
      uiShop.SetActive(isUiShop);
      
      // if (isFinishWave == true) {
      //   currentWave++;
      //   currentSubWave = 1;
      //   totalTimer = CalculateTotalTimer();
      //   playerHealth.currentHealth = playerLoader.maxHealth;
      //   playerHealth.UpdateHealthUI();
      // }
      
      currentWave++;
      currentSubWave = 1;
      totalTimer = CalculateTotalTimer();
      playerHealth.currentHealth = playerLoader.maxHealth;
      playerHealth.UpdateHealthUI();
    }

    SpawnEnemies();
    timer = waveDataLoader.subWaveTimes[currentSubWave - 1];
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
    Vector3 spawnPosition = GetRandomSpawnPosition();
    GameObject spawnPoint = Instantiate(spawnPointPrefab, spawnPosition, Quaternion.identity);
    StartCoroutine(SpawnEnemyWithDelay(spawnPoint));
  }

  private IEnumerator SpawnEnemyWithDelay(GameObject spawnPoint) {
    yield return new WaitForSeconds(waveDataLoader.spawnDelay);
    // Spawn enemy
    ObjectPool.Instance.SpawnFromPool("Enemy", spawnPoint.transform.position, Quaternion.identity);
    // Destroy SpawnPoint
    Destroy(spawnPoint);
  }

  private Vector3 GetRandomSpawnPosition() {
    Collider2D wallCollider = wallCheck.GetComponent<Collider2D>();
    Vector3 wallSize = wallCollider.bounds.size;
    Vector3 spawnPosition = wallCheck.transform.position + new Vector3(
        Random.Range(-wallSize.x / 2f, wallSize.x / 2f),
        Random.Range(-wallSize.y / 2f, wallSize.y / 2f),
        Random.Range(-wallSize.z / 2f, wallSize.z / 2f)
    );
    return spawnPosition;
  }

  private void ClearEnemies() {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    foreach (GameObject enemy in enemies) {
      ObjectPool.Instance.ReturnToPool("Enemy", enemy);
    }
  }

  private void UpdateText() {
    waveText.text = "WAVE " + currentWave.ToString();
    subWaveText.text = "Sub wave: " + currentSubWave.ToString() + " / " + waveDataLoader.numSubWaves.ToString();
    countdownText.text = "Countdown: " + Mathf.Round(timer).ToString() + "s";
    totalTimerText.text = Mathf.Round(totalTimer).ToString();
  }

  private float CalculateTotalTimer() {
    float total = 0f;
    for (int i = 0; i < waveDataLoader.numSubWaves; i++) {
      total += waveDataLoader.subWaveTimes[i];
    }

    return total;
  }
}