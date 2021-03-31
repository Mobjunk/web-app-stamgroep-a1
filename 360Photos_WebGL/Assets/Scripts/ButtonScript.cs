using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private Camera mainCamera;
    PhotoChanger photoChanger;

    public enum ButtonTypes
    {
        MoveToNextArea,
        MoveToPreviousArea,
        ShowInfo,
        ShowTest    
    }
    public ButtonTypes currentType = ButtonTypes.MoveToNextArea;

    private void Start()
    {
       photoChanger = GameObject.FindObjectOfType<PhotoChanger>();
       RaycastCheck.current.interactionTriggered += HasInteracted;
    }

    private void HasInteracted(string name )
    {
        if (this.transform.name == name)
        {
            print(photoChanger);
            switch (currentType)
            {
                case ButtonTypes.MoveToNextArea:
                    //photoChanger.ChangePhoto(true);
                    break;
                case ButtonTypes.MoveToPreviousArea:
                    //photoChanger.ChangePhoto(false);
                    break;
                case ButtonTypes.ShowInfo:
                    break;
                case ButtonTypes.ShowTest:
                    break;
                default:
                    break;
            }
        }
    }
}
