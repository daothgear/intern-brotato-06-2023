using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] private int currentHealth;
  [SerializeField] private Slider playerHealthSlider;
  [SerializeField] private Text textHealth;
  private PlayerLoader playerLoader;

  private void Start()
  {
    playerLoader = GetComponent<PlayerLoader>();
    currentHealth = playerLoader.maxHealth;
    UpdateHealthUI();
  }

  private void Update()
  {
    UpdateHealthUI();
  }
  private void UpdateHealthUI()
  {
    playerHealthSlider.maxValue = playerLoader.maxHealth;
    playerHealthSlider.value = currentHealth;
    textHealth.text = currentHealth + "/" + playerLoader.maxHealth;
  }

  private void OnTriggerEnter2D( Collider2D collision )
  {
    if ( collision.gameObject.tag == "Wall" )
    {
      Debug.Log("Collision");
    }
  }
}
