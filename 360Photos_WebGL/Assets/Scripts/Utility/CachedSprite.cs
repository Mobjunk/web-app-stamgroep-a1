using UnityEngine;

public class CachedSprite
{
    public string fileName;
    public Sprite sprite;

    public CachedSprite(string fileName, Sprite sprite)
    {
        this.fileName = fileName;
        this.sprite = sprite;
    }
}
