using UnityEngine;

public class UICombatTextManager : MonoBehaviour {
    [SerializeField] private UICombatText uicombatTextPrefabs;
    [SerializeField] private Transform canvasText;
    public void CreateUICombatText(Vector3 position, string text , Color color) {
        UICombatText uiCombatText = Instantiate(uicombatTextPrefabs, canvasText);
        uiCombatText.SetUp(position, text, color);
    }
}
