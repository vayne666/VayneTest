using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

public class AtlasBuild {
    [MenuItem("Vayne/Ugui/BuildAtlas")]
    public static void Build() {
        string buildPath = Application.dataPath.Replace("Assets", "")+"AssetBundles";
        string path = "Assets/UGUI/Atlas/Atlas1.spriteatlas";
        bool isAll = false;
        var deps = AssetDatabase.GetDependencies(path).Where(t => t!=path&&t!=""&&Path.GetExtension(t)!="").ToArray();
        AssetBundleBuild[] builds = new AssetBundleBuild[1];
        AssetBundleBuild temp = new AssetBundleBuild();
        temp.assetBundleName="ugui";
        string[] files = null;
        if (isAll) {
            files=new string[deps.Length+1];
            for (int i = 0; i<deps.Length; i++) {
                files[i]=deps[i];
            }
            files[deps.Length]=path;
        } else {
            files=new string[] {
                path,
            };
        }
        temp.assetNames=files;
        builds[0]=temp;
        BuildPipeline.BuildAssetBundles(buildPath, builds, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows);
        //AssetDatabase.FindAssets("")
    }
}
