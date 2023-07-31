using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardType {
        addweaponlevel1,
        addweaponlevel2,
        addweaponlevel3,
        plus1level,
        plus1wave,
    }

    public CardType cardType;
    private TextMesh textMesh;

    private void OnValidate() {
        if (textMesh == null) {
            textMesh.GetComponentInChildren<TextMesh>();
        }
    }

    private void Update() {
        switch (cardType) {
            case CardType.addweaponlevel1:
                Debug.Log("level 1");
                AddWeapon1();
                break;
            case CardType.addweaponlevel2:
                Debug.Log("level 2");
                AddWeapon2();
                break;
            case CardType.addweaponlevel3:
                Debug.Log("level 3");
                AddWeapon3();
                break;
            case CardType.plus1wave:
                Debug.Log("level 4");
                Plus1Wave();
                break;
            case CardType.plus1level:
                Debug.Log("level 5");
                Plus1Level();
                break;
        }
    }

    public void AddWeapon1() {
       
    }

    public void AddWeapon2() {
        
    }

    public void AddWeapon3() {
        
    }

    public void Plus1Wave() {
        
    }

    public void Plus1Level() {
        
    }
}
