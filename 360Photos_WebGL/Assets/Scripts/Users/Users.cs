using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Users
{
    public int id;
    public string username;
    public string email;
    public string firstName;
    public string lastName;
    public string password;
    public Class currentClass;
    public List<Role> roles = new List<Role>();
    public List<Class> classes = new List<Class>();

    public Users(int id, string username, string email, string firstName, string lastName, string currentRoles, string currentClasses, string password = "")
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.password = password;
        AddRoles(currentRoles);
        AddClass(currentClasses);
    }

    private void AddRoles(string currentRoles)
    {
        string[] splitRoles = currentRoles.Split(':');
        foreach (string roleName in splitRoles)
        {
            Role role = RoleManager.instance.GetRoleByName(roleName);
            if (role == null) continue;
            roles.Add(role);
        }
    }

    private void AddClass(string currentClass)
    {
        string[] splitRoles = currentClass.Split(':');
        foreach (string roleName in splitRoles)
        {
            Class @class = ClassManager.instance.GetClassByName(roleName);
            if (@class == null) continue;
            classes.Add(@class);
        }
    }

    private bool ContainsRole(string roleName)
    {
        return roles.Any(role => role.ROLE_NAME.Equals(roleName));
    }

    public bool isTeacher()
    {
        return ContainsRole("Teacher");
    }

    public bool IsAdmin()
    {
        return ContainsRole("Administrator");
    }
}