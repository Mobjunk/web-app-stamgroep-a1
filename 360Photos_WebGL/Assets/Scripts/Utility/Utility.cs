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
}
