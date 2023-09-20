using UnityEngine;

public class PlayerUi : MonoBehaviour {
  private Player player;

  private void OnValidate() {
    if (player == null) {
      player = GetComponent<Player>();
    }
  }

  public void UpdateExpUI() {
    if (player.playerLoader.characterLevel == player.maxLevel) {
      player.textExp.text = "LV. Max";
    }
    else {
      player.playerExpSlider.maxValue = player.playerLoader.maxExp;
      player.playerExpSlider.value = player.currentExp;
      player.textExp.text = "LV." + (player.playerLoader.characterLevel + 1);
    }
  }

  public void UpdateHealthUI() {
    player.playerHealthSlider.maxValue = player.playerLoader.maxHealth;
    player.playerHealthSlider.value = player.currentHealth;
    player.textHealth.text = player.currentHealth + "/" + player.playerLoader.maxHealth;
  }

  public void GetTextCoin() {
    player.textCoin.text = player.coinAmount.ToString();
  }

  public void UpdateWeaponInfoTexts(int position) {
    if (position < player.collectedWeapons.Count) {
      Weapon weaponComponent = player.collectedWeapons[position].GetComponent<Weapon>();
      WeaponDataLoader weaponDataLoader = WeaponDataLoader.Ins;
      player.weaponinfo =
          weaponDataLoader.LoadWeaponInfo(weaponComponent.currentWeaponId, weaponComponent.currentWeaponLevel);
      player.weaponInfoText.text = "Position: " + position +
                                   "\nID: " + weaponComponent.currentWeaponId +
                                   "\nLevel: " + (weaponComponent.currentWeaponLevel + 1) +
                                   "\nDamage: " + player.weaponinfo.damage +
                                   "\nRange: " + player.weaponinfo.attackRange +
                                   "\nFirerate: " + player.weaponinfo.firerate +
                                   "\nSpeed: " + player.weaponinfo.attackSpeed;
    }
    else {
      player.weaponInfoText.text = "You don't have any weapons in this position";
    }
  }

  public void UpdateButtonWeapon() {
    for (int i = 0; i < player.weaponInfoButtons.Length; i++) {
      if (i < player.collectedWeapons.Count) {
        player.weaponInfoButtons[i].gameObject.SetActive(true);
      }
      else {
        player.weaponInfoButtons[i].gameObject.SetActive(false);
      }
    }
  }

  public void UpdateWeaponLevelTexts() {
    for (int i = 0; i < player.weaponInfoTexts.Length; i++) {
      if (i < player.collectedWeapons.Count) {
        Weapon weaponComponent = player.collectedWeapons[i].GetComponent<Weapon>();
        player.weaponInfoTexts[i].text = "Weapon Level: " + (weaponComponent.currentWeaponLevel + 1);
      }
      else {
        player.weaponInfoTexts[i].text = "";
      }
    }
  }
}