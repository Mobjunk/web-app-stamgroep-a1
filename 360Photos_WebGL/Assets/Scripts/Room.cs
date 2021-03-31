using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Room(Texture photo, List<ButtonSave> buttons)
    {
        this.photo = photo;
        this.buttons = buttons;
    }
    public Texture photo;
    public List<ButtonSave> buttons;
}
