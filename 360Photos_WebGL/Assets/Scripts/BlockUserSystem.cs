using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUserSystem : WebRequestManager
{
    public static BlockUserSystem instance;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public void BlockUser(string id, string blockedValue)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("block", blockedValue);
        StartCoroutine(PostRequest($"{Utility.action_url}blockUser", form));
    }

    public override void FinishedResponse()
    {
        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            Debug.LogError(webError);
            return;
        }
        else
        {
            QuizList.instance.QuizListRequest();
        }
    }
}
