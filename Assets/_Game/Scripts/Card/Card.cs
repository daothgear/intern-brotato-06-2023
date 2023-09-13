using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
  
  [SerializeField] private Text ButtonUplevel;
  [SerializeField] private Text ButtonNextWave;
  [SerializeField] private Text ButtonAddWeapon;
  [SerializeField] private Text ButtonDoubleCoin;
  [SerializeField] private TextMeshProUGUI textLevel;
  public int randomLevel;

  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Ins;
  }
  
  private int addWeapon = 2;
  private int uplevel = 3;
  private int nextWave = 3;
  private int doubleCoin = 3;
  
  public void Awake() {
    MessageDispatcher.AddListener(Constants.Mess_randomWeapon,RandomLevel);
  }

  public void Start() {
    textLevel.text = "Level: " + (randomLevel + 1);
    ButtonNextWave.text = (nextWave * 10).ToString();
    ButtonUplevel.text = (uplevel * 10).ToString();
    ButtonDoubleCoin.text = (doubleCoin * 10).ToString();
    ButtonAddWeapon.text = (addWeapon * 10).ToString();
    }

  private int GetCost(int value) {
    return value * 10;
  }

  public void AddWeapon() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    int cost = GetCost(addWeapon);
    MessageDispatcher.SendMessage(Constants.Mess_UpdateTextCoin);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      if (ReferenceHolder.Ins.playerWeapon.isBuydone == true) {
        addWeapon++;
        MessageDispatcher.SendMessage(Constants.Mess_addWeapon);
        ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
        ButtonAddWeapon.text = (addWeapon * 10).ToString();
      }
      else {
        ButtonAddWeapon.text = "Max";
      }
    }
  }

  public void AddLevel() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    if (playerLoader.characterLevel == ReferenceHolder.Ins.playerExp.maxLevel) {
      ButtonUplevel.text = "Max";
      return;
    }
    int cost = GetCost(uplevel);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      uplevel++;
      MessageDispatcher.SendMessage(Constants.Mess_plus1Level);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    ButtonUplevel.text = (uplevel * 10).ToString();
  }

  public void NextWave() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    int cost = GetCost(nextWave);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      nextWave++;
      MessageDispatcher.SendMessage(Constants.Mess_nextwave);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    ButtonNextWave.text = (nextWave * 10).ToString();
  }

  public void DoubleMoney() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    int cost = GetCost(doubleCoin);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      doubleCoin++;
      MessageDispatcher.SendMessage(Constants.Mess_doubleMoney);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }

    ButtonDoubleCoin.text = (doubleCoin * 10).ToString();
  }
  
  private void RandomLevel(IMessage img) {
    randomLevel = UnityEngine.Random.Range(1, 3);
    textLevel.text = "Level: " + (randomLevel + 1);
    MessageDispatcher.SendMessage(Constants.Mess_LevelWeapon,randomLevel);
  }
}