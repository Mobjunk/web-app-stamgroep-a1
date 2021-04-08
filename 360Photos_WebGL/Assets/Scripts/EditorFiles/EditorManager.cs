using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AreaChanger))]
[RequireComponent(typeof(PhotoChanger))]
public class EditorManager : Singleton<EditorManager>
{
    [HideInInspector] public AreaChanger areaChanger;
    [HideInInspector] public PhotoChanger photoChanger;
    public Transform areaParent;
    public GameObject imageSelector;
    public GameObject arrowPopUp;
    //            room id         value              
    [HideInInspector] public Dictionary<string, Room> rooms = new Dictionary<string, Room>();
    [HideInInspector] public string activeRoom;

    private int lastButtonID = 1;

    private void Awake()
    {
        areaChanger = GetComponent<AreaChanger>();
        photoChanger = GetComponent<PhotoChanger>();
    }

    public void AddRoom(string roomID, Texture photo, string photoName, GameObject area, List<ButtonSave> buttons)
    {
        rooms.Add(roomID, new Room(roomID, photo, photoName, area, buttons));
    }

    public ButtonSave AddButton(GameObject gameobject, string room)
    {
        ButtonSave buttonSave = new ButtonSave("0" + lastButtonID, gameobject, room, "", null, "", "");
        lastButtonID++;
        rooms[room].buttons.Add(buttonSave);
        return buttonSave;

    }

    public Room GetActiveRoom()
    {
        return rooms[activeRoom];
    }

    public void RemoveButton(GameObject gameobject, string room)
    {
        for (int i = 0; i < rooms[room].buttons.Count; i++)
        {
            if (rooms[room].buttons[i].gameobject == gameobject)
            {
                rooms[room].buttons.RemoveAt(i);
                return;
            }
        }
        
    }
}
