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

        StartCoroutine(GetRequest($"{Utility.action_url}classes"));
        StartCoroutine(GetRequest($"{Utility.action_url}roles"));
    }

    public void CreateUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", userNameInput.text);
        form.AddField("password", passwordInput.text);
        form.AddField("firstName", firstNameInput.text);
        form.AddField("lastName", lastNameInput.text);
        form.AddField("email", emailInput.text);
        form.AddField("class", classDropdown.itemText.text);
        form.AddField("role", roleDropdown.itemText.text);

        StartCoroutine(PostRequest($"{Utility.action_url}createUser", form));
    }

    public override void FinishedResponse()
    {
        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            Debug.LogError(webError);
            return;
        }

        if (webResponse == "") return;

        classes = JsonHelper.FromJson<Class>(webResponse);
        roles = JsonHelper.FromJson<Role>(webResponse);

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (var klas in classes)
        {
            if (klas.CLASS_NAME != null)
            {
                options.Add(new Dropdown.OptionData(klas.CLASS_NAME));
            }
        }
        if (options.Count > 0)
        {
            classDropdown.options = options;
        }

        options.Clear();
        foreach (var rol in roles)
        {
            if (rol.ROLE_NAME != null)
            {
                options.Add(new Dropdown.OptionData(rol.ROLE_NAME));
            }
        }
        if (options.Count > 0)
        {
            roleDropdown.options = options;
        }
    }
}
