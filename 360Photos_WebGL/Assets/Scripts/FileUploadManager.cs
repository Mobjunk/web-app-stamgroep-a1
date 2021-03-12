using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class FileUploadManager : WebRequestManager
{
    [DllImport("__Internal")]
    private static extern void ImageUploaderCaptureClick();
    [DllImport("__Internal")]
    private static extern void ImageUploaderInit();
 
    /// <summary>
    /// Handles placing the upload button on the webpage
    /// </summary>
    void Start () {
        ImageUploaderInit ();
    }

    /// <summary>
    /// Name of the file thats being uploaded
    /// </summary>
    private string fileName;
 
    IEnumerator UploadImage(string url) {
        Console.WriteLine("Call 2");
        WWW image = new WWW (url);
        yield return image;
        
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", image.bytes, fileName);
        
        StartCoroutine(PostRequest($"{Utility.action_url}upload", form));
    }
 
    void FileSelected(string url) {
        Console.WriteLine("Call 1");
        StartCoroutine(UploadImage (url));
    }

    /// <summary>
    /// Handles receiving the file name from the webgl application
    /// </summary>
    /// <param name="fileName"></param>
    void FileName(string fileName)
    {
        this.fileName = fileName;
    }
 
    public void OnButtonPointerDown() {
        ImageUploaderCaptureClick();
    }

    public override void FinishedResponse()
    {
        Debug.Log("webResponse: " + webResponse);
    }
}
