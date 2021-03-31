using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGetRoom : WebRequestManager
{
    public override void FinishedResponse()
    {
        if (webResponse.Contains("room:"))
        {
            string[] data = webResponse.Replace("room: ", "").Split(',');
            StartCoroutine(LoadRoom(data));
        }
        else
        {
            Debug.LogError(webResponse);
        }
    }

    private IEnumerator LoadRoom(string[] data)
    {
        string roomID = data[0];

        GameObject newArea = Instantiate(new GameObject(), EditorManager.Instance().areaParent);
        newArea.transform.position = Vector3.zero;
        newArea.name = roomID;

        Texture photo = null;
        if (data.Length > 2)
        {
            if (data[2] != "")
            {
                Debug.LogError("Downloading Photo");
                yield return StartCoroutine(Utility.DownloadTexture(Utility.web_url + $"/images/{data[2]}", (response) =>
                {
                    Debug.LogError("Photo downloaded " + response);
                    photo = response;
                }));
            }
        }

        newArea.SetActive(false);
        EditorManager.Instance().AddRoom(roomID, photo, newArea, new List<ButtonSave>());
    }
}
