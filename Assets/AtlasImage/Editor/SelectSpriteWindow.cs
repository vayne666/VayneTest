using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D;
using System;
using System.Linq;

public class SelectSpriteWindow : ScriptableWizard {

    static Action<string> onPick;
    static SpriteAtlas spriteAtlas;

    float size = 80f;
    float padded = 10f;
    float fontPadded = 40;
    static string selectSprite = string.Empty;

    public static void Open(SpriteAtlas Atlas, Action<string> pickCallBack, string select = "") {

        onPick = pickCallBack;
        spriteAtlas = Atlas;
        selectSprite = select;
        DisplayWizard<SelectSpriteWindow>("Select a Sprite");
    }



    private void OnGUI() {
        if (spriteAtlas == null) return;
        SerializedProperty spPackedSprites = new SerializedObject(spriteAtlas).FindProperty("m_PackedSprites");
        Sprite[] sprites = Enumerable.Range(0, spPackedSprites.arraySize)
            .Select(index => spPackedSprites.GetArrayElementAtIndex(index).objectReferenceValue).OfType<Sprite>().ToArray();

        int screenWidth = (int)EditorGUIUtility.currentViewWidth;
        int columns = Mathf.FloorToInt(screenWidth / (size + padded));
        int row = Mathf.CeilToInt(sprites.Length / (float)columns);
        var max = sprites.Length;
        Rect rect = new Rect(10f, 0, size, size);
        GUILayout.Space(20);
        GUILayout.BeginVertical();
        GUILayout.BeginScrollView(Vector3.zero);
        for (int i = 0; i < row; i++) {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < columns; j++) {
                var idx = (i * columns) + j;
                if (idx >= max) break;
                var sprite = sprites[idx];
                var tex = sprite.texture;
                rect.x = j * size + (j + 1) * padded;
                rect.y = i * size + (i + 1) * padded + i * fontPadded;
                if (GUI.Button(rect, tex)) {
                    selectSprite = sprite.name;
                    onPick.Invoke(selectSprite);
                }
                var fontRect = new Rect(rect.x, rect.y + rect.height, rect.width, 40f);
                var style = selectSprite == sprite.name ? "MeTransitionSelectHead" : "ProgressBarBack";
                GUI.Label(new Rect(rect.x, rect.y + rect.height, rect.width, 40f), sprite.name, style);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    private void OnDestroy() {
        onPick = null;
        spriteAtlas = null;
        selectSprite = string.Empty;
    }



}
