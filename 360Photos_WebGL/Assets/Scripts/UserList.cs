using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserList : WebRequestManager
{
    [SerializeField] private InputField searchBar;

    // Start is called before the first frame update
    void Start()
    {
        UserListRequest();
    }

    public void UserListRequest()
    {
        webResponse = string.Empty;
        webError = string.Empty;

        string searchVector = "";
        if(searchBar) searchVector = searchBar.text;

        //TODO: Add a check for length for the password and maybe username

        WWWForm form = new WWWForm();
        form.AddField("searchVector", searchVector);

        StartCoroutine(PostRequest($"{Utility.web_url}index.php?action=users", form));
    }

    public override void FinishedResponse()
    {
        //Check if the user is logged in
        //if (gameManger.CurrentUser == null)
        //{
        //    Debug.LogError("Trying to get users while not logged in!");
        //    return;
        //}

        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            Debug.LogError($"Web error: {webError}");
            return;
        }

        //Checks if the web response contained unsuccessful
        if (webResponse.Contains("Unsuccessful"))
        {
            Debug.LogError("Unsuccessful login do something!");
            return;
        }

        Debug.Log("webResponse: " + webResponse);

        string[] outcome = webResponse.Replace("Succesful: ", "").Split(',');

        //string className = outcome[5];
        //string roleName = outcome[6];

        //gameManger.CurrentUser = new Users(int.Parse(outcome[0]), outcome[1], outcome[2], outcome[3], outcome[4]);
    }
}
