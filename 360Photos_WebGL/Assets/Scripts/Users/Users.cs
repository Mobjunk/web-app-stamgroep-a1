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
    public Class currentClass;
    public List<Role> roles = new List<Role>();

    public Users(int id, string username, string email, string firstName, string lastName, string currentRoles)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        AddRoles(currentRoles);
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