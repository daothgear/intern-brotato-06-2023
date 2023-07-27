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
    currentExp = 0;
    UpdateExpUI();
    MessageDispatcher.AddListener("addExp",AddExp);
  }

  private void Update() {
    UpdateExpUI();
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
}