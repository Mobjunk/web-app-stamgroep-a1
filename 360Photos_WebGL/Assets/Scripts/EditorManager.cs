using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : Singleton<EditorManager>
{
   public Dictionary<int, List<ButtonSave>> buttons = new Dictionary<int, List<ButtonSave>>();

   public void AddButton(GameObject gameobject, int room)
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
