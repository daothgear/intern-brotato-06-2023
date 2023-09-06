using System;
using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin : Singleton<PlayerCoin> {
  public int coinAmount;
  [SerializeField] private Text textCoin;
  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_doubleMoney, AddCoin);
    MessageDispatcher.AddListener(Constants.Mess_playerDie, ResetData);
    LoadCoinAmount();
    textCoin.text = coinAmount.ToString();
  }

  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_doubleMoney, AddCoin);
  }

  public bool HasEnoughCoins(int amount) {
    return coinAmount >= amount;
  }

  public void DeductCoins(int amount) {
    coinAmount -= amount;
    textCoin.text = coinAmount.ToString();
    SaveCoinAmount();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag(Constants.Tag_Coin)) {
      coinAmount++;
      textCoin.text = coinAmount.ToString();
      ObjectPool.Ins.ReturnToPool(Constants.Tag_Coin, collision.gameObject);
      SaveCoinAmount();
    }
  }

  private void AddCoin(IMessage img) {
    coinAmount *= 2;
    textCoin.text = coinAmount.ToString();
    SaveCoinAmount();
  }

  private void SaveCoinAmount() {
    PlayerPrefs.SetInt(Constants.PrefsKey_Coin, coinAmount);
  }

  private void LoadCoinAmount() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_Coin)) {
      coinAmount = PlayerPrefs.GetInt(Constants.PrefsKey_Coin);
    }
  }

  private void ResetData(IMessage img) {
    coinAmount = 500;
    SaveCoinAmount();
  }
}