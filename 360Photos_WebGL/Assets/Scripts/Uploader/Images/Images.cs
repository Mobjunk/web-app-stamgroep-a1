[System.Serializable]
public class Images
{
    public int ID;
    public string FILE_NAME;

    public Images(int id, string fileName)
    {
        ID = id;
        FILE_NAME = fileName;
    }
}