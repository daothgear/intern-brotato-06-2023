using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class WeaponDataLoader : InstanceStatic<WeaponDataLoader> {
  private WeaponData weaponLevelData;

  public void ReceiveData(string fileName, string jsonData)
  {
    if (fileName == Constants.Data_Weapon)
    {
      weaponLevelData = JsonConvert.DeserializeObject<WeaponData>(jsonData);
    }
  }
  public WeaponData.WeaponInfo LoadWeaponInfo(int currentWeaponID, int currentLevel) {
    foreach (WeaponData.WeaponInfo weaponInfo in weaponLevelData.weaponInfo) {
      if (weaponInfo.weaponID == currentWeaponID && weaponInfo.currentlevel == currentLevel) {
        return weaponInfo;
      }
    }
    return null; 
  }
}