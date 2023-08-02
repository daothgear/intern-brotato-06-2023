using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour {
  [SerializeField] private int currentExp;
  [SerializeField] private Slider playerExpSlider;
  [SerializeField] private Text textExp;

  private PlayerDataLoader playerLoader {
    get => PlayerDataLoader.Instance;
  }

  private EnemyDataLoader enemyLoader {
    get => EnemyDataLoader.Instance;
  }

  private void Start() {
    MessageDispatcher.AddListener(Constants.Mess_addExp, AddExp);
    MessageDispatcher.AddListener(Constants.Mess_plus1Level, LevelUp); 
    textExp.text = "LV." + playerLoader.characterLevel;
  }

  private void OnDestroy() {
    MessageDispatcher.RemoveListener(Constants.Mess_addExp, AddExp);
    MessageDispatcher.AddListener(Constants.Mess_plus1Level, LevelUp); 
  }

  
  private void UpdateExpUI() {
    playerExpSlider.maxValue = playerLoader.maxExp;
    playerExpSlider.value = currentExp;
    textExp.text = "LV." + playerLoader.characterLevel;
  }

  public void AddExp(IMessage img) {
    currentExp += enemyLoader.enemyExp;
    while (currentExp >= playerLoader.maxExp) {
      playerLoader.characterLevel++;
      currentExp -= playerLoader.maxExp;
      playerLoader.LoadCharacterInfo(playerLoader.characterLevel);
    }
    UpdateExpUI();
  }

  public void LevelUp(IMessage msg) {
    playerLoader.characterLevel++;
    playerLoader.LoadCharacterInfo(playerLoader.characterLevel);
    UpdateExpUI();
  }
}
