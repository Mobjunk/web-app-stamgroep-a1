using TMPro;
using UnityEngine;

public class LoginManager : WebRequestManager
{
    [Header("Login UI")]
    [SerializeField] TMP_InputField inputUsername;
    [SerializeField] TMP_InputField inputPassword;

    public void LoginRequest()
    {
        webResponse = string.Empty;
        webError = string.Empty;

        string username = inputUsername.text;
        string password = inputPassword.text;

        if (username == string.Empty || password == string.Empty)
        {
            Debug.Log("Username or password is empty.");
            return;
        }

        //TODO: Add a check for length for the password and maybe username
        
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        
        StartCoroutine(PostRequest($"{Utility.web_url}index.php?action=login", form));
    }

    public override void FinishedResponse()
    {
        //Check if the user isnt already logged in
        if (gameManger.CurrentUser != null)
        {
            Debug.LogError("Trying to login when there is already someone logged in");
            return;
        }
        
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

        gameManger.CurrentUser = new Users(int.Parse(outcome[0]), outcome[1], outcome[2], outcome[3], outcome[4]);
    }
}
