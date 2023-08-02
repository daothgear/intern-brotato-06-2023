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
  [SerializeField] private Text ButtonDoubleCoin;

  private int addWeapon = 2;
  private int uplevel = 3;
  private int nextWave = 3;
  private int doubleCoin = 3;
  
  public void Start() {
    ButtonAddWeapon.text = (addWeapon * weaponDataLoader.weaponPierce).ToString() + "$";
    ButtonNextWave.text = (nextWave * weaponDataLoader.weaponPierce).ToString() + "$";
    ButtonUplevel.text = (uplevel * weaponDataLoader.weaponPierce).ToString() + "$";
    ButtonDoubleCoin.text = (doubleCoin * weaponDataLoader.weaponPierce).ToString() + "$";
  }

  private int GetCost(int value) {
    return value * weaponDataLoader.weaponPierce;
  }

  public void AddWeapon() {
    int cost = GetCost(addWeapon);
    if (PlayerCoin.Instance.HasEnoughCoins(cost)) {
      addWeapon++;
      MessageDispatcher.SendMessage(Constants.Mess_addWeapon);
      PlayerCoin.Instance.DeductCoins(cost);
    }
    ButtonAddWeapon.text = (addWeapon * weaponDataLoader.weaponPierce).ToString() + "$";
  }

  public void AddLevel() {
    int cost = GetCost(uplevel);
    if (PlayerCoin.Instance.HasEnoughCoins(cost)) {
      uplevel++;
      MessageDispatcher.SendMessage(Constants.Mess_plus1Level);
      PlayerCoin.Instance.DeductCoins(cost);
    }
    ButtonUplevel.text = (uplevel * weaponDataLoader.weaponPierce).ToString() + "$";
  }

  public void NextWave() {
    int cost = GetCost(nextWave);
    if (PlayerCoin.Instance.HasEnoughCoins(cost)) {
      nextWave++;
      MessageDispatcher.SendMessage(Constants.Mess_nextwave);
      PlayerCoin.Instance.DeductCoins(cost);
    }
    ButtonNextWave.text = (nextWave * weaponDataLoader.weaponPierce).ToString() + "$";
  }

  public void DoubleMoney() {
    int cost = GetCost(doubleCoin);
    if (PlayerCoin.Instance.HasEnoughCoins(cost)) {
      doubleCoin++;
      MessageDispatcher.SendMessage(Constants.Mess_doubleMoney);
      PlayerCoin.Instance.DeductCoins(cost);
    }
    ButtonDoubleCoin.text = (doubleCoin * weaponDataLoader.weaponPierce).ToString() + "$";
  }
}
