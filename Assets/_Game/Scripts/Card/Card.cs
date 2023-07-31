using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
  public void AddWeapon1() {
    MessageDispatcher.SendMessage("addweapon1");
  }

  public void AddLevel() {
    MessageDispatcher.SendMessage("plus1level");
  }

  public void NextWave() {
    MessageDispatcher.SendMessage("nextwave");
  }

  public void DoubleMoney() {
    MessageDispatcher.SendMessage("doubleMoney");
  }
}
