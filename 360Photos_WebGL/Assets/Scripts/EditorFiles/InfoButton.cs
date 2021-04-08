using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoButton : ButtonScript
{
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
        EditorManager.Instance().infoPopUp.SetActive(true);
        InputField InfoInput = GameObject.Find("InfoTextInput").GetComponent<InputField>();
        InfoInput.onValueChanged.RemoveAllListeners();
        if (buttonSave.infoText == null) InfoInput.text = "";
        else InfoInput.text = buttonSave.infoText;
        InfoInput.onValueChanged.AddListener(CommitText);
    }

    public void CommitText(string text)
    {
        buttonSave.infoText = text;
    }
}
