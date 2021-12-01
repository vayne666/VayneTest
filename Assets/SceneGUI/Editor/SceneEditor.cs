using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneEditor : Editor {
    static GenericMenu menu;

    [InitializeOnLoadMethod]
    static void Init() {
        menu=new GenericMenu();
        menu.AddItem(new GUIContent("Create/Img"), false, OnMenuClick, "img");
        menu.AddItem(new GUIContent("Create/Button"), false, OnMenuClick, "btn");
        menu.AddItem(new GUIContent("Create/Text"), false, OnMenuClick, "txt");
        SceneView.duringSceneGui+=OnSceneGUI;
    }

    public void OnEnable() {

    }

    static void OnSceneGUI(SceneView view) {
        if (Event.current.type==EventType.MouseDown&&Event.current.button==1) {
            if (Selection.objects.Length<1) return;
            var obj = Selection.objects[0] as GameObject;
            var ui = obj.GetComponent<UIBehaviour>();
            if (ui!=null) {
                menu.ShowAsContext();
            }
        }
    }

    public static void OnMenuClick(object userData) {
        //Create(userData as string);
        string name = userData as string;
        var patent = Selection.objects[0] as GameObject;
        switch (name) {
            case "img":
                GameObject go = new GameObject("imgxx", typeof(Image));
                go.layer=patent.layer;
                go.transform.SetParent(patent.transform, false);
                //go.transform.parent=patent.transform;
                //go.transform.localPosition=Vector3.zero;
                //go.transform.localScale=Vector3.one;
                break;

        }

    }

    private static void Create(string name) {
        GameObject gameObject = new GameObject(name);
    }
}
