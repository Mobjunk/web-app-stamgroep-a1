using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAreaScript : MonoBehaviour
{
    private int lastRoomID = 1;

    public void CreateArea()
    {
        GameObject newArea = new GameObject();
        newArea.transform.parent = EditorManager.Instance().areaParent;
        newArea.transform.position = Vector3.zero;
        string roomID = "0" + lastRoomID;
        lastRoomID++;
        newArea.name = roomID;

        Texture photo = null;
        string photoName = "";

        newArea.SetActive(false);
        EditorManager.Instance().AddRoom(roomID, photo, photoName, newArea, new List<ButtonSave>());
        EditorManager.Instance().areaChanger.SetArea(roomID);
        EditorManager.Instance().imageSelector.SetActive(true);
    }
}
