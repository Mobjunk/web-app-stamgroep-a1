using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorLoader : WebRequestManager
{
    [SerializeField] private GameObject loadingScreen;
    private string roomsId;
    [SerializeField] private Transform areaParent;


    public override void FinishedResponse()
    {
        
        if (webResponse.Contains("world:"))
        {
            string[] data = webResponse.Replace("world: ", "").Split(',');
            roomsId = data[2];

            //split de string naar array in die wordt gezet in de list
            EditorManager.Instance().roomsId = new List<string>(roomsId.Split(':'));
            foreach (var roomid in EditorManager.Instance().roomsId)
            {
                Debug.Log(roomid);
                GameObject newArea = Instantiate(new GameObject(), areaParent);

                newArea.transform.position = Vector3.zero;
                newArea.name = roomid;
            }
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(true);

        WWWForm form = new WWWForm();
        form.AddField("id", QuizEditorOpen.worldID);
        StartCoroutine(PostRequest($"{Utility.action_url}get360World", form));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
