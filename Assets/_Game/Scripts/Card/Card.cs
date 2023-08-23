using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
  
  [SerializeField] private Text ButtonUplevel;
  [SerializeField] private Text ButtonNextWave;
  [SerializeField] private Text ButtonAddWeapon;
  [SerializeField] private Text ButtonDoubleCoin;

  private int addWeapon = 2;
  private int uplevel = 3;
  private int nextWave = 3;
  private int doubleCoin = 3;
  
  public void Awake() {
    ButtonAddWeapon.text = (addWeapon * 10).ToString();
    ButtonNextWave.text = (nextWave * 10).ToString();
    ButtonUplevel.text = (uplevel * 10).ToString();
    ButtonDoubleCoin.text = (doubleCoin * 10).ToString();
  }

  private int GetCost(int value) {
    return value * 10;
  }

  public void AddWeapon() {
    int cost = GetCost(addWeapon);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      addWeapon++;
      MessageDispatcher.SendMessage(Constants.Mess_addWeapon);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    ButtonAddWeapon.text = (addWeapon * 10).ToString();
  }

  public void AddLevel() {
    int cost = GetCost(uplevel);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      uplevel++;
      MessageDispatcher.SendMessage(Constants.Mess_plus1Level);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    ButtonUplevel.text = (uplevel * 10).ToString();
  }

  public void NextWave() {
    int cost = GetCost(nextWave);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      nextWave++;
      MessageDispatcher.SendMessage(Constants.Mess_nextwave);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    ButtonNextWave.text = (nextWave * 10).ToString();
  }

  public void DoubleMoney() {
    int cost = GetCost(doubleCoin);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      doubleCoin++;
      MessageDispatcher.SendMessage(Constants.Mess_doubleMoney);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    ButtonDoubleCoin.text = (doubleCoin * 10).ToString();
  }
}
