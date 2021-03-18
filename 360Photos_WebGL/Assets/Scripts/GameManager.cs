using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    
    private Users currentUser;
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
