using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveUserSystem : WebRequestManager
{
    public void RemoveUser(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        StartCoroutine(PostRequest($"{Utility.action_url}deleteUser", form));
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
            UserList.instance.UserListRequest();
        }
    }
}
