using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccessManager : MonoBehaviour
{
    private GameManager _gameManager => GameManager.Instance();

    [Header("All UI elements that need to be unhidden for teachers")]
    [SerializeField] private GameObject[] teacherAcces;
    [Header("All UI elements that need to be unhidden for admins")]
    [SerializeField] private GameObject[] administartorAcces;

    private void Start()
    {
        if (_gameManager.CurrentUser == null)
            Utility.SwitchScenes(SceneManager.GetActiveScene().name, "Login scene");
        else
        {
            //Debug.Log($"teacher: {_gameManager.CurrentUser.isTeacher()}, admin: {_gameManager.CurrentUser.IsAdmin()}");
            if (_gameManager.CurrentUser.isTeacher())
                foreach(GameObject gObject in teacherAcces)
                    gObject.SetActive(true);

            if (_gameManager.CurrentUser.IsAdmin())
                foreach(GameObject gObject in administartorAcces)
                    gObject.SetActive(true);
        }
    }
}
