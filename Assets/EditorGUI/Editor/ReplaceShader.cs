using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ReplaceShader : EditorWindow {

    [MenuItem("Vayne/Window/ReplaceShader")]
    public static void OpenWindow() {
        var win = GetWindow<ReplaceShader>();
        win.Show();

        win.position=new Rect(Screen.width/2, Screen.height/2, 500, 300);
    }


    public string path;
    public Shader sourceShader;
    public Shader targetShader;

    

    private void OnGUI() {


        

        //source=GUILayout.TextField(source);
        //GUILayout.Label("target:");
        //target=GUILayout.TextField(target);
        GUILayout.BeginHorizontal();
        GUILayout.Label("path:");
        path=GUILayout.TextField(path);
        GUILayout.EndHorizontal();
        sourceShader=(Shader)EditorGUILayout.ObjectField("sourceShader", sourceShader, typeof(Shader), false);
        targetShader=(Shader)EditorGUILayout.ObjectField("targetShader", targetShader, typeof(Shader), false);

        if (GUILayout.Button("Replace")) {
            if (sourceShader!=null&&targetShader!=null) {
                Replace(path);
            }
        }
    }

    private void Replace(string path) {
        if (string.IsNullOrEmpty(path)) {
            path="Assets";
        }
        var assets = AssetDatabase.FindAssets("t:Material", new string[] { path });
        foreach (var item in assets) {
            var filePath = AssetDatabase.GUIDToAssetPath(item);
            var mat = AssetDatabase.LoadAssetAtPath<Material>(filePath);
            //Debug.Log($"shader name :{mat.shader.name}");
            if (mat.shader.name==sourceShader.name) {
                Debug.LogError(mat.shader.name);
                Debug.Log($"Replace:{filePath}");
                //mat.shader.name=targetShader.name;
                mat.shader=targetShader;
                Debug.LogError(mat.shader.name);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


}
