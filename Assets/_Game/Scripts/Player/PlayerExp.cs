using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour {


  [SerializeField] private int currentExp;
  [SerializeField] private Slider playerExpSlider;
  [SerializeField] private Text textExp;

  private PlayerLoader playerLoader;

  private void Start() {
    playerLoader = PlayerLoader.Instance;
    currentExp = 0;
    UpdateExpUI();
  }

  private void Update() {
    UpdateExpUI();
  }

  private void UpdateExpUI() {
    playerExpSlider.maxValue = playerLoader.maxExp;
    playerExpSlider.value = currentExp;
    textExp.text = "LV." + playerLoader.characterLevel;
  }

  public void AddExp(int expAmount) {
    currentExp += expAmount;
    while (currentExp >= playerLoader.maxExp) {
      playerLoader.characterLevel++;
      currentExp -= playerLoader.maxExp;
      playerLoader.LoadCharacterInfo(playerLoader.characterLevel);
    }

    UpdateExpUI();
  }
}