using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponDataLoader : Singleton<WeaponDataLoader> {
  private Dictionary<int , WeaponLevelData.WeaponInfo> weaponDataDict = new Dictionary<int , WeaponLevelData.WeaponInfo>();

  protected override void Awake() {
    base.Awake();
    LoadWeaponData();
  }

  private void LoadWeaponData() {
    string weaponDataPath = Path.Combine(Application.streamingAssetsPath , Constants.Data_Weapon);
    if (File.Exists(weaponDataPath)) {
      string weaponDataJson = File.ReadAllText(weaponDataPath);
      WeaponLevelData weaponData = JsonConvert.DeserializeObject<WeaponLevelData>(weaponDataJson);
      Debug.Log(weaponDataJson);
      foreach (var weaponInfo in weaponData.weaponInfo) {
        if (!weaponDataDict.ContainsKey(weaponInfo.weaponID)) {
          weaponDataDict.Add(weaponInfo.weaponID , weaponInfo);
        }
      }
    } else {
      Debug.LogError("File not found: " + weaponDataPath);
    }
  }

  public bool TryGetWeaponInfo(int weaponID , out WeaponLevelData.WeaponInfo weaponInfo) {
    return weaponDataDict.TryGetValue(weaponID , out weaponInfo);
  }
}