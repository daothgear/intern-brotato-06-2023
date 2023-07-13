using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerLoader : MonoBehaviour
{
  [SerializeField] private PlayerLoader playerLoader;
  private CharacterLevelData characterLevelData;

  private void Awake()
  {
    LoadCharacterInfo(1);
  }

  public void LoadCharacterInfo( int currentLevel )
  {
    string characterLevelPath = Path.Combine(Application.streamingAssetsPath , "CharacterLevelData.json");
    if ( File.Exists(characterLevelPath) )
    {
      string characterLevelJson = File.ReadAllText(characterLevelPath);
      characterLevelData = JsonConvert.DeserializeObject<CharacterLevelData>(characterLevelJson);

      foreach ( var characterInfo in characterLevelData.characterInfo )
      {
        if ( characterInfo.characterID == currentLevel )
        {
          CharacterLevelData.CharacterInfo currentCharacterInfo = characterInfo;
          Debug.Log("Character level data loaded successfully.");
          GetComponent<PlayerMove>().speed = currentCharacterInfo.moveSpeed;
          GetComponent<PlayerHealth>().maxHealth = currentCharacterInfo.maxHP;
          GetComponent<PlayerExp>().characterLevel = currentCharacterInfo.characterID;
          GetComponent<PlayerExp>().maxExp = currentCharacterInfo.exp;
          break;
        }
      }

      if ( characterLevelData == null )
      {
        Debug.LogError("Character info not found for level: " + currentLevel);
      }
    }
    else
    {
      Debug.LogError("File not found: " + characterLevelPath);
    }
  }
}
