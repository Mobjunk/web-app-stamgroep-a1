using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddQuizSystem : WebRequestManager
{
    [SerializeField] private GameObject addUserPanel;
    [SerializeField] private InputField userNameInput;
    [SerializeField] private InputField firstNameInput;
    [SerializeField] private InputField lastNameInput;
    [SerializeField] private InputField emailInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private DropdownMulti roleDropdown;
    [SerializeField] private DropdownMulti classDropdown;
    [SerializeField] private Button doneButton;

    private Class[] classes;
    private Role[] roles;

    public void OpenAddQuizPanel()
    {
        StartCoroutine(OpenAddQuizPanelEnum());
    }

    public IEnumerator OpenAddQuizPanelEnum()
    {
        yield return StartCoroutine(GetRequest($"{Utility.action_url}classes"));
        yield return StartCoroutine(GetRequest($"{Utility.action_url}roles"));

        userNameInput.text = "";
        firstNameInput.text = "";
        lastNameInput.text = "";
        emailInput.text = "";
        passwordInput.transform.parent.gameObject.SetActive(true);
        passwordInput.text = "";
        roleDropdown.value = new int[] { 0};
        classDropdown.value = new int[0];
        doneButton.onClick.RemoveAllListeners();
        doneButton.onClick.AddListener(() => CreateUser());

        if (addUserPanel) addUserPanel.SetActive(true);
    }

    public void CreateUser()
    {
        if (userNameInput.text == "") return;
        if (passwordInput.text == "") return;
        if (emailInput.text == "") return;
        if (firstNameInput.text == "") return;
        if (lastNameInput.text == "") return;

        WWWForm form = new WWWForm();
        form.AddField("username", userNameInput.text);
        form.AddField("password", passwordInput.text);
        form.AddField("firstName", firstNameInput.text);
        form.AddField("lastName", lastNameInput.text);
        form.AddField("email", emailInput.text);
        string classes = "";
        foreach (var value in classDropdown.value)
        {
            classes += value + 1 + ":";
        }
        classes = classes.Remove(classes.Length - 1);
        form.AddField("class", classes);
        string roles = "";
        foreach (var value in roleDropdown.value)
        {
            roles += value + 1 + ":";
        }
        roles = roles.Remove(roles.Length - 1);
        form.AddField("role", roles);

        StartCoroutine(PostRequest($"{Utility.action_url}createQuiz", form));
        if (addUserPanel) addUserPanel.SetActive(false);
    }

    public override void FinishedResponse()
    {
        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            Debug.LogError(webError);
            return;
        }

        if (webResponse == "Succesfully added a new user!")
        {
            UserList.instance.UserListRequest();
            return;
        }

        classes = JsonHelper.FromJson<Class>(webResponse);
        roles = JsonHelper.FromJson<Role>(webResponse);

        List<DropdownMulti.OptionData> classOptions = new List<DropdownMulti.OptionData>();
        foreach (var klas in classes)
        {
            if (klas.CLASS_NAME != null)
            {
                classOptions.Add(new DropdownMulti.OptionData(klas.CLASS_NAME));
            }
        }
        if (classOptions.Count > 1)
        {
            classDropdown.options = classOptions;
        }

        List<DropdownMulti.OptionData> roleOptions = new List<DropdownMulti.OptionData>();
        foreach (var rol in roles)
        {
            if (rol.ROLE_NAME != null)
            {
                roleOptions.Add(new DropdownMulti.OptionData(rol.ROLE_NAME));
            }
        }
        if (roleOptions.Count > 0)
        {
            roleDropdown.options = roleOptions;
        }
    }
}
