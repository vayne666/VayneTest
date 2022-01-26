using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

public class MenuEditor {

    [MenuItem("Vayne/Asset/PrintDepend")]
    static void GetSelectDepend() {
        var objs = Selection.objects;
        if (objs!=null&&objs[0]!=null) {
            Debug.Log("========================START========================");
            var path = AssetDatabase.GetAssetPath(objs[0]);
            var deps = AssetDatabase.GetDependencies(path).Where(t => t!=path&&t!=""&&Path.GetExtension(t)!="");
            foreach (var item in deps) {
                Debug.Log(item);
            }
            Debug.Log("========================END========================");
        }


    }

    [MenuItem("Vayne/Asset/PrintDepend1")]
    static void GetSelectDepend1() {
        var objs = Selection.objects;
        if (objs!=null&&objs[0]!=null) {
            var path = AssetDatabase.GetAssetPath(objs[0]);
            //var deps = AssetDatabase.GetDependencies(path,false);
            var deps = EditorUtility.CollectDependencies(objs);
            //var t= deps.Distinct();
            var list = deps.Select(t => AssetDatabase.GetAssetPath(t)).Distinct().Where(t1 => t1!=path&&t1!="").ToList();
            //var list = deps.Select(t => AssetDatabase.GetAssetPath(t)).Distinct().ToList();
            Debug.Log("========================START========================");
            foreach (var item in list) {
                Debug.Log(item);
            }
            Debug.Log("========================END========================");
        }


    }

}
