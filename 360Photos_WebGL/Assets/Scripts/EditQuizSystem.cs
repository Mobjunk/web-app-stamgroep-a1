using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditQuizSystem : WebRequestManager
{
    [SerializeField] private GameObject editQuizPanel;
    [SerializeField] private InputField QuizNameInput;
    [SerializeField] private DropdownMulti classDropdown;
    [SerializeField] private Button doneButton;

    private Class[] classes;
    public static EditQuizSystem instance;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public IEnumerator OpenEditQuizPanel(string id, string quizName, string klassenRaw)
    {

        yield return StartCoroutine(GetRequest($"{Utility.action_url}classes"));
        yield return StartCoroutine(GetRequest($"{Utility.action_url}roles"));

        QuizNameInput.text = quizName;
        List<int> classes = new List<int>();
        foreach (var klas in klassenRaw.Split(':'))
        {
            int klasID = 0;
            if (int.TryParse(klas, out klasID)) classes.Add(klasID);
        }
        classDropdown.value = classes.ToArray();
        doneButton.onClick.RemoveAllListeners();
        doneButton.onClick.AddListener(() => EditQuiz(id));

        if (editQuizPanel) editQuizPanel.SetActive(true);
    }

    public void EditQuiz(string id)
    {
        if (QuizNameInput.text == "") return;

        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("quizName", QuizNameInput.text);
        string classes = "";
        foreach (var value in classDropdown.value)
        {
            classes += value + 1 + ":";
        }
        if(classes.Length > 1) classes = classes.Remove(classes.Length - 1);
        form.AddField("class", classes);

        StartCoroutine(PostRequest($"{Utility.action_url}editQuiz", form));
        if (editQuizPanel) editQuizPanel.SetActive(false);
    }

    public override void FinishedResponse()
    {
        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            Debug.LogError(webError);
            return;
        }

        if (webResponse == "Succesfully edited quiz!")
        {
            QuizList.instance.QuizListRequest();
            return;
        }
        else if (webResponse.Contains("error"))
        {
            Debug.LogError(webResponse);
            return;
        }

        Debug.LogError(webResponse);
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
