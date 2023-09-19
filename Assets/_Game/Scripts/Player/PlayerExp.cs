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
    playerUi.UpdateExpUI();
  }
  
  public void AddExp(int exp) {
    if (player.playerLoader.characterLevel == player.maxLevel) {
      player.playerLoader.LoadCharacterInfo(player.playerLoader.characterLevel);
    }
    else {
      player.currentExp += exp;
      while (player.currentExp >= player.playerLoader.maxExp) {
        player.playerLoader.characterLevel++;
        player.currentExp -= player.playerLoader.maxExp;
        player.playerLoader.LoadCharacterInfo(player.playerLoader.characterLevel);
      }
    }

    SaveLevel();
    playerUi.UpdateExpUI();
  }

  public void LevelUp() {
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

  public void ResetLevel() {
    PlayerDataLoader.Ins.LoadCharacterInfo(0);
    SaveLevel();
  }
}