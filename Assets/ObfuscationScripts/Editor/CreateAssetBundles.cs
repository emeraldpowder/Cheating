using System.IO;
using UnityEditor;
using UnityEngine;
using File = UnityEngine.Windows.File;

public class CreateAssetBundles : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);

        // Encrypt them:
        foreach (string file in Directory.GetFiles(assetBundleDirectory))
        {
            if (file.EndsWith(".meta") || file.EndsWith(".manifest")) continue;

            var bytes = File.ReadAllBytes(file);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte) (bytes[i] ^ BundleLoader.salt);
            }

            File.WriteAllBytes(file, bytes);
        }
    }
}