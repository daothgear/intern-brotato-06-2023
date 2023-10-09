using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
  [Header("Player move")] 
  public Animator animator;
  public Joystick joystick;
  public Rigidbody2D rb;
  public bool isFacingRight = true;

  [Header("Player health Logic")] 
  public int currentHealth;
  public bool die;
  public PlayerExp playerExp;

  [Header("Player health Ui")] 
  public Slider playerHealthSlider;
  public TMP_Text textHealth;

  [Header("Player exp Logic")] 
  public int currentExp;
  public int maxLevel;

  [Header("Player exp Ui")] 
  public Slider playerExpSlider;
  public TMP_Text textExp;

  [Header("Player coin Logic")] 
  public int coinAmount;

  [Header("Player coinUi")] 
  public TMP_Text textCoin;

  [Header("Player weapons Logic")] 
  public List<Transform> weaponPositions = new List<Transform>();
  public List<WeaponPositionInfo> weaponPositionInfo = new List<WeaponPositionInfo>();
  public List<GameObject> collectedWeapons = new List<GameObject>();
  public GameObject weaponPrefab;
  public int nextAvailableWeaponIndex;

  [Header("Player weapon UI")] 
  public TMP_Text[] weaponInfoTexts;
  public TMP_Text weaponInfoText;
  public Button[] weaponInfoButtons;
  
  [Header("Data")]
  public WeaponData.WeaponInfo weaponinfo;
  public PlayerData.PlayerInfo playerInfo;
  //Properties
  public PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Ins;
  }

  public EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Ins;
  }
  [Header("current Data")]
  public int currentLevel;

  [Header("Player Data")]
  public int characterLevel;
  public float speed;
  public int maxHealth;
  public int maxExp;
  private void OnValidate() {
    if (animator == null) {
      animator = GetComponent<Animator>();
    }

    if (rb == null) {
      rb = GetComponent<Rigidbody2D>();
    }

    if (playerExp == null) {
      playerExp = GetComponent<PlayerExp>();
    }
  }

  private void Awake() {
    playerInfo = PlayerDataLoader.Ins.LoadCharacterInfo(characterLevel);
  }

  public void UpdateData() {
    maxExp = playerInfo.exp;
    maxHealth = playerInfo.maxHP;
    speed = playerInfo.moveSpeed;
  }
}