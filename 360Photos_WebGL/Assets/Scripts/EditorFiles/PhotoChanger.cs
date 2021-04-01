using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotoChanger : MonoBehaviour
{
    //public List<Texture> photos = new List<Texture>();
    //public GameObject areaParent;
    //public int currentPhoto = 0;

    [SerializeField] private Texture emptyTexture;
    [SerializeField] private Material skyboxMaterial;

    public void SetPhoto(string roomID)
    {
        if (!RenderSettings.skybox) RenderSettings.skybox = skyboxMaterial;
        if (EditorManager.Instance().rooms[roomID].photo)
        {
            RenderSettings.skybox.SetTexture("_MainTex", EditorManager.Instance().rooms[roomID].photo);
        }
        else RenderSettings.skybox.SetTexture("_MainTex", emptyTexture);
    }

    public IEnumerator DownloadPhoto(string filename, Room room)
    {
        if (GameManager.Instance().TextureIsCached(filename))
        {
            room.photo = GameManager.Instance().GetCachedTexture(filename);
            SetPhoto(room.roomID);
        }
        else
        {
            yield return StartCoroutine(Utility.DownloadTexture(Utility.web_url + $"/images/{filename}", (response) =>
            {
                room.photo = response;
                SetPhoto(room.roomID);
                GameManager.Instance().AddCachedTexture(filename, response);
            }));
        }
    }
}
