using UnityEngine;

public class Constants : MonoBehaviour {
  //Data
  public const string Data_Player = "PlayerData.json";
  public const string Data_Weapon = "WeaponData.json";
  public const string Data_Wave = "WaveData.json";
  public const string Data_Enemy = "EnemyData.json";
  //Tag
  public const string Tag_Player = "Player";
  public const string Tag_Enemy = "Enemy";
  public const string Tag_Bullets = "Bullets";
  public const string Tag_Coin = "Coin";
  public const string Tag_CombatText = "CombatText";
  public const string Tag_Pistol = "Pistol";
  //Anim-Enemy
  public const string Anim_Idle = "Idle";
  public const string Anim_Walk = "Walk";
  public const string Anim_Die = "Die";
  //Anim-Player
  public const string Anim_PlayerIdle = "PlayerIdle";
  public const string Anim_PlayerWalk = "PlayerWalk";
  //PrefKey
  public const string PrefsKey_Coin = "PlayerCoinAmount";
  public const string PrefsKey_PlayerExp = "PlayerExp";
  public const string PrefsKey_CollectedWeaponsCount = "CollectedWeaponsCount";
  public const string PrefsKey_CollectedWeaponsLevels = "CollectedWeaponsLevels";
  public const string PrefsKey_CurrentWave = "CurrentWave";
  public const string PrefsKey_CurrentSubWave = "CurrentSubWave";
  public const string PrefsKey_TotalTimer = "TotalTimer";
  public const string PrefsKey_ShopState = "ShopState";
  public const string PrefsKey_MusicVolume = "MusicVolume";
  public const string PrefsKey_SfxVolume = "SfxVolume";
  //scene
  public const string Scene_StartGame = "StartGame";
  public const string Scene_Menu = "Menu";
  public const string Scene_GamePlay = "GamePlay";
}