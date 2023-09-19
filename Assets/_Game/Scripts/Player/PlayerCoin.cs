using System;
using com.ootii.Messages;
using UnityEngine;

public class PlayerCoin : MonoBehaviour {
  private Player player;
  private PlayerUi playerUi;

  private void OnValidate() {
    if (player == null) {
      player = GetComponent<Player>();
    }

    if (playerUi == null) {
      playerUi = GetComponent<PlayerUi>();
    }
  }

  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_doubleMoney, AddCoin);
    MessageDispatcher.AddListener(Constants.Mess_playerDie, ResetData);
    LoadCoinAmount();
    playerUi.GetTextCoin();
  }

  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_doubleMoney, AddCoin);
    MessageDispatcher.RemoveListener(Constants.Mess_playerDie, ResetData);
  }

  public bool HasEnoughCoins(int amount) {
    return player.coinAmount >= amount;
  }

  public void DeductCoins(int amount) {
    player.coinAmount -= amount;
    playerUi.GetTextCoin();
    SaveCoinAmount();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag(Constants.Tag_Coin)) {
      player.coinAmount++;
      playerUi.GetTextCoin();
      ObjectPool.Ins.ReturnToPool(Constants.Tag_Coin, collision.gameObject);
      SaveCoinAmount();
    }
  }

  private void AddCoin(IMessage img) {
    player.coinAmount *= 2;
    if (player.coinAmount < 0) {
      player.coinAmount = Int32.MaxValue;
    }

    playerUi.GetTextCoin();
    SaveCoinAmount();
  }

  private void SaveCoinAmount() {
    PlayerPrefs.SetInt(Constants.PrefsKey_Coin, player.coinAmount);
  }

  private void LoadCoinAmount() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_Coin)) {
      player.coinAmount = PlayerPrefs.GetInt(Constants.PrefsKey_Coin);
    }
  }

  private void ResetData(IMessage img) {
    player.coinAmount = 500;
    SaveCoinAmount();
  }
}