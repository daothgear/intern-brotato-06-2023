using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Joystick joystick;

    [SerializeField] private float speed;

    [SerializeField] private CharacterLevelData characterLevelData; 
    [SerializeField] private CharacterLevelData.CharacterInfo currentCharacterInfo;

    private void Start()
    {
        animator = GetComponent<Animator>();
        LoadCharacterInfo(1);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += new Vector3(joystick.Horizontal, joystick.Vertical, 0) * Time.deltaTime * speed;
        if (joystick.Horizontal!= 0 || joystick.Vertical!= 0)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
    
    private void LoadCharacterInfo(int currentLevel)
    {
        // Load JSON data from file
        string characterLevelPath = Path.Combine(Application.streamingAssetsPath, "CharacterLevelData.json");
        if (File.Exists(characterLevelPath))
        {
            string characterLevelJson = File.ReadAllText(characterLevelPath);
            characterLevelData = JsonConvert.DeserializeObject<CharacterLevelData>(characterLevelJson);

            // Find the CharacterInfo for the current level
            foreach (var characterInfo in characterLevelData.characterInfo)
            {
                if (characterInfo.characterID == currentLevel)
                {
                    currentCharacterInfo = characterInfo;
                    Debug.Log("Character level data loaded successfully.");

                    // Set the speed based on moveSpeed from JSON
                    speed = currentCharacterInfo.moveSpeed;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UpLevel")
        {
            LoadCharacterInfo(currentCharacterInfo.characterID + 1);
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
