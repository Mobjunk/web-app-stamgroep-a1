using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AreaChanger))]
[RequireComponent(typeof(PhotoChanger))]
public class EditorManager : Singleton<EditorManager>
{
    [HideInInspector] public AreaChanger areaChanger;
    [HideInInspector] public PhotoChanger photoChanger;
    public Transform areaParent;
    public GameObject imageSelector;
    public GameObject arrowPopUp;
    public GameObject infoPopUp;
    //            room id         value              
    [HideInInspector] public Dictionary<string, Room> rooms = new Dictionary<string, Room>();
    [HideInInspector] public string activeRoom;

    private int lastButtonID = 1;

    private void Awake()
    {
        areaChanger = GetComponent<AreaChanger>();
        photoChanger = GetComponent<PhotoChanger>();
    }

    public void AddRoom(string roomID, Texture photo, string photoName, GameObject area, Dictionary<string, ButtonSave> buttons)
    {
        rooms.Add(roomID, new Room(roomID, photo, photoName, area, buttons));
    }

    public ButtonSave AddButton(GameObject gameobject, string room)
    {
        ButtonSave buttonSave = new ButtonSave("0" + lastButtonID, gameobject, room, "", null, "", "", "");
        lastButtonID++;
        rooms[room].buttons.Add(buttonSave.id ,buttonSave);
        return buttonSave;

    }

    public ButtonSave AddExitingButton(string id, GameObject gameobject, string room, string question, string[] options, string answer, string travelRoom, string infoText)
    {
        ButtonSave buttonSave = new ButtonSave(id, gameobject, room, question, options, answer, travelRoom, infoText);
        rooms[room].buttons.Add(buttonSave.id, buttonSave);
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
            if (rooms[room].buttons.Values.ElementAt<ButtonSave>(i).gameobject == gameobject)
            {
                rooms[room].buttons.Remove(rooms[room].buttons.Keys.ElementAt<string>(i));
                return;
            }
        }
        
    }
}
