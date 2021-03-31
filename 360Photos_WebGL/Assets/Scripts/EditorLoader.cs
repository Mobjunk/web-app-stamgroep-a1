using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EditorGetRoom))]
[RequireComponent(typeof(EditorGetWorld))]
public class EditorLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    private string roomsId;
    [SerializeField] private Transform areaParent;

    private EditorGetWorld editorGetWorld;
    private EditorGetRoom editorGetRoom;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadEditor());
    }

    private IEnumerator LoadEditor()
    {
        loadingScreen.SetActive(true);

        editorGetRoom = GetComponent<EditorGetRoom>();
        editorGetWorld = GetComponent<EditorGetWorld>();

        WWWForm form = new WWWForm();
        form.AddField("id", QuizEditorOpen.worldID);
        yield return StartCoroutine(editorGetWorld.PostRequest($"{Utility.action_url}get360World", form));

        roomsId = editorGetWorld.data[2];

        //split de string naar array in die wordt gezet in de list
        //foreach (var roomid in EditorManager.Instance().roomsId)
        //{
        //    Debug.Log(roomid);
        //    GameObject newArea = Instantiate(new GameObject(), areaParent);

        //    newArea.transform.position = Vector3.zero;
        //    newArea.name = roomid;
        //}
    }
}
