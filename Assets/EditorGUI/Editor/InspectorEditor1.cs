using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditorGUITestComp1))]
public class InspectorEditor1 : Editor {

    SerializedProperty intValue;


    private void OnEnable() {
        intValue=serializedObject.FindProperty("IntValue");
    }


    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.IntSlider(intValue, 0, 100, "slider");
        //EditorGUILayout.
        serializedObject.ApplyModifiedProperties();
    }
}
