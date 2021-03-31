using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSave
{
    public GameObject gameobject;
    public string room;
    public string question;
    public string[] options;
    public string answer;
                                             //room id    vraag         mogelijke antwoorden  correct antwoord
    public ButtonSave(GameObject gameobject, string room, string question, string[] options, string answer)
    {
        this.gameobject = gameobject;
        this.room = room;
        this.question = question;
        this.options = options;
        this.answer = answer;

        //alle gameobjects die je maakt moeten opgeslagen worden
        //key wordt welke room je zit
    }
}
