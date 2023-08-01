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

  public void AddWeapon1() {
    addWeapon++;
    MessageDispatcher.SendMessage("addweapon1");
  }

  public void AddLevel() {
    uplevel++;
    MessageDispatcher.SendMessage("plus1level");
  }

  public void NextWave() {
    nextWave++;
    MessageDispatcher.SendMessage("nextwave");
  }

  public void DoubleMoney() {
    x2Coin++;
    MessageDispatcher.SendMessage("doubleMoney");
  }
}
