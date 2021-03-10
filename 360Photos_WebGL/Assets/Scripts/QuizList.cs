using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizList : WebRequestManager
{
    [SerializeField] private InputField searchBar;
    [SerializeField] private int page = 0;
    [SerializeField] private int numberOfQuizes = 30;
    [SerializeField] private Transform quizParent;
    [SerializeField] private GameObject quizDisplay;

    public static QuizList instance;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        QuizListRequest();
    }

    public void QuizListRequest()
    {
        webResponse = string.Empty;
        webError = string.Empty;

        string searchVector = "";
        if(searchBar) searchVector = searchBar.text;

        //TODO: Add a check for length for the password and maybe username

        WWWForm form = new WWWForm();
        form.AddField("searchVector", searchVector);
        form.AddField("numberOfUsers", numberOfQuizes);
        form.AddField("page", page);
        form.AddField("ownerID", gameManager.CurrentUser.id);

        StartCoroutine(PostRequest($"{Utility.web_url}index.php?action=quizes", form));
    }

    public override void FinishedResponse()
    {
        //Check if the user is logged in
        if (gameManager.CurrentUser == null)
        {
            Debug.LogError("Trying to get quizes while not logged in!");
            return;
        }

        //Handles checking if the web error isnt empty
        if (!webError.Equals(string.Empty))
        {
            Debug.LogError($"Web error: {webError}");
            return;
        }

        //Checks if the web response contained unsuccessful
        if (webResponse.Contains("Unsuccessful"))
        {
            Debug.LogError(webResponse);
            return;
        }

        Debug.Log("webResponse: " + webResponse);

        string[] users = webResponse.Split(';');

        foreach (Transform child in quizParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var user in users)
        {
            if (user != "")
            {
                string[] quizInfo = user.Replace("Quiz: ", "").Split(',');

                //Users userClass = new Users(int.Parse(quizInfo[0]), quizInfo[1], quizInfo[2], quizInfo[3], quizInfo[4], quizInfo[6], quizInfo[5]);
                GameObject placedQuizDisplay = Instantiate(quizDisplay, quizParent);

                placedQuizDisplay.name = "User ID#" + quizInfo[0];
                Transform information = placedQuizDisplay.transform.GetChild(0);
                information.Find("Name").GetComponent<Text>().text = quizInfo[1];
                string[] classesID = quizInfo[5].Split(':');

                string classes = "";
                foreach (var item in classesID)
                {
                    if (item.Trim() == "") continue;
                    classes += ClassManager.instance.GetClassByID(item).CLASS_NAME;
                    classes += ", ";
                }
                if (classes.Length > 2) classes = classes.Remove(classes.Length - 2, 2);
                information.Find("AccesClasses").GetComponent<Text>().text = classes;

                Transform buttons = placedQuizDisplay.transform.GetChild(1);
                buttons.Find("Delete").GetComponent<Button>().onClick.AddListener(() => RemoveQuizSystem.instance.RemoveQuiz(quizInfo[0]));

                buttons.Find("Edit").GetComponent<Button>().onClick.AddListener(() => StartCoroutine(EditQuizSystem.instance.OpenEditQuizPanel(quizInfo[0], quizInfo[1], quizInfo[5])));
            }
        }
    }
}
