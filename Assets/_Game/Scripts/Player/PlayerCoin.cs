using System;
using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin : Singleton<PlayerCoin> {
  [SerializeField] private int coinAmount;
  [SerializeField] private Text textCoin;

  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_doubleMoney, AddCoin);
    coinAmount = 0;
    textCoin.text = coinAmount.ToString();
  }

  private void OnDestroy() {
    MessageDispatcher.AddListener(Constants.Mess_doubleMoney, AddCoin);
  }

  public bool HasEnoughCoins(int amount) {
    return coinAmount >= amount;
  }

  public void DeductCoins(int amount) {
    coinAmount -= amount;
    textCoin.text = coinAmount.ToString();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag(Constants.Tag_Coin)) {
      coinAmount++;
      textCoin.text = coinAmount.ToString();
      ObjectPool.Instance.ReturnToPool(Constants.Tag_Coin, collision.gameObject);
      Debug.Log("Coin return done");
    }
  }

  private void AddCoin(IMessage img) {
    coinAmount *= 2;
    textCoin.text = coinAmount.ToString();
  }
}
