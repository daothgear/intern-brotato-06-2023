using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

  private WeaponDataLoader weaponDataLoader {
    get => WeaponDataLoader.Instance;
  }

  [SerializeField] private Text ButtonUplevel;
  [SerializeField] private Text ButtonNextWave;
  [SerializeField] private Text ButtonAddWeapon;
  [SerializeField] private Text ButtonX2Coin;

  [SerializeField] private int addWeapon;
  [SerializeField] private int uplevel;
  [SerializeField] private int nextWave;
  [SerializeField] private int x2Coin;

  private void Start() {
    addWeapon = 1;
    uplevel = 1;
    nextWave = 1;
    x2Coin = 1;
  }

  public void Update() {
    ButtonAddWeapon.text = (addWeapon * weaponDataLoader.weaponPierce).ToString() + "$";
    ButtonNextWave.text = (nextWave * weaponDataLoader.weaponPierce).ToString() + "$";
    ButtonUplevel.text = (uplevel * weaponDataLoader.weaponPierce).ToString() + "$";
    ButtonX2Coin.text = (x2Coin * weaponDataLoader.weaponPierce).ToString() + "$";
  }

  private int GetCost(int value) {
    return value * weaponDataLoader.weaponPierce;
  }

  public void AddWeapon1() {
    int cost = GetCost(addWeapon);
    if (PlayerCoin.Instance.HasEnoughCoins(cost)) {
      addWeapon++;
      MessageDispatcher.SendMessage("addweapon1");
      PlayerCoin.Instance.DeductCoins(cost);
    }
  }

  public void AddLevel() {
    int cost = GetCost(uplevel);
    if (PlayerCoin.Instance.HasEnoughCoins(cost)) {
      uplevel++;
      MessageDispatcher.SendMessage("plus1level");
      PlayerCoin.Instance.DeductCoins(cost);
    }
  }

  public void NextWave() {
    int cost = GetCost(nextWave);
    if (PlayerCoin.Instance.HasEnoughCoins(cost)) {
      nextWave++;
      MessageDispatcher.SendMessage("nextwave");
      PlayerCoin.Instance.DeductCoins(cost);
    }
  }

  public void DoubleMoney() {
    int cost = GetCost(x2Coin);
    if (PlayerCoin.Instance.HasEnoughCoins(cost)) {
      x2Coin++;
      MessageDispatcher.SendMessage("doubleMoney");
      PlayerCoin.Instance.DeductCoins(cost);
    }
  }
}
