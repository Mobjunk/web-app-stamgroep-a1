using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotoChanger : MonoBehaviour
{
    public List<Material> photos = new List<Material>();
    int currentPhoto = 0;

    public void Start()
    {
        RenderSettings.skybox = photos[0];
    }

    public void ChangePhoto(bool isPositive)
    {
        if (isPositive && currentPhoto < photos.Count -1)
        {
            currentPhoto++;
        }
        else if(!isPositive && currentPhoto > 0)
        {
            currentPhoto--;   
        }
        RenderSettings.skybox = photos[currentPhoto];
    }
}
