using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.IO;

public class PlayerMove : MonoBehaviour
{
  [SerializeField] private Animator animator;
  [SerializeField] private Joystick joystick;
  [SerializeField] private GameObject[] weapons;

  [SerializeField] private float speed;
  //
  [SerializeField] private int characterlevel;
  //
  [SerializeField] private int maxHealth;
  [SerializeField] private int currentHealth;
  [SerializeField] private Slider playerHealthSlider;
  [SerializeField] private Text textHealth;


  //
  [SerializeField] private int maxExp;
  [SerializeField] private int currentExp;
  [SerializeField] private Slider playerExpSlider;
  [SerializeField] private Text textExp;

  [SerializeField] private CharacterLevelData characterLevelData;

  private CharacterLevelData.CharacterInfo currentCharacterInfo;
  private bool isFacingRight = true;

  private void Awake()
  {
    LoadCharacterInfo(1);
  }

  private void Start()
  {
    animator = GetComponent<Animator>();
    currentHealth = maxHealth;
    currentExp = maxExp;
  }

  private void Update()
  {
    Move();
    Health();
    Exp();
  }

  private void Move()
  {
    Vector3 movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0) * Time.deltaTime * speed;
    transform.position += movement;

    if (movement.magnitude > 0)
    {
      animator.SetTrigger("Walk");
    }
    else
    {
      animator.SetTrigger("Idle");
    }

    if (ShouldFlip())
    {
      Flip();
    }
  }

  private bool ShouldFlip()
  {
    if (joystick.Horizontal < 0 && isFacingRight)
    {
      return true;
    }
    else if (joystick.Horizontal > 0 && !isFacingRight)
    {
      return true;
    }

    return false;
  }

  private void Flip()
  {
    isFacingRight = !isFacingRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
  }

  private void LoadCharacterInfo(int currentLevel)
  {
    string characterLevelPath = Path.Combine(Application.streamingAssetsPath, "CharacterLevelData.json");
    if (File.Exists(characterLevelPath))
    {
      string characterLevelJson = File.ReadAllText(characterLevelPath);
      characterLevelData = JsonConvert.DeserializeObject<CharacterLevelData>(characterLevelJson);

      foreach (var characterInfo in characterLevelData.characterInfo)
      {
        if (characterInfo.characterID == currentLevel)
        {
          currentCharacterInfo = characterInfo;
          Debug.Log("Character level data loaded successfully.");
          characterlevel = currentCharacterInfo.characterID;
          speed = currentCharacterInfo.moveSpeed;
          maxHealth = currentCharacterInfo.maxHP;
          maxExp = currentCharacterInfo.exp;
          break;
        }
      }

      if (currentCharacterInfo == null)
      {
        Debug.LogError("Character info not found for level: " + currentLevel);
      }
    }
    else
    {
      Debug.LogError("File not found: " + characterLevelPath);
    }
  }

  private void Health()
  {
    playerHealthSlider.maxValue = maxHealth;
    playerHealthSlider.value = currentHealth;
    textHealth.text = currentHealth + "/" + maxHealth;
  }
  private void Exp()
  {
    playerExpSlider.maxValue = maxExp;
    playerExpSlider.value = currentExp;
    textExp.text = "LV." + characterlevel;
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "UpLevel")
    {
      LoadCharacterInfo(currentCharacterInfo.characterID + 1);
      animator.SetTrigger("UpLevel");
      Debug.Log("Trigger");
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Wall")
    {
      Debug.Log("Collision");
    }
  }
}
