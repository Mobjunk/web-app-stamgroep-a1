using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public static class Utility
{
    public const string web_url = "http://www.mediaenvormgeving.nl/stamgroepa1/";
    public const string action_url = "http://www.mediaenvormgeving.nl/stamgroepa1/index.php?action=";
    
    public static void AddSceneIfNotLoaded(string sceneName)
    {
        Scene playerScene = SceneManager.GetSceneByName(sceneName);
        if (!playerScene.IsValid())
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    public static void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public static void SwitchScenes(string oldSceneName, string newSceneName)
    {
        SceneManager.UnloadSceneAsync(oldSceneName);
        AddSceneIfNotLoaded(newSceneName);
    }
    
    public static IEnumerator DownloadSprite(string url, System.Action<Sprite> callback)
    {
        using(var www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var texture = DownloadHandlerTexture.GetContent(www);
                    var rect = new Rect(0, 0, texture.width, texture.height);
                    var sprite = Sprite.Create(texture,rect,new Vector2(texture.width / 2, texture.height / 2));
                    callback(sprite);
                }
            }
        }
    }

    public static IEnumerator DownloadTexture(string url, System.Action<Texture> callback)
    {
        using (var www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    callback(DownloadHandlerTexture.GetContent(www));
                }
            }
        }
    }
}
