using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour {
  public int characterLevel;
  public int maxExp;
  [SerializeField] private int currentExp;
  [SerializeField] private Slider playerExpSlider;
  [SerializeField] private Text textExp;

  private PlayerLoader playerLoader;

  private void Start() {
    playerLoader = FindObjectOfType<PlayerLoader>();
    currentExp = 0;
    UpdateExpUI();
  }

  private void Update() {
    UpdateExpUI();
  }

  private void UpdateExpUI() {
    playerExpSlider.maxValue = maxExp;
    playerExpSlider.value = currentExp;
    textExp.text = "LV." + characterLevel;
  }

  public void AddExp(int expAmount) {
    currentExp += expAmount;
    while (currentExp >= maxExp) {
      characterLevel++;
      currentExp -= maxExp;
      playerLoader.LoadCharacterInfo(characterLevel);
    }

    UpdateExpUI();
  }
}