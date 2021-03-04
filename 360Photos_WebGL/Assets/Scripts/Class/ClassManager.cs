using System.Linq;

public class ClassManager : JsonHandler<Class>
{
    public static ClassManager instance;
    
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
        return $"{Utility.action_url}classes";
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

    public Class GetClassByName(string className)
    {
        return entries.FirstOrDefault(c => c.CLASS_NAME.Equals(className));
    }

    public Class GetClassByID(string classID)
    {
        int.TryParse(classID, out int ID);
        return entries.FirstOrDefault(c => c.ID.Equals(ID));
    }
}
