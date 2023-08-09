using System.Collections;
using System.Collections.Generic;
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
  //Mess
  public const string Mess_playerTakeDamage = "playerTakeDamage";
  public const string Mess_addExp = "addExp";
  public const string Mess_resetHealth = "resetHealth";
  public const string Mess_playerDie = "PlayerDie";
  public const string Mess_playerFlipRight = "Right";
  public const string Mess_playerFlipLeft = "Left";
  public const string Mess_addWeapon = "addweapon1";
  public const string Mess_plus1Level = "plus1level";
  public const string Mess_nextwave = "nextwave";
  public const string Mess_doubleMoney = "doubleMoney";
}