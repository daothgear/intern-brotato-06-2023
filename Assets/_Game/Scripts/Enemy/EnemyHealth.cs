using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

  public float maxHealth;
  [SerializeField] private float currentHealth;

  [SerializeField] private bool drop;
  [SerializeField] private GameObject theDrop;

  void Start()
  {
    currentHealth = maxHealth;
    Debug.Log("max hp:" + maxHealth);
  }

  public void TakeDamage(float dame)
  {
    currentHealth -= dame;
    if (currentHealth <= 0)
    {
      makeDead();
    }
  }

  public void makeDead()
  {
    gameObject.SetActive(false);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      makeDead();
      if (drop)
      {
        Instantiate(theDrop, transform.position, Quaternion.identity);
      }
    }
  }
}