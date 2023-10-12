using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorTool
{
  [MenuItem("Tool/Clear Prefs")]
  private static void RemovePlayerPrefs() {
    PlayerPrefs.DeleteAll();
  }
}
