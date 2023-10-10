using UnityEngine;

public class PlayerExp : MonoBehaviour {
  [SerializeField] private Player player;
  [SerializeField] private PlayerUi playerUi;

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
    player.playerInfo = PlayerDataLoader.Ins.LoadCharacterInfo(player.characterLevel);
    player.maxLevel = PlayerDataLoader.Ins.GetLastPlayerID();
  }

  private void Start() {
    player.characterLevel = player.playerInfo.characterID;
    player.maxExp = player.playerInfo.exp;
    playerUi.UpdateExpUI();
  }

  public void AddExp(int exp) {
    if (player.characterLevel == player.maxLevel) {
      player.playerInfo = player.playerLoader.LoadCharacterInfo(player.characterLevel);
      player.UpdateData();
    } else {
      player.currentExp += exp;
      while (player.currentExp >= player.maxExp) {
        player.characterLevel++;
        player.maxExp = player.playerInfo.exp;
        player.currentExp -= player.maxExp;
        player.playerInfo = player.playerLoader.LoadCharacterInfo(player.characterLevel);
        player.UpdateData();
      }
    }

    SaveLevel(player.characterLevel);
    playerUi.UpdateExpUI();
    playerUi.UpdateHealthUI();
  }

  public void LevelUp() {
    player.characterLevel++;
    player.playerInfo = player.playerLoader.LoadCharacterInfo(player.characterLevel);
    player.UpdateData();
    SaveLevel(player.characterLevel);
    playerUi.UpdateExpUI();
  }

  public void SaveLevel(int level) {
    player.characterLevel = level;
    PlayerPrefs.SetInt(Constants.PrefsKey_PlayerExp, player.characterLevel);
  }

  private void LoadLevel() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_PlayerExp)) {
      player.characterLevel = PlayerPrefs.GetInt(Constants.PrefsKey_PlayerExp);
    }
  }

  public void ResetLevel() {
    player.characterLevel = 0;
    player.playerInfo = PlayerDataLoader.Ins.LoadCharacterInfo(player.characterLevel);
    player.UpdateData();
    SaveLevel(player.characterLevel);
  }

}