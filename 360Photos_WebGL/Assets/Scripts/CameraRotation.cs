using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraRotation : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField, Range(5, 20)] int rotationSpeed;
    float posX;
    float posY;
 
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            posX += Input.GetAxis("Mouse X") * rotationSpeed;
            posY += Input.GetAxis("Mouse Y") * rotationSpeed;
            posY = Mathf.Clamp(posY, -80f, 80f);
            //clamp zorgt dat de positie tussen de 2 meegegeven getallen blijft
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
