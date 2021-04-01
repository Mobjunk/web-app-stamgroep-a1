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
                if (GameManager.Instance().TextureIsCached(data[2])) photo = GameManager.Instance().GetCachedTexture(data[2]);
                else
                {
                    yield return StartCoroutine(Utility.DownloadTexture(Utility.web_url + $"/images/{data[2]}", (response) =>
                    {
                        photo = response;
                        GameManager.Instance().AddCachedTexture(data[2], response);
                    }));
                }
            }
        }

        newArea.SetActive(false);
        EditorManager.Instance().AddRoom(roomID, photo, newArea, new List<ButtonSave>());
    }
}
