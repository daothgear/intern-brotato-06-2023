using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin : MonoBehaviour {
  [SerializeField] private int coinAmount;
  [SerializeField] private Text textCoin;

  private void Start() {
    coinAmount = 0;
  }

  private void Update() {
    textCoin.text = coinAmount.ToString();
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.tag == Constants.Tag_Coin) {
      coinAmount++;
      ObjectPool.Instance.ReturnToPool(Constants.Tag_Coin, collision.gameObject);
      Debug.Log("Coin return done");
    }
  }
}