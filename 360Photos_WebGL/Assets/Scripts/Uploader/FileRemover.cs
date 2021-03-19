using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileRemover : WebRequestManager
{
    public static FileRemover instance;
    
    private void Start()
    {
        instance = this;
    }

    public void RemoveImage(int id, string fileName)
    {
        webResponse = string.Empty;
        webError = string.Empty;

        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("fileName", fileName);
        
        StartCoroutine(PostRequest($"{Utility.action_url}removeImage", form));
    }
    
    public override void FinishedResponse()
    {
        Debug.Log("webResponse: " + webResponse);
    }
}
