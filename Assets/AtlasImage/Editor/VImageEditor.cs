using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using System.IO;
using System.Linq;
using Coffee.UIExtensions;
using UnityEngine.U2D;
using System.Reflection;

[CustomEditor(typeof(VImage), true)]
[CanEditMultipleObjects]
public class VImageEditor : ImageEditor {

    private SerializedProperty spriteAtlas;
    private SerializedProperty spriteName;
    GUIContent guiContent = new GUIContent("11");

    string[] spriteAtlasList = null;
    string[] spriteAtlasDisplayList = null;
    int selectAtlasIdx = 0;

    protected override void OnEnable() {
        base.OnEnable();
        if (target == null) return;

        spriteAtlas = serializedObject.FindProperty("spriteAtlas");
        spriteName = serializedObject.FindProperty("spriteName");
        spriteAtlasList = AssetDatabase.FindAssets("t:" + typeof(SpriteAtlas).Name).Select(x => AssetDatabase.GUIDToAssetPath(x)).ToArray();
        spriteAtlasDisplayList = spriteAtlasList.Select(t => Path.GetFileNameWithoutExtension(t)).ToArray();
    }

    public override void OnInspectorGUI() {
        base.serializedObject.Update();


        var b = spriteAtlas == null;
        if (GUILayout.Button("select")) {

        }
        //GUILayout.Label("SpriteAtlas");
        var rect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.popup);
        EditorGUILayout.BeginHorizontal();
        //selectAtlasIdx = EditorGUILayout.Popup("Atlas", selectAtlasIdx, spriteAtlasDisplayList);
        selectAtlasIdx = EditorGUILayout.Popup("",selectAtlasIdx, spriteAtlasDisplayList);
        EditorGUILayout.PropertyField(spriteAtlas,new GUIContent(""));
        EditorGUILayout.EndHorizontal();
        selectAtlasIdx = EditorGUILayout.Popup("Sprite", selectAtlasIdx, spriteAtlasDisplayList);

        //SpriteGUI();
        //GUILayout.Label(spriteName.ToString());

        AppearanceControlsGUI();
        RaycastControlsGUI();
        MaskableControlsGUI();
        NativeSizeButtonGUI();


        base.serializedObject.ApplyModifiedProperties();

    }
}
