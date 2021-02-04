using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraRotation : MonoBehaviour
{
    Camera mainCamera;
    float posX;
    float posY;
    [SerializeField, Range(4, 10)] int rotationSpeed;
    
    void Start()
    {

        mainCamera = Camera.main;
        
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            posX += Input.GetAxis("Mouse X") * rotationSpeed;

            posY += Input.GetAxis("Mouse Y") * rotationSpeed;
            mainCamera.transform.rotation = Quaternion.Euler(-posY, posX, 0);
            //euler zorgt dat je de graden gebruikt bij rotatie
            ChangeCursorState(false);
        }
        else 
        {
            ChangeCursorState(true);
        }
    }

    void ChangeCursorState(bool isVisible)
    {
        if (isVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        
    }
    
}
