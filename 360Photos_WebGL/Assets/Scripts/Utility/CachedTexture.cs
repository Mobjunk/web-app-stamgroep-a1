using UnityEngine;

public class CachedTexture
{
    public string fileName;
    public Texture texture;

    public CachedTexture(string fileName, Texture texture)
    {
        this.fileName = fileName;
        this.texture = texture;
    }
}
