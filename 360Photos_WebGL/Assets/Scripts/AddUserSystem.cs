using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddUserSystem : WebRequestManager
{
    [SerializeField] private GameObject addUserPanel;
    [SerializeField] private InputField userNameInput;
    [SerializeField] private InputField firstNameInput;
    [SerializeField] private InputField lastNameInput;
    [SerializeField] private InputField emailInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private Dropdown roleDropdown;
    [SerializeField] private Dropdown classDropdown;

    private Class[] classes;
    private Role[] roles;

    public void OpenAddUserPanel()
    {
        if(addUserPanel) addUserPanel.SetActive(true);

        userNameInput.text = "";
        firstNameInput.text = "";
        lastNameInput.text = "";
        emailInput.text = "";
        passwordInput.text = "";
        roleDropdown.value = 0;
        classDropdown.value = 0;

        StartCoroutine(GetRequest($"{Utility.action_url}classes"));
        StartCoroutine(GetRequest($"{Utility.action_url}roles"));
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
        if(classDropdown.value != 0) form.AddField("class", classDropdown.options[classDropdown.value].text);
        form.AddField("role", roleDropdown.options[roleDropdown.value].text);

        StartCoroutine(PostRequest($"{Utility.action_url}createUser", form));
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

        List<Dropdown.OptionData> classOptions = new List<Dropdown.OptionData> { new Dropdown.OptionData("None") };
        foreach (var klas in classes)
        {
            if (klas.CLASS_NAME != null)
            {
                classOptions.Add(new Dropdown.OptionData(klas.CLASS_NAME));
            }
        }
        if (classOptions.Count > 1)
        {
            classDropdown.options = classOptions;
        }

        List<Dropdown.OptionData> roleOptions = new List<Dropdown.OptionData>();
        foreach (var rol in roles)
        {
            if (rol.ROLE_NAME != null)
            {
                roleOptions.Add(new Dropdown.OptionData(rol.ROLE_NAME));
            }
        }
        if (roleOptions.Count > 0)
        {
            roleDropdown.options = roleOptions;
        }
    }
}
