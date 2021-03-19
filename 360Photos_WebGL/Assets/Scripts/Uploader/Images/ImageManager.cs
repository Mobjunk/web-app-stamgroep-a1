public class ImageManager : JsonHandler<Images>
{
    public static ImageManager instance;
    
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
        return $"{Utility.action_url}images";
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
}
