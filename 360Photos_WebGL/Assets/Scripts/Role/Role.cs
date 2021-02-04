[System.Serializable]
public class Role
{
    public int ID;
    public string ROLE_NAME;

    public Role(int id, string roleName)
    {
        ID = id;
        ROLE_NAME = roleName;
    }
}