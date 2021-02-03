using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class WebRequestManager : MonoBehaviour
{
    /// <summary>
    /// Reference to the game manager script
    /// </summary>
    public GameManager gameManger => GameManager.Instance();
        
    [Header("Web responses")]
    //Kunnen misschien straks allemaal de tag [HideInInspector] krijgen
    public bool finishedResponse;
    public string webResponse;
    public string webError;
    void Update()
    {
        if (finishedResponse)
        {
            FinishedResponse();
            finishedResponse = false;
        }
    }

    public abstract void FinishedResponse();

    /// <summary>
    /// Handles getting a response from a page using a get request
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError) webError = webRequest.error;
            else webResponse = webRequest.downloadHandler.text;
        }

        finishedResponse = true;
    }

    /// <summary>
    /// Handles getting a response from a page using a post request
    /// </summary>
    /// <param name="url"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    public IEnumerator PostRequest(string url, WWWForm form)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError) webError = webRequest.error;
            else webResponse = webRequest.downloadHandler.text;
        }

        finishedResponse = true;
    }
}
