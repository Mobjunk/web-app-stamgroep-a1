using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewHandler : Singleton<PreviewHandler>
{

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GameObject _previewObject;
    [SerializeField] private TMP_Text _previewText;
    [SerializeField] private Image _imageHandler;

    public void Open(string previewName, Sprite sprite)
    {
        float width = sprite.rect.width;
        float height = sprite.rect.height;
        
        if (width > 1820) width = 1820;
        if (height > 980) height = 980;
        
        _rectTransform.sizeDelta = new Vector2(width, height);
        
        _previewText.text = $"Preview of: {previewName}";
        _imageHandler.sprite = sprite;
        _previewObject.SetActive(true);
    }
    
    public void Close()
    {
        _previewObject.SetActive(false);
    }
}
