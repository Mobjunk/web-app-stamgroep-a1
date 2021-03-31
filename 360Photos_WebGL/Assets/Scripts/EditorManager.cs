using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : Singleton<EditorManager>
{
    //            room id         value              
   public Dictionary<string, Room> rooms = new Dictionary<string, Room>();

    
    public void AddRoom(string roomID, Texture photo, List<ButtonSave> buttons)
    {
        rooms.Add(roomID, new Room(photo, buttons));
    }

    public void AddButton(GameObject gameobject, string room)
    {
        rooms[room].buttons.Add(new ButtonSave(gameobject, room, "", null, ""));
    }
}
