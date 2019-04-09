using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BundleLoader : MonoBehaviour
{
    public const byte salt = 89;
    
    private void Start()
    {
        LoadDecrypted("mybundle");

        var assetBundle = LoadDecrypted("scenebundle");
        string[] scenePath = assetBundle.GetAllScenePaths();
        SceneManager.LoadScene(scenePath[0]);
    }

    private AssetBundle LoadDecrypted(string name)
    {
        var bytes = File.ReadAllBytes(@"D:\Documents\YouTube\Cheating\Assets\AssetBundles\" + name);
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = (byte) (bytes[i] ^ salt);
        }

        var bundle = AssetBundle.LoadFromMemory(bytes);
        if(!bundle.isStreamedSceneAssetBundle) bundle.LoadAllAssets();
        return bundle;
    }
}
