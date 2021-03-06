using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEditorOpen : WebRequestManager
{
    public static string worldID;
    public static string quizID;
    public static string quizAcces;
    public static string quizName;
    public static string quizOwner;

    public override void FinishedResponse()
    {
        if (!webResponse.Contains("Succesfully"))
        {
            Debug.LogError(webResponse);
            return;
        }
        QuizEditorOpen.worldID = webResponse;
        Utility.UnloadScene("LoggedIn");
        Utility.SwitchScenes("QuizPanelScene", "360Ruimte");
    }

    public void OpenEditor(string quizID, string quizName, string quizOwner, string quizAcces, string worldID)
    {
        QuizEditorOpen.quizID = quizID;
        QuizEditorOpen.quizAcces = quizAcces;
        QuizEditorOpen.quizName = quizName;
        QuizEditorOpen.quizOwner = quizOwner;

        if (worldID.Trim() == "")
        {
            WWWForm form = new WWWForm();
            form.AddField("ownerID", quizOwner);
            form.AddField("accesClassID", quizAcces);
            form.AddField("name", quizName);
            form.AddField("quizID", quizID);
            StartCoroutine(PostRequest($"{Utility.action_url}create360World", form));
        }
        else
        {
            QuizEditorOpen.worldID = worldID;
            Utility.UnloadScene("LoggedIn");
            Utility.SwitchScenes("QuizPanelScene", "360Ruimte");
        }
    }
}
