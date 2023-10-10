using System;
using UnityEngine;

public class PlayerCoin : MonoBehaviour {
  [SerializeField] private Player player;
  [SerializeField] private PlayerUi playerUi;

  private void OnValidate() {
    if (player == null) {
      player = GetComponent<Player>();
    }

    if (playerUi == null) {
      playerUi = GetComponent<PlayerUi>();
    }
  }

  private void Start() {
    LoadCoinAmount();
    player.lastCoin = player.coinAmount;
    playerUi.GetTextCoin();
  }
  
  public bool HasEnoughCoins(int amount) {
    return player.coinAmount >= amount;
  }

  public void DeductCoins(int amount) {
    player.coinAmount -= amount;
    player.lastCoin = player.coinAmount;
    playerUi.GetTextCoin();
    SaveCoinAmount(player.coinAmount);
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag(Constants.Tag_Coin)) {
      player.coinAmount++;
      player.lastCoin = player.coinAmount;
      playerUi.GetTextCoin();
      ObjectPool.Ins.ReturnToPool(Constants.Tag_Coin, collision.gameObject);
      SaveCoinAmount(player.coinAmount);
    }
  }

  public void DoubleCurrentCoin() {
    player.coinAmount *= 2;
    player.lastCoin = player.coinAmount;
    if (player.coinAmount < 0) {
      player.coinAmount = Int32.MaxValue;
    }

    playerUi.GetTextCoin();
    SaveCoinAmount(player.coinAmount);
  }

  public void SaveCoinAmount(int coin) {
    player.coinAmount = coin;
    PlayerPrefs.SetInt(Constants.PrefsKey_Coin, player.coinAmount);
  }

  private void LoadCoinAmount() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_Coin)) {
      player.coinAmount = PlayerPrefs.GetInt(Constants.PrefsKey_Coin);
    }
  }

  public void ResetData() {
    player.coinAmount = 500;
    player.lastCoin = player.coinAmount;
    SaveCoinAmount(player.coinAmount);
  }
}