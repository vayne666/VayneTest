using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using System.IO;
using System.Linq;
using UnityEngine.U2D;
using System.Reflection;
using UnityEditor.U2D;

[CustomEditor(typeof(VImage), true)]
[CanEditMultipleObjects]
public class VImageEditor : ImageEditor {

    private SerializedProperty atlas;
    Object lastAtlas;
    private SerializedProperty spriteName;
    private SerializedProperty spritePreserveAspect;

    private AnimBool animShowType;
    private SerializedProperty spriteType;
    protected override void OnEnable() {
        base.OnEnable();
        if (target == null) return;
        atlas = serializedObject.FindProperty("spriteAtlas");
        spriteName = serializedObject.FindProperty("spriteName");
        spriteType = serializedObject.FindProperty("m_Type");
        spritePreserveAspect = serializedObject.FindProperty("m_PreserveAspect");
        lastAtlas = atlas.objectReferenceValue;

        animShowType = new AnimBool(atlas.objectReferenceValue && !string.IsNullOrEmpty(spriteName.stringValue));
        animShowType.valueChanged.AddListener(new UnityAction(base.Repaint));
    }

    public override void OnInspectorGUI() {
        base.serializedObject.Update();
        EditorGUILayout.PropertyField(atlas);
        using (new EditorGUILayout.HorizontalScope()) {
            EditorGUILayout.PrefixLabel("SpriteName");
            var name = string.IsNullOrEmpty(spriteName.stringValue) ? "-" : spriteName.stringValue;
            if (GUILayout.Button(name, "DropDown")) {
                SelectSpriteWindow.Open((atlas.objectReferenceValue as SpriteAtlas), OnSelectSprite, spriteName.stringValue);
            }
            if (GUILayout.Button("Editor", GUILayout.Width(60))) {
                EditSprite();

            }
        }
        if (atlas.objectReferenceValue != lastAtlas) {
            OnAtlasChange();
        }

        AppearanceControlsGUI();
        RaycastControlsGUI();
        MaskableControlsGUI();

        animShowType.target = atlas.objectReferenceValue && !string.IsNullOrEmpty(spriteName.stringValue);

        if (EditorGUILayout.BeginFadeGroup(animShowType.faded))
            this.TypeGUI();
        EditorGUILayout.EndFadeGroup();
        var imageType = (Image.Type)spriteType.intValue;
        base.SetShowNativeSize(imageType == Image.Type.Simple || imageType == Image.Type.Filled, false);

        if (EditorGUILayout.BeginFadeGroup(m_ShowNativeSize.faded)) {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(spritePreserveAspect);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        
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
        if (atlas.objectReferenceValue != null && (!string.IsNullOrEmpty(spriteName.stringValue))) {
            var sp = (atlas.objectReferenceValue as SpriteAtlas).GetSprite(spriteName.stringValue);
            if (sp != null) {
                var packs = SpriteAtlasExtensions.GetPackables(atlas.objectReferenceValue as SpriteAtlas);
                foreach (var item in packs) {
                    var path = AssetDatabase.GetAssetPath(item);
                    UnityEngine.Object obj = null;
                    if (item.name == spriteName.stringValue) {
                        obj = item;
                    } else {
                        if (item.GetType() == typeof(DefaultAsset)) {
                            var p = AssetDatabase.GetAssetPath(item) + "/" + spriteName.stringValue + ".png";
                            obj = AssetDatabase.LoadAssetAtPath<Sprite>(p);
                        }
                    }

                    if (obj != null) {
                        Selection.activeObject = obj;
                        ShowSpriteEditorWindow();
                    }
                }
            }
        }



    }

    private void ShowSpriteEditorWindow() {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.U2D.Sprites.ISpriteEditor));
        var window = assembly.GetType("UnityEditor.U2D.Sprites.SpriteEditorWindow");
        if (window != null) {
            window.GetMethod("OpenSpriteEditorWindow", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null);
        }
    }
}
