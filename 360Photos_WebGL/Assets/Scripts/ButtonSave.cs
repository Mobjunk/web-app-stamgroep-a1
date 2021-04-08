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
    public string travelRoom;
    public string id;
    public string infoText;

                                             //room id    vraag         mogelijke antwoorden  correct antwoord
    public ButtonSave(string id, GameObject gameobject, string room, string question, string[] options, string answer, string travelRoom, string infoText)
    {
        this.gameobject = gameobject;
        this.room = room;
        this.question = question;
        this.options = options;
        this.answer = answer;
        this.travelRoom = travelRoom;
        this.id = id;
        this.infoText = infoText;

        //alle gameobjects die je maakt moeten opgeslagen worden
        //key wordt welke room je zit
    }
}
