using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    private GameManager _gameManager => GameManager.Instance();
    
    public void LoadUsers()
    {
        Utility.AddSceneIfNotLoaded("AdminPanelScene");
    }

    public void Logout()
    {
        SceneManager.LoadScene("Main scene");
    }

    public void Load360Graden()
    {
        Utility.SwitchScenes("LoggedIn", "360Ruimte");
    }

    public void CloseUserList()
    {
        Utility.UnloadScene("AdminPanelScene");
    }
}
