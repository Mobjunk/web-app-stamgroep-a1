using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotoChanger : MonoBehaviour
{
    public List<Material> photos = new List<Material>();
    public GameObject areaParent;
    public int currentPhoto = 0;

    public void Start()
    {
        for (int i = 0; i < areaParent.transform.childCount; i++)
        {
            if (i == 0)
            {
                areaParent.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                areaParent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        RenderSettings.skybox = photos[0];
    }

    public void ChangePhoto(bool isPositive)
    {
        areaParent.transform.GetChild(currentPhoto).gameObject.SetActive(false);
        if (isPositive && currentPhoto < photos.Count -1)
        {
            currentPhoto++;
        }
        else if(!isPositive && currentPhoto > 0)
        {
            currentPhoto--;   
        }
        areaParent.transform.GetChild(currentPhoto).gameObject.SetActive(true);
        RenderSettings.skybox = photos[currentPhoto];
    }
}
