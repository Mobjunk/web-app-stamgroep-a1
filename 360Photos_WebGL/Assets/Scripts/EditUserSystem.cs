using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditUserSystem : WebRequestManager
{
    [SerializeField] private GameObject editUserPanel;
    [SerializeField] private InputField userNameInput;
    [SerializeField] private InputField firstNameInput;
    [SerializeField] private InputField lastNameInput;
    [SerializeField] private InputField emailInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private Dropdown roleDropdown;
    [SerializeField] private Dropdown classDropdown;
    [SerializeField] private Button doneButton;

    private Class[] classes;
    private Role[] roles;
    public static EditUserSystem instance;
    private Users latestUser;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public void OpenEditUserPanel(Users user)
    {
        latestUser = user;
        if(editUserPanel) editUserPanel.SetActive(true);

        StartCoroutine(GetRequest($"{Utility.action_url}classes"));
        StartCoroutine(GetRequest($"{Utility.action_url}roles"));

        userNameInput.text = user.username;
        firstNameInput.text = user.firstName;
        lastNameInput.text = user.lastName;
        emailInput.text = user.email;
        passwordInput.transform.parent.gameObject.SetActive(false);
        roleDropdown.value = user.roles[0].ID - 1;
        if (user.classes.Count > 0) classDropdown.value = user.classes[0].ID;
        else classDropdown.value = 0;
        doneButton.onClick.RemoveAllListeners();
        doneButton.onClick.AddListener(() => EditUser());
    }

    public void EditUser()
    {
        if (userNameInput.text == "") return;
        if (emailInput.text == "") return;
        if (firstNameInput.text == "") return;
        if (lastNameInput.text == "") return;

        WWWForm form = new WWWForm();
        form.AddField("id", latestUser.id);
        form.AddField("username", userNameInput.text);
        form.AddField("firstName", firstNameInput.text);
        form.AddField("lastName", lastNameInput.text);
        form.AddField("email", emailInput.text);
        if(classDropdown.value != 0) form.AddField("class", classDropdown.options[classDropdown.value].text);
        form.AddField("role", roleDropdown.options[roleDropdown.value].text);

        StartCoroutine(PostRequest($"{Utility.action_url}editUser", form));
        if (editUserPanel) editUserPanel.SetActive(false);
    }

    public override void FinishedResponse()
    {
        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            Debug.LogError(webError);
            return;
        }

        if (webResponse == "Succesfully edited user!")
        {
            UserList.instance.UserListRequest();
            return;
        }
        else if (webResponse.Contains("error"))
        {
            Debug.LogError(webResponse);
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
