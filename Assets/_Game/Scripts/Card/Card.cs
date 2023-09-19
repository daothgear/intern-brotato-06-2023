using System;
using com.ootii.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
  public int randomLevel;
  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Ins;
  }

  private CardUi cardUi;
  
  public int addWeapon = 2;
  public int uplevel = 3;
  public int nextWave = 3;
  public int doubleCoin = 3;

  private void OnValidate() {
    if (cardUi == null) {
      cardUi = GetComponent<CardUi>();
    }
  }

  public void Awake() {
    MessageDispatcher.AddListener(Constants.Mess_randomWeapon,RandomLevel);
  }

  public void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_randomWeapon,RandomLevel);
  }
  
  private int GetCost(int value) {
    return value * 10;
  }

  public void AddWeapon() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    int cost = GetCost(addWeapon);
    MessageDispatcher.SendMessage(Constants.Mess_UpdateTextCoin);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      if (ReferenceHolder.Ins.player.isBuydone == true) {
        addWeapon++;
        MessageDispatcher.SendMessage(Constants.Mess_addWeapon);
        ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
        cardUi.ButtonAddWeapon.text = (addWeapon * 10).ToString();
      }
      else {
        cardUi. ButtonAddWeapon.text = "Max";
      }
    }
  }

  public void AddLevel() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    if (playerLoader.characterLevel == ReferenceHolder.Ins.player.maxLevel) {
      cardUi.ButtonUplevel.text = "Max";
      return;
    }
    int cost = GetCost(uplevel);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      uplevel++;
      MessageDispatcher.SendMessage(Constants.Mess_plus1Level);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    cardUi.ButtonUplevel.text = (uplevel * 10).ToString();
  }

  public void NextWave() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    int cost = GetCost(nextWave);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      nextWave++;
      MessageDispatcher.SendMessage(Constants.Mess_nextwave);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    cardUi.ButtonNextWave.text = (nextWave * 10).ToString();
  }

  public void DoubleMoney() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    int cost = GetCost(doubleCoin);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      doubleCoin++;
      MessageDispatcher.SendMessage(Constants.Mess_doubleMoney);
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }

    cardUi.ButtonDoubleCoin.text = (doubleCoin * 10).ToString();
  }
  
  private void RandomLevel(IMessage img) {
    randomLevel = UnityEngine.Random.Range(1, 3);
    cardUi.textLevel.text = "Level: " + (randomLevel + 1);
    MessageDispatcher.SendMessage(gameObject,Constants.Mess_LevelWeapon,gameObject.GetComponent<Card>().randomLevel,0);
  }
}