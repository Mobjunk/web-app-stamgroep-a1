using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    
    public GameObject Areas;
    private Users currentUser;
    Text areaName;
    public Users CurrentUser
    {
        get => currentUser;
        set => currentUser = value;
    }

    private void Awake()
    {
        
        Instantiate(Areas);
        Utility.AddSceneIfNotLoaded("Login Scene");
    }

    public void ChangeAreaText(GameObject newArea)
    {
        string[] newAreaName = newArea.ToString().Split('(', ')');

        areaName.text = newAreaName[1];
    }
}
