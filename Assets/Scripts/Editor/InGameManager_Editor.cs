using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InGameManager))]
public class InGameManager_Editor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(GUILayout.Button("Remove PlayerPrefs - level")) {
            PlayerPrefs.DeleteKey("level");
        }
    }
}
