using System;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour {
  [SerializeField] private int currentExp;
  [SerializeField] private Slider playerExpSlider;
  [SerializeField] private Text textExp;
  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Ins;
  }

  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Ins;
  }

  private void Awake() {
    LoadLevel();
    PlayerDataLoader.Ins.LoadCharacterInfo(playerLoader.characterLevel);
  }

  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_addExp, AddExp);
    MessageDispatcher.AddListener(Constants.Mess_plus1Level, LevelUp); 
    MessageDispatcher.AddListener(Constants.Mess_playerDie, ResetLevel);
    UpdateExpUI();
  }

  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_addExp, AddExp);
    MessageDispatcher.RemoveListener(Constants.Mess_plus1Level, LevelUp); 
  }

  
  private void UpdateExpUI() {
    playerExpSlider.maxValue = playerLoader.maxExp;
    playerExpSlider.value = currentExp;
    textExp.text = "LV." + (playerLoader.characterLevel + 1);
  }

  public void AddExp(IMessage img) {
    currentExp += enemyLoader.enemyExp;
    while (currentExp >= playerLoader.maxExp) {
      playerLoader.characterLevel++;
      currentExp -= playerLoader.maxExp;
      playerLoader.LoadCharacterInfo(playerLoader.characterLevel);
      SaveLevel();
    }
    UpdateExpUI();
  }

  public void LevelUp(IMessage msg) {
    playerLoader.characterLevel++;
    playerLoader.LoadCharacterInfo(playerLoader.characterLevel);
    UpdateExpUI();
  }
  
  private void SaveLevel() {
    PlayerPrefs.SetInt(Constants.PrefsKey_PlayerExp, playerLoader.characterLevel);
  }

  private void LoadLevel() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_PlayerExp)) {
      playerLoader.characterLevel = PlayerPrefs.GetInt(Constants.PrefsKey_PlayerExp);
    }
  }

  private void ResetLevel(IMessage img) {
    PlayerDataLoader.Ins.LoadCharacterInfo(0);
    SaveLevel();
  }
}
