using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Room(string roomID, Texture photo, GameObject area, List<ButtonSave> buttons)
    {
        this.roomID = roomID;
        this.photo = photo;
        this.area = area;
        this.buttons = buttons;
    }

    public string roomID;
    public Texture photo;
    public GameObject area;
    public List<ButtonSave> buttons;
}
