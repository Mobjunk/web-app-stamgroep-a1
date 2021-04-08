using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSaveButton : WebRequestManager
{
    public List<string> buttonsID = new List<string>();

    public override void FinishedResponse()
    {
        if (webResponse.Contains("Succesfully"))
        {
            if (webResponse.Contains("updated"))
            {
                string data = webResponse.Replace("Succesfully updated button with id: ", "");
                buttonsID.Add(data);
            }
            else if (webResponse.Contains("created"))
            {
                string data = webResponse.Replace("Succesfully created button with id: ", "");
                buttonsID.Add(data);
            }
        }
        else
        {
            Debug.LogError(webResponse);
        }
    }
}
