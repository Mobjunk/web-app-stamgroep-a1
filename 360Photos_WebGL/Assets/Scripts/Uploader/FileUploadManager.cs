using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class FileUploadManager : WebRequestManager
{
    private ImageLoader _imageLoader => ImageLoader.Instance();

    private GameManager _gameManager => GameManager.Instance();
    
    [DllImport("__Internal")]
    private static extern void ImageUploaderCaptureClick();
    [DllImport("__Internal")]
    private static extern void ImageUploaderInit();
 
    /// <summary>
    /// Handles placing the upload button on the webpage
    /// </summary>
    void Start() {
        ImageUploaderInit();
    }

    /// <summary>
    /// Name of the file thats being uploaded
    /// </summary>
    private string fileName;
    /// <summary>
    /// The image that was uploaded converted to a sprite
    /// </summary>
    private Sprite spriteUploaded;
 
    IEnumerator UploadImage(string url) {
        WWW image = new WWW(url);
        yield return image;
        
        //TODO: Convert the uploadded stuff to sprite;
        
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", image.bytes, fileName);
        
        StartCoroutine(PostRequest($"{Utility.action_url}upload", form));
    }
 
    public void FileSelected(string url)
    {
        StartCoroutine(UploadImage(url));
    }

    /// <summary>
    /// Handles receiving the file name from the webgl application
    /// </summary>
    /// <param name="fileName"></param>
    public void FileName(string fileName)
    {
        this.fileName = fileName;
    }
 
    public void OnButtonPointerDown() {
        ImageUploaderCaptureClick();
    }

    public override void FinishedResponse()
    {
        Debug.Log("webResponse: " + webResponse);
        if (webResponse.Contains("Succes: "))
        {
            StartCoroutine(Utility.DownloadSprite(Utility.web_url + $"/images/{fileName}", (response) =>
            {
                spriteUploaded = response;
                
                //Handles adding the entry to the list
                int id = int.Parse(webResponse.Replace("Succes: ", ""));
                _imageLoader.Insert(new Images(id, fileName));
                
                _imageLoader.InsertImage();
            }));
        }
    }
}
