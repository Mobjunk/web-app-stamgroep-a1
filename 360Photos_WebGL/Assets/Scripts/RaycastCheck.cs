using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RaycastCheck : MonoBehaviour
{
    Camera mainCamera;
    RaycastHit hit;

    /// <summary>
    /// Usage: sets up the Event for triggering an action (Can be given an int value to specify the button pressed)
    /// </summary>
    public static RaycastCheck current;
    public event Action<string> interactionTriggered;
    [HideInInspector] public bool isEditor;
    public GameObject selectedButton = null;
    public Button deleteButton;

    private void Awake()
    {
        current = this;
        mainCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                
                Debug.DrawRay(ray.origin, (mainCamera.transform.rotation * Vector3.forward) * hit.distance, Color.white);

                if (hit.transform.TryGetComponent<ButtonScript>(out ButtonScript buttonScript))
                {
                    Debug.Log("komt hier wel");
                    selectedButton = hit.transform.gameObject;
                    deleteButton.gameObject.SetActive(true);
                    //de delete button moet in de pop up komen
                    buttonScript.OnClick();
                }
                else if (hit.transform.GetComponent<ButtonScript>() != null && !hit.transform.CompareTag("Preview") && isEditor)
                {
                    if (selectedButton != null)
                    {
                        //maakt vorige geklikte wit
                        selectedButton.GetComponent<MeshRenderer>().material.color = Color.white;
                        print("make white");
                    }
                    
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, (mainCamera.transform.rotation * Vector3.forward) * 1000, Color.red);

                if (selectedButton != null)
                {
                    selectedButton.GetComponent<MeshRenderer>().material.color = Color.white;
                    print("deselect");
                    //deleteButton.gameObject.SetActive(false);
                }
                
            }
        }
    }
}