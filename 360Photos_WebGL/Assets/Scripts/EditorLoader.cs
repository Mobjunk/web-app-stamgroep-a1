using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(EditorGetRoom))]
[RequireComponent(typeof(EditorGetWorld))]
public class EditorLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;

    private EditorGetWorld editorGetWorld;
    private EditorGetRoom editorGetRoom;

    private List<Coroutine> coroutines = new List<Coroutine>();

    // Start is called before the first frame update
    void Start()
    {
        if(QuizEditorOpen.worldID != "" && QuizEditorOpen.worldID != null) StartCoroutine(LoadEditor());
    }

    private IEnumerator LoadEditor()
    {
        loadingScreen.SetActive(true);

        editorGetRoom = GetComponent<EditorGetRoom>();
        editorGetWorld = GetComponent<EditorGetWorld>();

        foreach (Transform child in EditorManager.Instance().areaParent)
        {
            Destroy(child.gameObject);
        }

        WWWForm form = new WWWForm();
        form.AddField("id", QuizEditorOpen.worldID);
        yield return StartCoroutine(editorGetWorld.PostRequest($"{Utility.action_url}get360World", form));

        string[] roomsID = editorGetWorld.data[2].Split(':');

        foreach (var roomid in roomsID)
        {
            form = new WWWForm();
            form.AddField("id", roomid);
            StartCoroutine(editorGetRoom.PostRequest($"{Utility.action_url}get360Room", form));
        }

        while (EditorManager.Instance().rooms.Count < roomsID.Length)
        {
            Debug.LogError(EditorManager.Instance().rooms.Count + " van de " + roomsID.Length + " room loaded.");
            yield return null;
        }
        Debug.LogError(EditorManager.Instance().rooms.Count + " van de " + roomsID.Length + " room loaded.");

        string firstRoomID = "";
        foreach (string key in EditorManager.Instance().rooms.Keys)
        {
            if (firstRoomID == "") firstRoomID = key;
            else if (int.Parse(key) < int.Parse(firstRoomID)) firstRoomID = key;
        }
        EditorManager.Instance().photoChanger.SetPhoto(firstRoomID);
        EditorManager.Instance().areaChanger.SetArea(firstRoomID);
        loadingScreen.SetActive(false);
    }
}
