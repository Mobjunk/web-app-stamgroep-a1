using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCreator : MonoBehaviour
{
    [SerializeField] bool isEditor;
    List<GameObject> allAreas = new List<GameObject>();
    public List<Texture> photos;
    private Canvas editUi;
    private Dropdown selector;
    public GameObject[] buttons;
    public Transform areaParent;
    public Transform currentArea;
    public Camera mainCamera;
    public float createDistance = 20;
    //nummer van area die aanstaat
    public int currentSelected = 0;
    public int currentPhoto;
    
    GameManager gameManager;
    PhotoChanger photoChanger;
    GameObject previewButton = null;

    void Start()
    {
        editUi = GameObject.Find("Canvas").GetComponent<Canvas>();

        if (isEditor == false)
        { 
            editUi.gameObject.SetActive(false);
        }

        selector = GameObject.Find("ButtonSelector").GetComponent<Dropdown>();
        photoChanger = GameObject.FindObjectOfType<PhotoChanger>().GetComponent<PhotoChanger>();
        photos = photoChanger.photos;
        GameObject.FindObjectOfType<RaycastCheck>().GetComponent<RaycastCheck>().isEditor = isEditor;
        areaParent = photoChanger.areaParent.transform;

        if ((areaParent = GameObject.FindGameObjectWithTag("AreaParent").transform) == null)
        {
            currentPhoto = 1;
            GameObject newAreaParent = Instantiate(new GameObject());
            newAreaParent.transform.position = Vector3.zero;
            newAreaParent.transform.tag = "AreaParent";
            newAreaParent.transform.name = "Areas";
            allAreas.Add(newAreaParent);
            currentArea = allAreas[0].transform;
        }
        else
        {
            for (int i = 0; i < areaParent.childCount; i++)
            {
                allAreas.Add(areaParent.GetChild(i).gameObject);
                if (areaParent.GetChild(i).gameObject.activeInHierarchy)
                {
                    currentArea = areaParent.GetChild(i);
                    currentPhoto = i;
                }
            }
        }
    }

    public void CreateObject()
    {
        if (selector.value > 0 )
        {
            GameObject newButton = Instantiate(buttons[selector.value - 1], FindCurrentArea().transform);
            newButton.transform.position = mainCamera.transform.rotation * new Vector3(0, 0, createDistance + mainCamera.transform.position.z);
            newButton.transform.LookAt(mainCamera.transform);
            //EditorManager.Instance().AddButton(newButton, )
        }
    }

    public void CreatePreview()
    {
        if (selector.value > 0 )
        {
            print((buttons[selector.value - 1]));
            if (previewButton != null)
            {
                Destroy(previewButton);
            }

            previewButton = Instantiate(buttons[selector.value - 1], mainCamera.transform);
            previewButton.tag = "Preview";
            previewButton.transform.position = mainCamera.transform.rotation * new Vector3(0, 0, mainCamera.transform.position.z + createDistance);
            previewButton.transform.localScale = new Vector3(11, 11, 1);
            previewButton.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            Destroy(previewButton);
        }
    }

    public void CreateNewArea()
    {
            GameObject newArea = Instantiate(new GameObject(), areaParent);

            newArea.transform.position = Vector3.zero;
            newArea.name = $"Area {allAreas.Count + 1}(name)";
            currentArea.gameObject.SetActive(false);

            if (allAreas.Count > 0)
            {
                allAreas[allAreas.Count - 1].SetActive(false);
            }

            allAreas.Add(newArea);
            currentSelected = allAreas.Count;
            currentArea = newArea.transform;
    }

    public void ChangeArea(bool isPositive)
    {
        currentArea.gameObject.SetActive(false);

        if (isPositive && currentSelected < allAreas.Count)
        {
            currentSelected++;
        }
        else if (!isPositive && currentSelected > 1)
        {
            currentSelected--;
        }

        currentArea = allAreas[currentSelected - 1].transform;
        currentArea.gameObject.SetActive(true);

        FindCurrentArea();
        if (currentPhoto < photos.Count)
        {
          
            RenderSettings.skybox.SetTexture("_MainTex", photos[currentPhoto]);
        }
    }

    GameObject FindCurrentArea()
    {
        for (int i = 0; i < allAreas.Count; i++)
        {
                if (allAreas[i].gameObject.activeInHierarchy)
                {
                    currentPhoto = i;
                    GameObject thisArea = allAreas[i];
                    return thisArea;
                }
        }
        return null;
    }

    public void DeleteButton()
    {
        Destroy(GameObject.FindObjectOfType<RaycastCheck>().GetComponent<RaycastCheck>().selectedButton);
    }
    //kunnen deselecteren(done)
    //delete button alleen laten verschijnen als je ook op een item klikt(done)
    //maar 1 item kunnen selecteren(done)
    //zorg dat je op buttons kunt klikken zonder dat die de selection uitzet(werkt op magische wijze wel weer)
}
