using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChanger : MonoBehaviour
{
    public void SetArea(string roomID)
    {
        EditorManager.Instance().rooms[roomID].area.SetActive(true);
    }
}
