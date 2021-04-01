using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EditorSaveRoom))]
[RequireComponent(typeof(EditorSaveWorld))]
public class EditorSave : MonoBehaviour
{
    [SerializeField] private GameObject saveScreen;
    private EditorSaveRoom editorSaveRoom;
    private EditorSaveWorld editorSaveWorld;

    public void SaveWorld()
    {
        StartCoroutine(Save());
    }

    private IEnumerator Save()
    {
        saveScreen.SetActive(true);
        editorSaveRoom = GetComponent<EditorSaveRoom>();
        editorSaveWorld = GetComponent<EditorSaveWorld>();

        editorSaveRoom.roomsID.Clear();
        foreach (var room in EditorManager.Instance().rooms)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", room.Key);
            form.AddField("worldID", QuizEditorOpen.worldID);
            form.AddField("image", room.Value.photoName);
            form.AddField("buttonsID", "");
            if(room.Key.StartsWith("0")) StartCoroutine(editorSaveRoom.PostRequest($"{Utility.action_url}create360Room", form));
            else StartCoroutine(editorSaveRoom.PostRequest($"{Utility.action_url}update360Room", form));
        }

        while (editorSaveRoom.roomsID.Count < EditorManager.Instance().rooms.Count)
        {
            yield return null;
        }

        WWWForm form2 = new WWWForm();
        form2.AddField("id", QuizEditorOpen.worldID);
        form2.AddField("ownerID", QuizEditorOpen.quizOwner);
        form2.AddField("accesClassID", QuizEditorOpen.quizAcces);
        string roomsString = "";
        foreach (var rooms in editorSaveRoom.roomsID)
        {
            roomsString += rooms.Value + ":";
        }
        if (roomsString.Length > 2) roomsString = roomsString.Remove(roomsString.Length - 1);

        form2.AddField("roomsID", roomsString);
        form2.AddField("name", QuizEditorOpen.quizName);
        form2.AddField("description", "");
        form2.AddField("quizID", QuizEditorOpen.quizID);
        yield return StartCoroutine(editorSaveWorld.PostRequest($"{Utility.action_url}update360World", form2));

        saveScreen.SetActive(false);
    }
}
