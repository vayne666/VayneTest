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
        menu.AddItem(new GUIContent("Sibling/+1"), false, ChangeSibling, "Add");
        menu.AddItem(new GUIContent("Sibling/-1"), false, ChangeSibling, "");
        menu.AddItem(new GUIContent("Sibling/Last"), false, ChangeSibling, "Last");
        menu.AddItem(new GUIContent("合并"), false, Merge);
        menu.AddItem(new GUIContent("ResetPos"), false, ResetPos);
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
                Undo.RegisterCreatedObjectUndo(go, "go");
                Selection.activeObject=go;

                break;

        }

    }

    private static void Create(string name) {
        GameObject gameObject = new GameObject(name);
    }

    private static void Merge() {
        if (Selection.objects.Length>0) {
            var objs = Selection.objects;
            var frist = objs[0] as GameObject;

            Undo.IncrementCurrentGroup();

            var parent = new GameObject("objs");
            parent.transform.SetParent(frist.transform.parent);
            parent.layer=frist.transform.parent.gameObject.layer;
            parent.AddComponent<RectTransform>();
            parent.transform.localPosition=Vector3.zero;
            Undo.RegisterCreatedObjectUndo(parent, "Merge Go");
            foreach (var item in objs) {
                var go = item as GameObject;
                Undo.SetTransformParent(go.transform, parent.transform, "Merge Trs");
                //go.transform.SetParent(parent.transform);
                //go.transform.SetSiblingIndex()

            }

        }

    }

    private static void ChangeSibling(object userData) {
        string str = userData as string;
        var go = Selection.objects[0] as GameObject;
        var curIdx = go.transform.GetSiblingIndex();
        if (str=="Add") {
            go.transform.SetSiblingIndex(curIdx+1);
        } else if (str=="Last") {
            go.transform.SetAsLastSibling();
        } else {
            go.transform.SetSiblingIndex(curIdx-1);
        }
    }

    private static void ResetPos() {
        var obj = Selection.objects[0] as GameObject;
        //Undo.RecordObject(obj.transform, "resetpos");

        //Undo.IncrementCurrentGroup();   // 强制分组

        Undo.RegisterCompleteObjectUndo(obj.transform, "resetpos");
        obj.transform.localPosition=Vector3.zero;
        Undo.RegisterCompleteObjectUndo(obj, "reName");
        obj.name="new name";
    }
}
