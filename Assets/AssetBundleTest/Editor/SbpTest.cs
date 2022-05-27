using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Pipeline;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEditor.Build.Reporting;
using System;
public class SbpTest {
    public static void Build() {

        //var t = CompatibilityBuildPipeline.BuildAssetBundles("", BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        //ContentBuildInterface
        //IBundleBuildContent


    }

    public static void GetDepend() {

        var obj = Selection.objects[0];
        if (obj) {
            var path = AssetDatabase.GetAssetPath(obj);
            GUID guid = new GUID(AssetDatabase.AssetPathToGUID(path));
            //ContentBuildInterface.GetPlayerDependenciesForObject()
            var includedObjects = ContentBuildInterface.GetPlayerObjectIdentifiersInAsset(guid, BuildTarget.StandaloneWindows);
            foreach (var item in includedObjects) {
                Debug.LogError(AssetDatabase.GUIDToAssetPath(item.guid.ToString()));
            }
        }


        //assetResult.assetInfo.includedObjects = new List<ObjectIdentifier>(includedObjects);
        //var referencedObjects = ContentBuildInterface.GetPlayerDependenciesForObjects(includedObjects, input.Target, input.TypeDB);
        //assetResult.assetInfo.referencedObjects = new List<ObjectIdentifier>(referencedObjects);
        //var allObjects = new List<ObjectIdentifier>(includedObjects);


        //ContentBuildInterface.GetPlayerObjectIdentifiersInSerializedFile(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.objects[0])), EditorUserBuildSettings.activeBuildTarget);

        //ContentBuildInterface.GetPlayerDependenciesForObject()
    }
}
