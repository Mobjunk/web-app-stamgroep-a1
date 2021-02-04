using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserList : WebRequestManager
{
    [SerializeField] private InputField searchBar;
    [SerializeField] private int page = 0;
    [SerializeField] private int numberOfUsers = 30;
    [SerializeField] private Transform usersParent;
    [SerializeField] private GameObject userDisplay;

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
        form.AddField("numberOfUsers", numberOfUsers);
        form.AddField("page", page);

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

        string[] users = webResponse.Split(';');

        foreach (Transform child in usersParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var user in users)
        {
            if (user != "")
            {
                string[] userInfo = user.Replace("User: ", "").Split(',');

                GameObject placedUserDisplay = Instantiate(userDisplay, usersParent);

                placedUserDisplay.name = "User ID#" + userInfo[0];
                Transform information = placedUserDisplay.transform.GetChild(0);
                information.Find("Name").GetComponent<Text>().text = userInfo[1];
                information.Find("Email").GetComponent<Text>().text = userInfo[2];
                information.Find("Role").GetComponent<Text>().text = userInfo[6];
                information.Find("Classes").GetComponent<Text>().text = userInfo[5];
            }
        }
    }
}
