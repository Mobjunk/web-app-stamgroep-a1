using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGetButton : WebRequestManager
{
    ButtonCreator buttonCreator;

    private void Awake()
    {
        buttonCreator = GetComponent<ButtonCreator>();
    }

    public override void FinishedResponse()
    {
        if (webResponse.Contains("button:"))
        {
            string[] data = webResponse.Replace("button: ", "").Split(';');

            int index = 0;
            if (data[2] != "" && data[2] != "0") index = 0;
            else if (data[3] != "") index = 1;
            GameObject newButton = Instantiate(buttonCreator.buttons[index], EditorManager.Instance().rooms[data[4]].area.transform);
            string[] vector3Array = data[1].Split(':');
            Vector3 position = new Vector3(float.Parse(vector3Array[0]), float.Parse(vector3Array[1]), float.Parse(vector3Array[2]));

            newButton.transform.position = position;
            newButton.transform.LookAt(buttonCreator.mainCamera.transform);
            ButtonScript buttonScript = newButton.GetComponent<ButtonScript>();
            ButtonSave buttonSave = EditorManager.Instance().AddExitingButton(data[0], newButton, data[4], "", null, "", data[2], data[3]);
            buttonScript.SetUp(buttonSave);
        }
        else
        {
            Debug.LogError(webResponse);
        }
    }
}
