using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSave
{
    public GameObject gameobject;
    public int room;
    public string question;
    public string[] options;
    public string answer;

    public ButtonSave(GameObject gameobject, int room, string question, string[] options, string answer)
    {
        this.gameobject = gameobject;
        this.room = room;
        this.question = question;
        this.options = options;
        this.answer = answer;
    }
}
