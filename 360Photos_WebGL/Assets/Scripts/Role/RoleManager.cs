using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : JsonHandler<Role>
{
    public static RoleManager instance;
    
    protected override string GetFileName()
    {
        return "";
    }

    protected override string GetPath()
    {
        return "";
    }

    protected override string GetLink()
    {
        return $"{Utility.action_url}roles";
    }

    protected override string GetInsertLink()
    {
        return "";
    }

    public override void Start()
    {
        instance = this;
        Load(false);
    }

    public override void Update() { }

    public Role GetRoleByName(string roleName)
    {
        foreach (Role role in entries)
        {
            if (role.ROLE_NAME.Equals(roleName))
            {
                return role;
            }
        }
        return null;
    }
}
