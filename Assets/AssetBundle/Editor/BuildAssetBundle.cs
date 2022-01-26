using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildNode {
    public string path;
    public string searchType;
}


public class BuildAssetBundle {



    public static void Build() {
        BuildNode node = new BuildNode() {
            path="",
            searchType="",
        };
    }



    public void GetAllFile(BuildNode buildNode) {
        string path = Path.Combine(Application.dataPath, buildNode.path);
        string[] files = Directory.GetFiles(path, buildNode.searchType);
    }



    private static void GetDependencies(string path) {
        //var obj = AssetDatabase.GetAssetPath(path);
        //var dep = EditorUtility.CollectDependencies(obj);
    }

}
