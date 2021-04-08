using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSaveRoom : WebRequestManager
{
    public Dictionary<string, string> roomsID = new Dictionary<string, string>();

    public override void FinishedResponse()
    {
        if(webResponse.Contains("Succesfully"))
        {
            if (webResponse.Contains("updated"))
            {
                string[] data = webResponse.Replace("Succesfully updated room with id: ", "").Split(',');
                roomsID.Add(data[1], data[0]);
            }
            else if (webResponse.Contains("created"))
            {
                string[] data = webResponse.Replace("Succesfully created room with id: ", "").Split(',');
                roomsID.Add(data[1], data[0]);
                Room room = EditorManager.Instance().rooms[data[1]];
                room.roomID = data[0];
                room.area.name = data[0];
                EditorManager.Instance().rooms.Add(data[0], room);
                EditorManager.Instance().rooms.Remove(data[1]);
            }
        }
        else
        {
            Debug.LogError(webResponse);
        }
    }
}
