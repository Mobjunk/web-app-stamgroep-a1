using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonScript : MonoBehaviour
{
    public ButtonSave buttonSave;

    //private Camera mainCamera;
    //PhotoChanger photoChanger;

    //public enum ButtonTypes
    //{
    //    MoveToNextArea,
    //    MoveToPreviousArea,
    //    ShowInfo,
    //    ShowTest    
    //}
    //public ButtonTypes currentType = ButtonTypes.MoveToNextArea;

    //private void Start()
    //{
    //   photoChanger = GameObject.FindObjectOfType<PhotoChanger>();
    //   RaycastCheck.current.interactionTriggered += HasInteracted;
    //}

    //private void HasInteracted(string name )
    //{
    //    if (this.transform.name == name)
    //    {
    //        print(photoChanger);
    //        switch (currentType)
    //        {
    //            case ButtonTypes.MoveToNextArea:
    //                //photoChanger.ChangePhoto(true);
    //                break;
    //            case ButtonTypes.MoveToPreviousArea:
    //                //photoChanger.ChangePhoto(false);
    //                break;
    //            case ButtonTypes.ShowInfo:
    //                break;
    //            case ButtonTypes.ShowTest:
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}



    //abstract maakt de functie een verplichting in elke inherit van dit script maar wordt niet in de parent gemaakt
    //public abstract void OnClick();

    //de originele virtual is een basis die wordt uitgevoerd die in je in childs kunt aanpassen

    public virtual void OnClick()
    {
        Debug.Log("test");
        GetComponent<MeshRenderer>().material.color = Color.blue;
        
        
        //basis voor pop up en in child modifyen
    }

    public virtual void SetUp(ButtonSave buttonSave)
    {
        this.buttonSave = buttonSave;
    }

}
