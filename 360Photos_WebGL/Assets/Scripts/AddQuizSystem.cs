using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddQuizSystem : WebRequestManager
{
    [SerializeField] private GameObject addQuizPanel;
    [SerializeField] private InputField QuizNameInput;
    [SerializeField] private DropdownMulti classDropdown;
    [SerializeField] private Button doneButton;

    private Class[] classes;

    public void OpenAddQuizPanel()
    {
        StartCoroutine(OpenAddQuizPanelEnum());
    }

    public IEnumerator OpenAddQuizPanelEnum()
    {
        yield return StartCoroutine(GetRequest($"{Utility.action_url}classes"));

        QuizNameInput.text = "";
        classDropdown.value = new int[0];
        doneButton.onClick.RemoveAllListeners();
        doneButton.onClick.AddListener(() => CreateQuiz());

        if (addQuizPanel) addQuizPanel.SetActive(true);
    }

    public void CreateQuiz()
    {
        bool fieldEmpty = false;

        if (QuizNameInput.text.Trim() == "")
        {
            QuizNameInput.placeholder.color = Color.red;
            QuizNameInput.placeholder.GetComponent<Text>().text = "Dit veld is verplicht!";
            fieldEmpty = true;
        }

        if (fieldEmpty == true) return;

        WWWForm form = new WWWForm();
        form.AddField("quizName", QuizNameInput.text);

        string classes = "";
        foreach (var value in classDropdown.value)
        {
            classes += value + 1 + ":";
        }
        if(classes.Length > 1) classes = classes.Remove(classes.Length - 1);
        form.AddField("class", classes);
        form.AddField("ownerID", gameManager.CurrentUser.id);

        StartCoroutine(PostRequest($"{Utility.action_url}createQuiz", form));
        if (addQuizPanel) addQuizPanel.SetActive(false);
    }

    public override void FinishedResponse()
    {
        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            Debug.LogError(webError);
            return;
        }

        if (webResponse == "Succesfully added a new quiz!")
        {
            QuizList.instance.QuizListRequest();
            return;
        }

        classes = JsonHelper.FromJson<Class>(webResponse);

        List<DropdownMulti.OptionData> classOptions = new List<DropdownMulti.OptionData>();
        foreach (var klas in classes)
        {
            if (klas.CLASS_NAME != null)
            {
                classOptions.Add(new DropdownMulti.OptionData(klas.CLASS_NAME));
            }
        }
        if (classOptions.Count > 1)
        {
            classDropdown.options = classOptions;
        }
    }
}
