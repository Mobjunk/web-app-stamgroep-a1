using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : Singleton<ImageLoader>
{
    private ImageManager _imageManager => ImageManager.instance;
    private FileRemover _fileRemover => FileRemover.instance;

    [SerializeField] private bool finishedLoadingImages = false;
    
    
    [Header("Image related")]
    [SerializeField] private GameObject parentContainer;
    [SerializeField] private GameObject prefabImage;

    [Header("Title")] [SerializeField] private TMP_Text title;
    
    [Header("Page details")]
    [SerializeField] private int pageId = 0;
    [SerializeField] private int startingPoint;
    [SerializeField] private int endingPoint;
    [SerializeField] private int maxImages;
    [SerializeField] private int maxPages;
    
    void Update()
    {
        //Checks if fishing loading hasnt been set yet and the image manager is finished loading...
        if (!finishedLoadingImages && _imageManager.finishedLoading)
        {
            SetupImages();
            finishedLoadingImages = true;
        }
    }

    /// <summary>
    /// Handles setting up the ui with the correct images...
    /// </summary>
    void SetupImages(bool newestFirst = true)
    {
        //Makes sure it always showst the newest first
        if (newestFirst)
        {
            _imageManager.entries.Sort((a, b) => a.ID.CompareTo(b.ID));
            _imageManager.entries.Reverse();
        }
        
        //Gets how many images there are
        maxImages = _imageManager.entries.Count;
        
        //How many pages there are
        maxPages = maxImages > 5 ? maxImages / 5 : 0;

        //Make sure the pageId cannot be higher then max pages
        if (pageId > maxPages) pageId = maxPages;
        
        //Handles getting the starting and ending index for pages
        startingPoint = 5 * pageId;
        endingPoint = 5 * (pageId + 1);
        
        //Sets the title
        title.text = $"Foto's - Page {pageId + 1} of {maxPages + 1}";

        //Makes sure the end point never reaches above the max amount of images
        if (endingPoint > maxImages)
            endingPoint = maxImages;
        
        Debug.Log($"Start setting up the images using page: {pageId}, startingPoint: {startingPoint}, endingPoint: {endingPoint}, maxImages: {maxImages}....");
        
        for (int index = startingPoint; index < endingPoint; index++)
            InsertEntry(index, false);
    }

    /// <summary>
    /// Handles inserting a new child to the parent container
    /// </summary>
    /// <param name="index">The index within the entries list</param>
    /// <param name="setAsFirst">If the child needs to be set as the first child</param>
    void InsertEntry(int index, bool setAsFirst)
    {
        var entry = _imageManager.entries[index];

        GameObject entryObject = Instantiate(prefabImage, parentContainer.transform, true);
        entryObject.transform.localScale = new Vector3(1,1,1);
        entryObject.transform.name = $"{entry.ID} - {entry.FILE_NAME}";
        if(setAsFirst) entryObject.transform.SetAsFirstSibling();

        PreviewImage preview = entryObject.GetComponentInChildren<PreviewImage>();
        preview.SetData(entry.FILE_NAME, index);
    }

    /// <summary>
    /// Handles inserting the newest upload to the top of the page
    /// </summary>
    public void InsertImage()
    {
        int childCount = parentContainer.transform.childCount;
        if (childCount == 5)
            Destroy(parentContainer.transform.GetChild(childCount - 1).gameObject);
        InsertEntry(0, true);
    }

    /// <summary>
    /// Handles inserting a new entry to the local list without having to reload the data from the webserver
    /// </summary>
    /// <param name="image"></param>
    public void Insert(Images image)
    {
        _imageManager.entries.Insert(0, image);
        maxPages = _imageManager.entries.Count > 5 ? _imageManager.entries.Count / 5 : 0;
        
        //Sets the title
        title.text = $"Foto's - Page {pageId + 1} of {maxPages + 1}";
    }

    /// <summary>
    /// Handles switching to the previous page
    /// </summary>
    public void PreviousPage()
    {
        //Checks if the page your trying to reach is lower then 0
        if (pageId - 1 < 0)
        {
            Debug.Log("Cannot go lower in pages");
            return;
        }

        pageId--;
        ReloadPage();
    }

    /// <summary>
    /// Handles switching to the next page
    /// </summary>
    public void NextPage()
    {
        //Checks if the page your trying to reach is higher then the max pages
        if (pageId + 1 > maxPages)
        {
            Debug.Log($"Cannot go higher then {maxPages} pages");
            return;
        }

        pageId++;
        ReloadPage();
    }

    /// <summary>
    /// Handles reloading the page with the new data
    /// Also removes the old children from the parent container
    /// </summary>
    private void ReloadPage()
    {
        int childCount = parentContainer.transform.childCount;
        GameObject[] oldChildren = new GameObject[childCount];

        for (int index = 0; index < childCount; index++)
        {
            GameObject child = parentContainer.transform.GetChild(index).gameObject;
            child.SetActive(false);
            oldChildren[index] = child;
        }

        SetupImages(false);
        
        foreach(GameObject oldChild in oldChildren)
            Destroy(oldChild);
    }

    /// <summary>
    /// Handles deleting a entry from the container
    /// </summary>
    /// <param name="index"></param>
    public void DeleteEntry(int index)
    {
        //TODO: Make it just add a new entry to the end of the page instead of deleting the whole page and refreshing it
        
        _fileRemover.RemoveImage(_imageManager.entries[index].ID, _imageManager.entries[index].FILE_NAME);
        
        _imageManager.entries.RemoveAt(index);
        ReloadPage();
    }
}
