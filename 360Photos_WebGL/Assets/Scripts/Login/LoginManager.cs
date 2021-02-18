using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : WebRequestManager
{
    [Header("Login UI")]
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private TMP_Text errorText;

    public delegate void OnSuccesfulLogin();

    public OnSuccesfulLogin onSuccesfulLogin;

    void SuccesfulLogin()
    {
        onSuccesfulLogin?.Invoke();
    }

    public void LoginRequest()
    {
        errorPanel.SetActive(false);
        webResponse = string.Empty;
        webError = string.Empty;

        string username = inputUsername.text;
        string password = inputPassword.text;

        if (username == string.Empty || password == string.Empty)
        {
            SetErrorPanel("Username or password is empty.");
            return;
        }

        //TODO: Add a check for length for the password and maybe username
        
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        
        StartCoroutine(PostRequest($"{Utility.action_url}login", form));
    }

    public override void FinishedResponse()
    {
        //Check if the user isnt already logged in
        if (gameManager.CurrentUser != null)
        {
            SetErrorPanel("Trying to login when there is already someone logged in");
            return;
        }
        
        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            SetErrorPanel(webError);
            return;
        }
        
        //Checks if the web response contained unsuccessful
        if (webResponse.Contains("Unsuccessful"))
        {
            SetErrorPanel(webResponse);
            return;
        }
        
        string[] outcome = webResponse.Replace("Successful: ", "").Split(',');

        gameManager.CurrentUser = new Users(int.Parse(outcome[0]), outcome[1], outcome[2], outcome[3], outcome[4], outcome[6]);
        Utility.SwitchScenes("Login scene", "LoggedIn");
    }

    public void SetErrorPanel(string text)
    {
        errorPanel.SetActive(true);
        errorText.text = text;
    }
}
