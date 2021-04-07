using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChanger : MonoBehaviour
{
    public void SetArea(string roomID)
    {
        if (EditorManager.Instance().activeRoom != "") EditorManager.Instance().rooms[EditorManager.Instance().activeRoom].area.SetActive(false);
        EditorManager.Instance().rooms[roomID].area.SetActive(true);
        EditorManager.Instance().photoChanger.SetPhoto(roomID);
        EditorManager.Instance().activeRoom = roomID;
    }

}
