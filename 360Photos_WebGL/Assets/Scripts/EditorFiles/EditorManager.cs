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
    //            room id         value              
    [HideInInspector] public Dictionary<string, Room> rooms = new Dictionary<string, Room>();
    [HideInInspector] public string activeRoom;

    private void Awake()
    {
        areaChanger = GetComponent<AreaChanger>();
        photoChanger = GetComponent<PhotoChanger>();
    }

    public void AddRoom(string roomID, Texture photo, GameObject area, List<ButtonSave> buttons)
    {
        rooms.Add(roomID, new Room(roomID, photo, area, buttons));
    }

    public void AddButton(GameObject gameobject, string room)
    {
        rooms[room].buttons.Add(new ButtonSave(gameobject, room, "", null, ""));
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
