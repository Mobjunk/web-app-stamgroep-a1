using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    private GameManager _gameManager => GameManager.Instance();
    
    public void LoadUsers()
    {
        Utility.AddSceneIfNotLoaded("Test");
    }

    public void Logout()
    {
        SceneManager.LoadScene("Main scene");
    }
    
}
