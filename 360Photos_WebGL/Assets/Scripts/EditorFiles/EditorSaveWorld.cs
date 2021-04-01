using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSaveWorld : WebRequestManager
{
    public override void FinishedResponse()
    {
        if(webResponse.Contains("Succesfully"))
        {

        }
        else
        {
            Debug.LogError(webResponse);
        }
    }
}
