using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EditorGetButton))]
public class EditorGetRoom : WebRequestManager
{
    EditorGetButton editorGetButton;

    private void Start()
    {
        editorGetButton = GetComponent<EditorGetButton>();
    }

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

        GameObject newArea = new GameObject();
        newArea.transform.parent = EditorManager.Instance().areaParent;
        newArea.transform.position = Vector3.zero;
        newArea.name = roomID;

        Texture photo = null;
        string photoName = "";
        if (data.Length > 2)
        {
            if (data[2] != "")
            {
                if (GameManager.Instance().TextureIsCached(data[2]))
                {
                    photo = GameManager.Instance().GetCachedTexture(data[2]);
                    photoName = data[2];
                }
                else
                {
                    yield return StartCoroutine(Utility.DownloadTexture(Utility.web_url + $"/images/{data[2]}", (response) =>
                    {
                        photo = response;
                        photoName = data[2];
                        GameManager.Instance().AddCachedTexture(data[2], response);
                    }));
                }
            }
        }

        newArea.SetActive(false);
        EditorManager.Instance().AddRoom(roomID, photo, photoName, newArea, new Dictionary<string, ButtonSave>());

        foreach (var buttonID in data[3].Split(':'))
        {
            WWWForm form = new WWWForm();
            form.AddField("id", buttonID);
            form.AddField("roomID", roomID);
            StartCoroutine(editorGetButton.PostRequest($"{Utility.action_url}get360Button", form));
        }
    }
}
