using UnityEngine;

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
  
  private int GetCost(int value) {
    return value * 10;
  }

  public void AddWeapon() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    int cost = GetCost(addWeapon);
    bool canMerge;
    canMerge = ReferenceHolder.Ins.playerWeapon.CheckMerge(randomLevel);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      if (canMerge) {
        addWeapon++;
        ReferenceHolder.Ins.playerWeapon.AddWeapon();
        ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
        cardUi.ButtonAddWeapon.text = (addWeapon * 10).ToString();
      }
      else {
        cardUi.ButtonAddWeapon.text = "Can't Merge";
      }
    } else {
      cardUi.ButtonAddWeapon.text = "Don't have enough money";
    }
  }

  public void AddLevel() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    if (ReferenceHolder.Ins.player.characterLevel == ReferenceHolder.Ins.player.maxLevel) {
      cardUi.ButtonUplevel.text = "Max";
      return;
    }
    int cost = GetCost(uplevel);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      uplevel++;
      ReferenceHolder.Ins.playerExp.LevelUp();
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    cardUi.ButtonUplevel.text = (uplevel * 10).ToString();
  }

  public void NextWave() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    int cost = GetCost(nextWave);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      nextWave++;
      ReferenceHolder.Ins.timeManager.UpWave();
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }
    cardUi.ButtonNextWave.text = (nextWave * 10).ToString();
  }

  public void DoubleMoney() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    int cost = GetCost(doubleCoin);
    if (ReferenceHolder.Ins.playerCoin.HasEnoughCoins(cost)) {
      doubleCoin++;
      ReferenceHolder.Ins.playerCoin.DoubleCurrentCoin();
      ReferenceHolder.Ins.playerCoin.DeductCoins(cost);
    }

    cardUi.ButtonDoubleCoin.text = (doubleCoin * 10).ToString();
  }
  
  public void RandomLevel() {
    randomLevel = Random.Range(1, 3);
    cardUi.textLevel.text = "Level: " + (randomLevel + 1);
  }
  
  public void ClickNextButton() {
    ReferenceHolder.Ins.timeManager.CloseShopUI();
  }
}