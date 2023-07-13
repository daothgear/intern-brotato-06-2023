using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  public int maxHealth;
  [SerializeField] private int currentHealth;
  [SerializeField] private Slider playerHealthSlider;
  [SerializeField] private Text textHealth;

  private void Start()
  {
    currentHealth = maxHealth;
    UpdateHealthUI();
  }

  private void Update()
  {
    UpdateHealthUI();
  }
  private void UpdateHealthUI()
  {
    playerHealthSlider.maxValue = maxHealth;
    playerHealthSlider.value = currentHealth;
    textHealth.text = currentHealth + "/" + maxHealth;
  }

  private void OnTriggerEnter2D( Collider2D collision )
  {
    if ( collision.gameObject.tag == "Wall" )
    {
      Debug.Log("Collision");
    }
  }
}
