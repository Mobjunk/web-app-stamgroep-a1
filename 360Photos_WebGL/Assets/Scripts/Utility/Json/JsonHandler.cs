using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public abstract class JsonHandler<T> : MonoBehaviour
{
    /// <summary>
    /// Checks if it should be using a local file
    /// </summary>
    private bool localFile;
    /// <summary>
    /// The json string used for converting
    /// </summary>
    private string jsonString = "";
    /// <summary>
    /// Checks if its done loading everything
    /// </summary>
    public bool finishedLoading = false;
    /// <summary>
    /// A list of all the possible entries based on the generic type
    /// </summary>
    public List<T> entries = new List<T>();

    /// <summary>
    /// The file name of the json file
    /// </summary>
    /// <returns>The file name of the json file</returns>
    protected abstract string GetFileName();

    /// <summary>
    /// The path where the file can be found
    /// </summary>
    /// <returns>The path where the file can be found</returns>
    protected abstract string GetPath();

    /// <summary>
    /// The link where the json file is located
    /// </summary>
    /// <returns>The link where the json file is located</returns>
    protected abstract string GetLink();
    /// <summary>
    /// The link it uses to save/insert to
    /// </summary>
    /// <returns>The link it uses to save/insert to</returns>
    protected abstract string GetInsertLink();

    public abstract void Start();

    public abstract void Update();

    /// <summary>
    /// Handles saving to the json file
    /// </summary>
    public void Save()
    {
        if (localFile)
        {
            //Creates a new array with generic typing with the size of the list
            var array = new T[entries.Count];

            //Fills the array with the list entries
            for (var index = 0; index < array.Length; index++)
                array[index] = entries[index];

            //Converts the array to json
            var toJson = JsonHelper.ToJson(array, true);

            //Writes the json to the file using the correct path and file name
            var sr = File.CreateText($"{GetPath()}{GetFileName()}");
            sr.WriteLine(toJson);
            sr.Close();
        }
    }

    /// <summary>
    /// Handles inserting a entry to the database online
    /// </summary>
    /// <param name="entry">Handles inserting a entry to the database online</param>
    public virtual void Insert(T entry)
    {
        
    }

    /// <summary>
    /// Handles loading a json file
    /// </summary>
    public void Load(bool localFile = true)
    {
        this.finishedLoading = false;
        this.localFile = localFile;
        if (localFile)
        {
            if (!File.Exists($"{GetPath()}{GetFileName()}")) return;

            //Read the json file
            //And closes the stream reader
            var reader = new StreamReader($"{GetPath()}{GetFileName()}");
            jsonString = reader.ReadToEnd();
            reader.Close();
            FillArray();
        }
        else
            StartCoroutine(StartReadingText());
    }

    /// <summary>
    /// Handles filling the list with entries
    /// </summary>
    void FillArray()
    {
        //Converts the json to an array
        //And fills the list with the array entries
        var array = JsonHelper.FromJson<T>(jsonString);
        
        foreach (var entry in array)
            entries.Add(entry);

        finishedLoading = true;
    }

    private IEnumerator StartReadingText()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(GetLink()))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError) yield break;
            
            jsonString = webRequest.downloadHandler.text;
            
            Debug.Log("jsonString: " + jsonString);
            
            FillArray();
        }
    }
}