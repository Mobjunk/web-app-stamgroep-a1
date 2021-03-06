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

    public static UserList instance;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

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
        if (gameManager.CurrentUser == null)
        {
            Debug.LogError("Trying to get users while not logged in!");
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
            Debug.LogError(webResponse);
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

                Users userClass = new Users(int.Parse(userInfo[0]), userInfo[1], userInfo[2], userInfo[3], userInfo[4], userInfo[6], userInfo[5]);
                GameObject placedUserDisplay = Instantiate(userDisplay, usersParent);

                placedUserDisplay.name = "User ID#" + userClass.id;
                Transform information = placedUserDisplay.transform.GetChild(0);
                information.Find("Name").GetComponent<Text>().text = userClass.username;
                information.Find("Email").GetComponent<Text>().text = userClass.email;
                string role = "";
                foreach (var item in userClass.roles)
                {
                    role += item.ROLE_NAME;
                    role += ", ";
                }
                role = role.Remove(role.Length - 2,2);
                information.Find("Role").GetComponent<Text>().text = role;

                string classes = "";
                foreach (var item in userClass.classes)
                {
                    classes += item.CLASS_NAME;
                    classes += ", ";
                }
                if(classes.Length > 2) classes = classes.Remove(classes.Length - 2,2);
                information.Find("Classes").GetComponent<Text>().text = classes;

                Transform buttons = placedUserDisplay.transform.GetChild(1);
                if (gameManager.CurrentUser.username == userInfo[1]) buttons.Find("Delete").gameObject.SetActive(false);
                else buttons.Find("Delete").GetComponent<Button>().onClick.AddListener(() => RemoveUserSystem.instance.RemoveUser(userInfo[0]));

                if (gameManager.CurrentUser.username == userInfo[1]) buttons.Find("Block").gameObject.SetActive(false);
                else
                {
                    bool blockState;
                    if (userInfo[7] == "0") blockState = false;
                    else blockState = true;
                    Button blockButton = buttons.Find("Block").GetComponent<Button>();
                    if (blockState) blockButton.GetComponentInChildren<Text>().text = "Deblokkeren";
                    else blockButton.GetComponentInChildren<Text>().text = "Blokkeren";
                    blockButton.onClick.AddListener(() => BlockUserSystem.instance.BlockUser(userInfo[0], (!blockState).ToString()));
                }

                buttons.Find("Edit").GetComponent<Button>().onClick.AddListener(() => StartCoroutine(EditUserSystem.instance.OpenEditUserPanel(userClass)));
            }
        }
    }

    public void PageRight()
    {
        page++;
        Debug.Log(page);
    }

    public void PageLeft()
    {
        page--;
        Debug.Log(page);
    }
}
