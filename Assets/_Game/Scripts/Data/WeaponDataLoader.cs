using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class WeaponDataLoader : Singleton<WeaponDataLoader> {
  private WeaponLevelData weaponLevelData;
  public int currentWeaponLevel;
  public float firerate;
  public int weaponCost;
  public int weaponDamage;
  public float weaponAttackRange;
  public float weaponAttackSpeed;
  public int weaponPierce;
  public float weaponPierceDamageReduce;

  protected override void Awake() {
    base.Awake();
    LoadWeaponInfo(1);
  }

  public void LoadWeaponInfo(int currentWeaponID) {
    string weaponLevelPath = Path.Combine(Application.streamingAssetsPath , Constants.Data_Weapon);
    if (File.Exists(weaponLevelPath)) {
      string weaponLevelJson = File.ReadAllText(weaponLevelPath);
      weaponLevelData = JsonConvert.DeserializeObject<WeaponLevelData>(weaponLevelJson);
      foreach (var weaponInfo in weaponLevelData.weaponInfo) {
        if (weaponInfo.weaponID == currentWeaponID) {
          WeaponLevelData.WeaponInfo currentWeaponInfo = weaponInfo;
          Debug.Log("Weapon level data loaded successfully.");
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

      if (weaponLevelData == null) {
        Debug.LogError("Weapon info not found for ID: " + currentWeaponID);
      }
    } else {
      Debug.LogError("File not found: " + weaponLevelPath);
    }
  }
}
