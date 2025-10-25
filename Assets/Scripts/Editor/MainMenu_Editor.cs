using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MainMenuController))]
public class MainMenu_Editor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(GUILayout.Button("Remove PlayerPrefs")) {
            PlayerPrefs.DeleteAll();
        }
    }
}
