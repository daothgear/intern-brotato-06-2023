using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class WeaponDataLoader : InstanceStatic<WeaponDataLoader> {
  private WeaponData weaponLevelData;
  public int currentWeaponID;
  public int currentWeaponLevel;
  public float firerate;
  public int weaponCost;
  public int weaponDamage;
  public float weaponAttackRange;
  public float weaponAttackSpeed;
  public int weaponPierce;
  public float weaponPierceDamageReduce;
  
  public void ReceiveData(string fileName, string jsonData)
  {
    if (fileName == Constants.Data_Weapon)
    {
      weaponLevelData = JsonConvert.DeserializeObject<WeaponData>(jsonData);
    }
  }
  public void LoadWeaponInfo(int currentWeaponID , int currentLevel) {
    foreach (var weaponInfo in weaponLevelData.weaponInfo) {
      if (weaponInfo.weaponID == currentWeaponID && weaponInfo.currentlevel == currentLevel) {
        WeaponData.WeaponInfo currentWeaponInfo = weaponInfo;
        this.currentWeaponID = currentWeaponInfo.weaponID;
        currentWeaponLevel = currentWeaponInfo.currentlevel;
        firerate = currentWeaponInfo.firerate;
        weaponCost = currentWeaponInfo.cost;
        weaponDamage = currentWeaponInfo.damage;
        weaponAttackRange = currentWeaponInfo.attackRange;
        weaponAttackSpeed = currentWeaponInfo.attackSpeed;
        weaponPierce = currentWeaponInfo.pierce;
        weaponPierceDamageReduce = currentWeaponInfo.pierceDamageReduce;
        break;
      }
    } 
  }

    public int GetWeaponDamage(int weaponID , int level) {
      foreach (var weaponInfo in weaponLevelData.weaponInfo) {
        if (weaponInfo.weaponID == weaponID && weaponInfo.currentlevel == level) {
          return weaponInfo.damage;
        }
      }
      return 0;
    }

    public int GetWeaponLevel(int weaponID , int level) {
      foreach (var weaponInfo in weaponLevelData.weaponInfo) {
        if (weaponInfo.weaponID == weaponID && weaponInfo.currentlevel == level) {
          return weaponInfo.currentlevel;
        }
      }
      return 0;
    }
 }