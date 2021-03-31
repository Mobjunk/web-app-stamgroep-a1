using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : Singleton<EditorManager>
{
    //            room id         value              
   public Dictionary<string, List<ButtonSave>> buttons = new Dictionary<string, List<ButtonSave>>();


   public void AddButton(GameObject gameobject, string room)
    {
        if (buttons.ContainsKey(room))
        {
            buttons[room].Add(new ButtonSave(gameobject, room, "", null, ""));
        }
        else
        {
            buttons.Add(room, new List<ButtonSave> { new ButtonSave(gameobject, room, "", null, "") });
        }
    }
}
