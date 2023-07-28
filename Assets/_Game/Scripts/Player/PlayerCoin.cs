using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin : MonoBehaviour {
  [SerializeField] private int coinAmount;
  [SerializeField] private Text textCoin;
  
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Coin")) {
      coinAmount++;
      textCoin.text = coinAmount.ToString();
      ObjectPool.Instance.ReturnToPool(Constants.Tag_Coin, collision.gameObject);
      Debug.Log("Coin return done");
    }
  }
}