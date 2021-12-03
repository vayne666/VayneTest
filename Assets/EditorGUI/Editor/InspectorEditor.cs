using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EditorGUITestComp))]
public class InspectorEditor : Editor {

    EditorGUITestComp comp;

    private void OnEnable() {
        comp=target as EditorGUITestComp;
    }


    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        GUILayout.Label("哈哈哈哈哈哈哈哈哈哈");
        if (GUILayout.Button("Random progress")) {
            comp.Progress=Random.Range(0, 10);
        }
    }
}
