public class Users
{
    public int id;
    public string username;
    public string email;
    public string firstName;
    public string lastName;
    public Class currentClass;
    public Role role;

    public Users(int id, string username, string email, string firstName, string lastName)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
    }
}