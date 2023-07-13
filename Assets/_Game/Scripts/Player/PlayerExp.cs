using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
  public int characterLevel;
  public int maxExp;
  [SerializeField] private int currentExp;
  [SerializeField] private Slider playerExpSlider;
  [SerializeField] private Text textExp;

  private PlayerLoader playerLoader;

  private void Start()
  {
    playerLoader = GetComponent<PlayerLoader>();
    currentExp = maxExp;
    UpdateExpUI();
  }

  private void Update()
  {
    UpdateExpUI();
  }
  private void UpdateExpUI()
  {
    playerExpSlider.maxValue = maxExp;
    playerExpSlider.value = currentExp;
    textExp.text = "LV." + characterLevel;
  }

  private void OnTriggerEnter2D( Collider2D collision )
  {
    if ( collision.gameObject.tag == "UpLevel" )
    {
      playerLoader.LoadCharacterInfo(characterLevel + 1);
      GetComponent<Animator>().SetTrigger("UpLevel");
      Debug.Log("Trigger");
    }
  }
}
