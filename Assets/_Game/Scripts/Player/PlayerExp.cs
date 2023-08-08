using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour {
  [SerializeField] private int currentExp;
  [SerializeField] private Slider playerExpSlider;
  [SerializeField] private Text textExp;
  private const string PlayerExpPrefsKey = "PlayerExp";
  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Ins;
  }

  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Ins;
  }

  private void Start() {
    LoadLevel();
    PlayerDataLoader.Ins.LoadCharacterInfo(playerLoader.characterLevel);
    MessageDispatcher.AddListener(Constants.Mess_addExp, AddExp);
    MessageDispatcher.AddListener(Constants.Mess_plus1Level, LevelUp); 
    MessageDispatcher.AddListener(Constants.Mess_playerDie, ResetLevel);
    textExp.text = "LV." + playerLoader.characterLevel;
  }

  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_addExp, AddExp);
    MessageDispatcher.AddListener(Constants.Mess_plus1Level, LevelUp); 
  }

  
  private void UpdateExpUI() {
    playerExpSlider.maxValue = playerLoader.maxExp;
    playerExpSlider.value = currentExp;
    textExp.text = "LV." + playerLoader.characterLevel;
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
    PlayerPrefs.SetInt(PlayerExpPrefsKey, playerLoader.characterLevel);
  }

  private void LoadLevel() {
    if (PlayerPrefs.HasKey(PlayerExpPrefsKey)) {
      playerLoader.characterLevel = PlayerPrefs.GetInt(PlayerExpPrefsKey);
    }
  }

  private void ResetLevel(IMessage img) {
    PlayerDataLoader.Ins.LoadCharacterInfo(1);
    SaveLevel();
  }
}
