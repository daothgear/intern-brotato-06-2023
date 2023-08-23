using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour {
    public void DropdownItem(int index) {
        switch (index) {
            case 0:
                Debug.Log("1");
                break;
            case 1:
                Debug.Log("2");
                break;
            case 2:
                Debug.Log("3");
                break;
            case 3:
                Debug.Log("4");
                break;
        }
    }
}
