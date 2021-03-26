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
    [SerializeField] private DropdownMulti roleDropdown;
    [SerializeField] private DropdownMulti classDropdown;
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

    public IEnumerator OpenEditUserPanel(Users user)
    {
        latestUser = user;

        yield return StartCoroutine(GetRequest($"{Utility.action_url}classes"));
        yield return StartCoroutine(GetRequest($"{Utility.action_url}roles"));

        userNameInput.text = user.username;
        firstNameInput.text = user.firstName;
        lastNameInput.text = user.lastName;
        emailInput.text = user.email;
        passwordInput.transform.parent.gameObject.SetActive(false);
        List<int> roles = new List<int>();
        foreach (var role in user.roles)
        {
            roles.Add(role.ID - 1);
        }
        roleDropdown.value = roles.ToArray();
        List<int> classes = new List<int>();
        foreach (var klas in user.classes)
        {
            classes.Add(klas.ID - 1);
        }
        classDropdown.value = classes.ToArray();
        doneButton.onClick.RemoveAllListeners();
        doneButton.onClick.AddListener(() => EditUser());

        if (editUserPanel) editUserPanel.SetActive(true);
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
        string classes = "";
        foreach (var value in classDropdown.value)
        {
            classes += value + 1 + ":";
        }
        if (classes.Length > 1) classes = classes.Remove(classes.Length - 1);
        form.AddField("class", classes);
        string roles = "";
        foreach (var value in roleDropdown.value)
        {
            roles += value + 1 + ":";
        }
        roles = roles.Remove(roles.Length - 1);
        form.AddField("role", roles);

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

        Debug.LogError(webResponse);
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
