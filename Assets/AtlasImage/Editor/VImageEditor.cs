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
using UnityEditor.U2D;

[CustomEditor(typeof(VImage), true)]
[CanEditMultipleObjects]
public class VImageEditor : ImageEditor {

    private SerializedProperty atlas;
    Object lastAtlas;
    private SerializedProperty spriteName;
    GUIContent guiContent = new GUIContent("11", "aaaa");

    string[] spriteAtlasList = null;
    string[] spriteAtlasDisplayList = null;
    int selectAtlasIdx = 0;

    protected override void OnEnable() {
        base.OnEnable();
        if (target == null) return;

        atlas = serializedObject.FindProperty("spriteAtlas");
        spriteName = serializedObject.FindProperty("spriteName");
        spriteAtlasList = AssetDatabase.FindAssets("t:" + typeof(SpriteAtlas).Name).Select(x => AssetDatabase.GUIDToAssetPath(x)).ToArray();
        spriteAtlasDisplayList = spriteAtlasList.Select(t => Path.GetFileNameWithoutExtension(t)).ToArray();
        lastAtlas = atlas.objectReferenceValue;
    }

    public override void OnInspectorGUI() {
        base.serializedObject.Update();
        EditorGUILayout.PropertyField(atlas);
        //using (new EditorGUILayout.HorizontalScope()) {｝
        using (new EditorGUILayout.HorizontalScope()) {
            EditorGUILayout.PrefixLabel("SpriteName");
            var name = string.IsNullOrEmpty(spriteName.stringValue) ? "-" : spriteName.stringValue;
            if (GUILayout.Button(name, "DropDown")) {
                SelectSpriteWindow.Open((atlas.objectReferenceValue as SpriteAtlas), OnSelectSprite, spriteName.stringValue);
            }
            if (GUILayout.Button("Editor")) {
                //UnityEditor.U2D.Sprites.SpriteEditorWindow.GetWindow()
                EditSprite();

            }
        }
        if (atlas.objectReferenceValue != lastAtlas) {
            OnAtlasChange();
        }

        //SpriteGUI();
        AppearanceControlsGUI();
        RaycastControlsGUI();
        MaskableControlsGUI();
        NativeSizeButtonGUI();


        base.serializedObject.ApplyModifiedProperties();

    }

    private void OnAtlasChange() {
        //((VImage)target).SetAllDirty(
    }

    private void OnSelectSprite(string name) {
        spriteName.stringValue = name;
        spriteName.serializedObject.ApplyModifiedProperties();
    }

    private void EditSprite() {
        if (atlas.objectReferenceValue != null) {
            var sp = (atlas.objectReferenceValue as SpriteAtlas).GetSprite(spriteName.stringValue);
            if (sp != null) {
                //var path = AssetDatabase.GetAssetPath(sp);
                //var pp = AssetDatabase.LoadAssetAtPath<Sprite>(path);


                //Selection.activeObject = sp;
            }
        }
        var asm = Assembly.GetAssembly(typeof(UnityEditor.U2D.Sprites.ISpriteEditor));
        var typ = asm.GetType("UnityEditor.U2D.Sprites.SpriteEditorWindow");
        if (typ != null) {
            typ.GetMethod("OpenSpriteEditorWindow", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null);
        }

        Debug.Log(Selection.activeObject.name);


    }
}
