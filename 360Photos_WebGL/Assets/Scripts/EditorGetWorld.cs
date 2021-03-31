using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGetWorld : WebRequestManager
{
    public string[] data;
    public override void FinishedResponse()
    {
        if (webResponse.Contains("world:"))
        {
             data = webResponse.Replace("world: ", "").Split(',');
        }
        else
        {
            Debug.LogError(webResponse);
        }
    }
}
