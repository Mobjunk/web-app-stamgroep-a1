using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGetRoom : WebRequestManager
{
    public EditorLoader editorLoader;

    public override void FinishedResponse()
    {
        if (webResponse.Contains("room:"))
        {
            string[] data = webResponse.Replace("room: ", "").Split(',');
            string roomID = data[0];

            GameObject newArea = Instantiate(new GameObject(), editorLoader.areaParent);
            newArea.transform.position = Vector3.zero;
            newArea.name = roomID;

            EditorManager.Instance().AddRoom(roomID, null, new List<ButtonSave>());
        }
        else
        {
            Debug.LogError(webResponse);
        }
    }
}
