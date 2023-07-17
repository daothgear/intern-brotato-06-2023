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

  private Animator animator;
  void Start()
  {
    animator = FindAnyObjectByType<Animator>();
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
    animator.SetBool("Die", true);
    Destroy(gameObject, 1.5f);
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