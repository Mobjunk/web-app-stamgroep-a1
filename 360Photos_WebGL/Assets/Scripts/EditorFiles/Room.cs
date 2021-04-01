using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Room(string roomID, Texture photo, string photoName, GameObject area, List<ButtonSave> buttons)
    {
        this.roomID = roomID;
        this.photo = photo;
        this.photoName = photoName;
        this.area = area;
        this.buttons = buttons;
    }

    public string roomID;
    public Texture photo;
    public string photoName;
    public GameObject area;
    public List<ButtonSave> buttons;
}
