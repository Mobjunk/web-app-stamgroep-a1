using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EditorSaveRoom))]
[RequireComponent(typeof(EditorSaveWorld))]
[RequireComponent(typeof(EditorSaveButton))]
public class EditorSave : MonoBehaviour
{
    [SerializeField] private GameObject saveScreen;
    private EditorSaveRoom editorSaveRoom;
    private EditorSaveWorld editorSaveWorld;
    private EditorSaveButton editorSaveButton;

    public void SaveWorld()
    {
        StartCoroutine(Save());
    }

    private IEnumerator Save()
    {
        saveScreen.SetActive(true);
        editorSaveRoom = GetComponent<EditorSaveRoom>();
        editorSaveWorld = GetComponent<EditorSaveWorld>();
        editorSaveButton = GetComponent<EditorSaveButton>();

        editorSaveRoom.roomsID.Clear();
        foreach (var room in EditorManager.Instance().rooms)
        {
            editorSaveButton.currentRoom = room.Key;
            editorSaveButton.buttonsID.Clear();

            foreach (var button in room.Value.buttons)
            {
                WWWForm form3 = new WWWForm();
                form3.AddField("id", button.Value.id);
                form3.AddField("navRoom", button.Value.travelRoom);
                Vector3 buttonPosition = button.Value.gameobject.transform.position;
                string stringPosition = buttonPosition.x + ":" + buttonPosition.y + ":" + buttonPosition.z;
                form3.AddField("position", stringPosition);
                form3.AddField("text", button.Value.infoText);
                if (button.Value.id.StartsWith("0")) StartCoroutine(editorSaveRoom.PostRequest($"{Utility.action_url}create360Room", form3));
                else StartCoroutine(editorSaveRoom.PostRequest($"{Utility.action_url}update360Room", form3));
            }

            while (editorSaveButton.buttonsID.Count < EditorManager.Instance().rooms[room.Key].buttons.Count)
            {
                yield return null;
            }

            WWWForm form = new WWWForm();
            form.AddField("id", room.Key);
            form.AddField("worldID", QuizEditorOpen.worldID);
            form.AddField("image", room.Value.photoName);
            string buttonsID = "";
            foreach (var buttonID in editorSaveButton.buttonsID)
            {
                buttonsID += buttonID.Value + ":";
            }
            if (buttonsID.Length > 2) buttonsID = buttonsID.Remove(buttonsID.Length - 1);

            form.AddField("buttonsID", buttonsID);
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
