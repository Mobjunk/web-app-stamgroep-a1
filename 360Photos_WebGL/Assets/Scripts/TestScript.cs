using UnityEngine;

public class TestScript : WebRequestManager
{
    public override void SetupResponse()
    {
        base.SetupResponse();

        WWWForm form = new WWWForm();
        form.AddField("searchVector", "marc");
        
        StartCoroutine(PostRequest($"{Utility.action_url}users", form));
    }

    public override void FinishedResponse()
    {
        Debug.Log("webError: " + webError);
        Debug.Log("webResponse: " + webResponse);
    }
}