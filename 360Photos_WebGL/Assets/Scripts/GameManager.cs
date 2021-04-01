using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// A list of the already downloaded sprites from the web server
    /// </summary>
    public List<CachedSprite> cachedSprites = new List<CachedSprite>();
    public List<CachedTexture> cachedTextures = new List<CachedTexture>();

    /// <summary>
    /// Handles adding a sprite to the cache
    /// </summary>
    /// <param name="fileName">The file name of the sprite</param>
    /// <param name="sprite">The sprite</param>
    public void AddCachedSprite(string fileName, Sprite sprite)
    {
        Debug.Log($"Added {fileName} to the cached files...");
        cachedSprites.Add(new CachedSprite(fileName, sprite));
    }

    /// <summary>
    /// Handles checking if the sprite is already cached
    /// </summary>
    /// <param name="fileName">The name of the file</param>
    /// <returns></returns>
    public bool SpriteIsCached(string fileName)
    {
        return cachedSprites.Any(cachedSprite => cachedSprite.fileName.Equals(fileName));
    }

    /// <summary>
    /// Handles getting the sprite for a  file name
    /// </summary>
    /// <param name="fileName">The name of the file</param>
    /// <returns></returns>
    public Sprite GetCachedSprite(string fileName)
    {
        return (from cachedSprite in cachedSprites where cachedSprite != null && cachedSprite.fileName.Equals(fileName) select cachedSprite.sprite).FirstOrDefault();
    }

    /// <summary>
    /// Handles adding a sprite to the cache
    /// </summary>
    /// <param name="fileName">The file name of the sprite</param>
    /// <param name="sprite">The sprite</param>
    public void AddCachedTexture(string fileName, Texture texture)
    {
        Debug.Log($"Added {fileName} to the cached files...");
        cachedTextures.Add(new CachedTexture(fileName, texture));
    }

    /// <summary>
    /// Handles checking if the sprite is already cached
    /// </summary>
    /// <param name="fileName">The name of the file</param>
    /// <returns></returns>
    public bool TextureIsCached(string fileName)
    {
        return cachedTextures.Any(cachedTextures => cachedTextures.fileName.Equals(fileName));
    }

    /// <summary>
    /// Handles getting the sprite for a  file name
    /// </summary>
    /// <param name="fileName">The name of the file</param>
    /// <returns></returns>
    public Texture GetCachedTexture(string fileName)
    {
        return (from cachedTexture in cachedTextures where cachedTexture != null && cachedTexture.fileName.Equals(fileName) select cachedTexture.texture).FirstOrDefault();
    }

    /// <summary>
    /// The current user that is logged in
    /// </summary>
    private Users currentUser;
    
    /// <summary>
    /// Get and setting for the current user
    /// </summary>
    public Users CurrentUser
    {
        get => currentUser;
        set => currentUser = value;
    }

    private void Awake()
    {
        Utility.AddSceneIfNotLoaded("Login Scene");
    }
}
