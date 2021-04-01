using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PreviewImage : MonoBehaviour
{
    private PreviewHandler _previewHandler => PreviewHandler.Instance();

    private GameManager _gameManager => GameManager.Instance();
    
    private ImageLoader _imageLoader => ImageLoader.Instance();

    private int _index;
    private RectTransform _rectTransform;
    private Image _image;
    private string _filename;
    [SerializeField] private Sprite _targetSprite;
    [SerializeField] private TMP_Text imageName;
    
    public Sprite TargetSprite
    {
        get => _targetSprite;
        set => _targetSprite = value;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    public void ClickedImage()
    {
        _previewHandler.Open(imageName.text, _targetSprite);
    }

    
    /// <summary>
    /// Handles setting the data of this object
    /// </summary>
    /// <param name="fileName">The file name</param>
    /// <param name="index">The index within the list</param>
    public void SetData(string fileName, int index)
    {
        imageName.text = fileName;
        _index = index;
        _filename = fileName;

        if (_gameManager.SpriteIsCached(fileName))
        {
            Debug.Log($"{fileName} is already in the cache...");
            AdjustImage(_gameManager.GetCachedSprite(fileName), fileName);
        }
        else
        {
            Debug.Log("Downloading the image to put in cache...");
            StartCoroutine(Utility.DownloadSprite(Utility.web_url + $"/images/{fileName}", (response) => {
                AdjustImage(response, fileName, true);
            }));            
        }
    }

    /// <summary>
    /// Handles the actions of adjusting a image
    /// </summary>
    /// <param name="sprite">The sprite</param>
    /// <param name="fileName">The name of the sprite</param>
    /// <param name="addToCache">If the sprite will be added to cache</param>
    private void AdjustImage(Sprite sprite, string fileName, bool addToCache = false)
    {
        _targetSprite = sprite;
        
        if (_targetSprite.rect.width < 250 && _targetSprite.rect.height < 250) _rectTransform.sizeDelta = new Vector2(_targetSprite.rect.width, _targetSprite.rect.height);
        else _rectTransform.sizeDelta = new Vector2(250, 250);
            
        _image.sprite = _targetSprite;
        
        if(addToCache) _gameManager.AddCachedSprite(fileName, _targetSprite);
    }
    
    public void Delete()
    {
        _imageLoader.DeleteEntry(_index);
    }

    public void SelectImage()
    {
        StartCoroutine(EditorManager.Instance().photoChanger.DownloadPhoto(_filename, EditorManager.Instance().GetActiveRoom()));
        EditorManager.Instance().imageSelector.SetActive(false);
    }
}
