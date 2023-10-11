using UnityEngine;

public class PlayerHealth : MonoBehaviour {
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

  private void Start() {
    ReferenceHolder.Ins.uicontroller.UiEndGame.SetActive(player.die);
    player.UpdateData();
    player.currentHealth = player.maxHealth;
    playerUi.UpdateHealthUI();
  }
  
  public void TakeDamage(int damage) {
    if (player.currentHealth > 0) {
      player.currentHealth -= damage;
      if (player.currentHealth <= 0) {
        player.currentHealth = 0;
        ReferenceHolder.Ins.timeManager.Stoptime();
        Die();
      }

      playerUi.UpdateHealthUI();
    }
  }

  private void Die() {
    player.die = true;

    player.lastWeapon.Clear();
    foreach (GameObject weaponObject in player.collectedWeapons) {
      Weapon weaponComponent = weaponObject.GetComponent<Weapon>();
      int weaponID = weaponComponent.currentWeaponId;
      int weaponLevel = weaponComponent.currentWeaponLevel;
      WeaponData.WeaponInfo weaponInfo = WeaponDataLoader.Ins.LoadWeaponInfo(weaponID, weaponLevel);
      if (weaponInfo != null) {
        player.lastWeapon.Add(weaponInfo);
      }
    }

    //level player
    ReferenceHolder.Ins.playerExp.SaveLevel(0);
    //save time
    ReferenceHolder.Ins.timeManager.SavePlayerPrefsData(1);
    //coin
    ReferenceHolder.Ins.playerCoin.SaveCoinAmount(500);
    //weapon
    player.lastnextAvailableWeaponIndex = player.nextAvailableWeaponIndex;
    ReferenceHolder.Ins.playerWeapon.ResetWeapon();
    //save data
    ReferenceHolder.Ins.playerWeapon.SaveCollectedWeapons();
    ReferenceHolder.Ins.uicontroller.UiEndGame.SetActive(player.die);
    ReferenceHolder.Ins.uicontroller.ShowEndGame();
  }

  public void ResetHealth(int maxHealth) {
    player.currentHealth = maxHealth;
    playerUi.UpdateHealthUI();
  }
}