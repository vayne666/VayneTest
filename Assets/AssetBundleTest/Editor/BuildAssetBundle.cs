using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Build.Pipeline;
public class BuildNode {
    public string path;
    public string searchType;
}


public class BuildAssetBundle {



    [MenuItem("Bundle/Default Build")]
    public static void Build() {
        var path = new DirectoryInfo(Application.dataPath);
        string outPut = path.Parent.FullName + "\\Bundle";
        BuildPipeline.BuildAssetBundles(outPut, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
    }

    [MenuItem("Bundle/Sbp Build")]
    public static void SbpBuild() {
        Debug.Log("start build");
        var path = new DirectoryInfo(Application.dataPath);
        string outPut = path.Parent.FullName + "\\Bundle";
        CompatibilityBuildPipeline.BuildAssetBundles(outPut, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
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
