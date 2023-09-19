using System.Collections.Generic;
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
  public Text textHealth;

  [Header("Player exp Logic")] 
  public int currentExp;
  public int maxLevel;

  [Header("Player exp Ui")] 
  public Slider playerExpSlider;
  public Text textExp;

  [Header("Player coin Logic")] 
  public int coinAmount;

  [Header("Player coinUi")] 
  public Text textCoin;

  [Header("Player weapons Logic")] 
  public List<Transform> weaponPositions = new List<Transform>();
  public List<WeaponPositionInfo> weaponPositionInfo = new List<WeaponPositionInfo>();
  public List<GameObject> collectedWeapons = new List<GameObject>();
  public GameObject weaponPrefab;
  public int nextAvailableWeaponIndex;
  public bool isBuydone = true;

  [Header("Player weapon UI")] 
  public Text[] weaponInfoTexts;
  public Text weaponInfoText;
  public Button[] weaponInfoButtons;
  
  [Header("Data")]
  public WeaponData.WeaponInfo weaponinfo;

  //Properties
  public PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Ins;
  }

  public EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Ins;
  }

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
    PlayerDataLoader.Ins.LoadCharacterInfo(playerLoader.characterLevel);
  }
}