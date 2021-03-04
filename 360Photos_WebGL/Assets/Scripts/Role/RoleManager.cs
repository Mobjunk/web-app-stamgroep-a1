using System.Linq;

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
        return entries.FirstOrDefault(role => role.ROLE_NAME.Equals(roleName));
    }

    public Role GetRoleByID(string roleID)
    {
        int.TryParse(roleID, out int ID);
        return entries.FirstOrDefault(role => role.ID.Equals(ID));
    }
}
