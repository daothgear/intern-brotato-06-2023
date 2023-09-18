using com.ootii.Messages;
using UnityEngine;

public class PlayerExp : MonoBehaviour {
  private Player player;
  private PlayerUi playerUi;

  private void OnValidate() {
    if (player == null) {
      player = GetComponent<Player>();
    }

    if (playerUi == null) {
      playerUi = GetComponent<PlayerUi>();
    }
  }

  private void Awake() {
    LoadLevel();
    PlayerDataLoader.Ins.LoadCharacterInfo(player.playerLoader.characterLevel);
    player.maxLevel = PlayerDataLoader.Ins.GetLastPlayerID();
  }

  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_addExp, AddExp);
    MessageDispatcher.AddListener(Constants.Mess_plus1Level, LevelUp);
    MessageDispatcher.AddListener(Constants.Mess_playerDie, ResetLevel);
    playerUi.UpdateExpUI();
  }

  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_addExp, AddExp);
    MessageDispatcher.RemoveListener(Constants.Mess_plus1Level, LevelUp);
  }

  public void AddExp(IMessage img) {
    if (player.playerLoader.characterLevel == player.maxLevel) {
      player.playerLoader.LoadCharacterInfo(player.playerLoader.characterLevel);
    }
    else {
      player.currentExp += player.enemyLoader.enemyExp;
      while (player.currentExp >= player.playerLoader.maxExp) {
        player.playerLoader.characterLevel++;
        player.currentExp -= player.playerLoader.maxExp;
        player.playerLoader.LoadCharacterInfo(player.playerLoader.characterLevel);
      }
    }

    SaveLevel();
    playerUi.UpdateExpUI();
  }

  public void LevelUp(IMessage msg) {
    player.playerLoader.characterLevel++;
    player.playerLoader.LoadCharacterInfo(player.playerLoader.characterLevel);
    SaveLevel();
    playerUi.UpdateExpUI();
  }

  private void SaveLevel() {
    PlayerPrefs.SetInt(Constants.PrefsKey_PlayerExp, player.playerLoader.characterLevel);
  }

  private void LoadLevel() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_PlayerExp)) {
      player.playerLoader.characterLevel = PlayerPrefs.GetInt(Constants.PrefsKey_PlayerExp);
    }
  }

  private void ResetLevel(IMessage img) {
    PlayerDataLoader.Ins.LoadCharacterInfo(0);
    SaveLevel();
  }
}