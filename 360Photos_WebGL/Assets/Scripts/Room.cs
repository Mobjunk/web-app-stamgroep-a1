using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Room(Texture photo, GameObject area, List<ButtonSave> buttons)
    {
        this.photo = photo;
        this.area = area;
        this.buttons = buttons;
    }
    public Texture photo;
    public GameObject area;
    public List<ButtonSave> buttons;
}
