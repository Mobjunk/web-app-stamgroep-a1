using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelArrow : ButtonScript
{
    Dropdown arrowLocation;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClick()
    {
        base.OnClick();
        Debug.Log("click");
        arrowLocation = EditorManager.Instance().arrowPopUp.transform.Find("ArrowLocations").GetComponent<Dropdown>();
        arrowLocation.onValueChanged.RemoveAllListeners();
        EditorManager.Instance().arrowPopUp.SetActive(true);
        EditorManager.Instance().arrowPopUp.transform.Find("LocationText").GetComponent<Text>().text = "Je bent bij ruimte nummer: " + buttonSave.room;
        arrowLocation.ClearOptions();
        List<Dropdown.OptionData> dropDownOptions = new List<Dropdown.OptionData>();
        foreach (var key in EditorManager.Instance().rooms.Keys)
        {
            if (buttonSave.room == key) continue;
            dropDownOptions.Add(new Dropdown.OptionData(key));
        }
        arrowLocation.options = dropDownOptions;
        foreach (var option in arrowLocation.options)
        {
            if (option.text == buttonSave.travelRoom)
            {
                arrowLocation.value = arrowLocation.options.IndexOf(option);
                break;
            }
        }
        arrowLocation.onValueChanged.AddListener(SetTravelRoom);
        Button travelButton = EditorManager.Instance().arrowPopUp.transform.Find("TravelButton").GetComponent<Button>();
        travelButton.onClick.RemoveAllListeners();
        travelButton.onClick.AddListener(() => TravelToRoom());
    }

    public override void SetUp(ButtonSave buttonSave)
    {
        base.SetUp(buttonSave);
        foreach (var key in EditorManager.Instance().rooms.Keys)
        {
            if (buttonSave.room == key) continue;
            if (buttonSave.travelRoom == "") buttonSave.travelRoom = key;  
        }
        
    }

    public void SetTravelRoom(int value)
    {
        buttonSave.travelRoom = arrowLocation.options[value].text;
    }

    public void TravelToRoom()
    {
        Debug.Log(buttonSave.travelRoom);
        EditorManager.Instance().areaChanger.SetArea(buttonSave.travelRoom);
        EditorManager.Instance().arrowPopUp.SetActive(false);
    }

    //basis voor alle knoppen is klikken maar bij de travelarrow heb je ook nog pop up + de rest en omdat dit op alleen de arrow komt hoef je niet te checken op welke button je klikt
}
