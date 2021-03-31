using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EditorGetRoom))]
[RequireComponent(typeof(EditorGetWorld))]
public class EditorLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    public Transform areaParent;

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
        editorGetRoom.editorLoader = this;

        foreach (Transform child in areaParent)
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
        loadingScreen.SetActive(false);
    }
}
