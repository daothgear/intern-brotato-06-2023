using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICombatTextManager : Singleton<UICombatTextManager> {
    [SerializeField] private UICombatText uicombatTextPrefabs;
    [SerializeField] private Transform canvasText;
    public void CreateUICombatText(Vector3 position, string text , Color color) {
        UICombatText uiCombatText = Instantiate(uicombatTextPrefabs, canvasText);
        uiCombatText.SetUp(position, text, color);
    }
}
