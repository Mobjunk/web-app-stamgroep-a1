using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSaveButton : WebRequestManager
{
    public Dictionary<string, string> buttonsID = new Dictionary<string, string>();
    public string currentRoom;
    public List<ButtonSave> travelbuttons = new List<ButtonSave>();

    public override void FinishedResponse()
    {
        if (webResponse.Contains("Succesfully"))
        {
            if (webResponse.Contains("updated"))
            {
                string[] data = webResponse.Replace("Succesfully updated button with id: ", "").Split(',');
                buttonsID.Add(data[1], data[0]);
                if (data[2] != "" && data[2] != "0" && data[2].StartsWith("0")) travelbuttons.Add(EditorManager.Instance().rooms[currentRoom].buttons[data[0]]);
            }
            else if (webResponse.Contains("created"))
            {
                string[] data = webResponse.Replace("Succesfully created button with id: ", "").Split(',');
                buttonsID.Add(data[1], data[0]);
                ButtonSave button = EditorManager.Instance().rooms[currentRoom].buttons[data[1]];
                button.id = data[0];
                if (data[2] != "" && data[2] != "0" && data[2].StartsWith("0")) travelbuttons.Add(button);
                EditorManager.Instance().rooms[currentRoom].buttons.Add(data[0], button);
                EditorManager.Instance().rooms[currentRoom].buttons.Remove(data[1]);
            }
        }
        else
        {
            Debug.LogError(webResponse);
        }
    }
}
