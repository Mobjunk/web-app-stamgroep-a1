using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveQuizSystem : WebRequestManager
{
    public static RemoveQuizSystem instance;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public void RemoveQuiz(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        StartCoroutine(PostRequest($"{Utility.action_url}deleteQuiz", form));
    }

    public override void FinishedResponse()
    {
        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            Debug.LogError(webError);
            return;
        }
        else
        {
            QuizList.instance.QuizListRequest();
        }
    }
}
