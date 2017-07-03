using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;



public class BuildAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {  
        string directory = Path.Combine(Application.streamingAssetsPath, "AssetBundles");

        if(!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        
        BuildPipeline.BuildAssetBundles(directory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }
}