using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;

public class UINextLevel : MonoBehaviour {
  private TimeManager timeManager {
    get => TimeManager.Instance;
  }
  
  public void ClickNextButton() {
    timeManager.CloseShopUI();
  }
}
