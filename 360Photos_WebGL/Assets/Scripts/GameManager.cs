using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Users currentUser;
    public Users CurrentUser
    {
        get => currentUser;
        set => currentUser = value;
    }
}
