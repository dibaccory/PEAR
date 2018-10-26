using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class QandAController : MonoBehaviour {

    public GameObjectPool answerButtonObjectPool;
    public Text questionText;
    public Text correctAnswerTextBox;
    public Transform answerButtonParent;
    public GameObject questionPanel;
    public GameObject roundEndPanel;
    public GameObject roundFailedPanel;
    public GameObject questionAnsweredPanel;

    private List<Question> questionPool = new List<Question>();
    private int questionIndex;
    private bool isRoundActive = false;
    private int totalNumQuestions;
    private int numberCorrectlyAnswered;

    private FirebaseUser firebaseUser;

    private IDictionary<Question, Answer> userAnswers = new Dictionary<Question, Answer>();

    private float secondCount;

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private Item currentItem;
    
	// Use this for initialization
	void Start ()
    {
        // Values hardcoded for testing right now
        // TODO: Do this dynamically 
        // string classCode = "astronomy";
        string classCode = FindObjectOfType<SceneController>().classroom;
        // string moduleName = "solar system";
        string moduleName = FindObjectOfType<SceneController>().module;
        string item = FindObjectOfType<ItemSceneManager>().currentItem.name;
        string buildOrCollect = "build";
        DatabaseManager.sharedInstance.getQnA(classCode, moduleName, item, buildOrCollect, (result) =>
        {
            questionPool = result;
            totalNumQuestions = questionPool.Count;
            numberCorrectlyAnswered = 0;
            questionIndex = 0;
            ShowQuestion();
            isRoundActive = true;
            firebaseUser = DatabaseManager.sharedInstance.GetUser();
            DatabaseManager.sharedInstance.StoreAttempts("sqG05GXsh7TnGTiby9uMlDAkFz72",
                                                         FindObjectOfType<SceneController>().classroom,
                                                         FindObjectOfType<SceneController>().module,
                                                         FindObjectOfType<ItemSceneManager>().currentItem.name,
                                                         "collect");
        });
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        Question currentQuestion = questionPool[questionIndex];
        questionText.text = currentQuestion.QuestionText;
        
        for(int i = 0; i < currentQuestion.answers.Count; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent, false);
            answerButtonGameObjects.Add(answerButtonGameObject);
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(currentQuestion.answers[i]);
        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClick(Answer answer)
    {
        string message = "";
        string correctAnswer = GetCorrectAnswerText(questionPool[questionIndex]);
        // var uid = firebaseUser.UserId;

        string questionString = "question" + questionPool[questionIndex].QuestionNumber.ToString();

        DatabaseManager.sharedInstance.SubmitAnswer("sqG05GXsh7TnGTiby9uMlDAkFz72", 
                                                    FindObjectOfType<SceneController>().classroom, 
                                                    FindObjectOfType<SceneController>().module, 
                                                    FindObjectOfType<ItemSceneManager>().currentItem.name,
                                                    "collect", 
                                                    questionString, 
                                                    answer.answerText);

        if(answer.isCorrect)
        {
            Debug.Log("Correct answer clicked");
            message = "You're right! The correct answer was "
                            + correctAnswer;
            numberCorrectlyAnswered++;
        }
        else
        {
            Debug.Log("Incorrect answer clicked");

            message = "Sorry, the correct answer was "  + correctAnswer;
            Debug.Log("User selected " + answer.answerText + " instead of " + correctAnswer);
        }
        userAnswers.Add(questionPool[questionIndex], answer);
        correctAnswerTextBox.text = message;
        questionPanel.SetActive(false);
        questionText.enabled = false;
        questionAnsweredPanel.SetActive(true);
    }

    public void ContinueButtonClick()
    {
        if (questionPool.Count > questionIndex + 1)
        {
            questionAnsweredPanel.SetActive(false);
            questionText.enabled = true;
            questionPanel.SetActive(true);
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }

    private string GetCorrectAnswerText(Question currentQ)
    {
        List<Answer> answers = currentQ.answers;
        foreach(Answer ans in answers)
        {
            if(ans.isCorrect)
            {
                return ans.answerText;
            }
        }
        return "";
    }

    public void EndRound()
    {
        isRoundActive = false;
        questionPanel.SetActive(false);
        questionAnsweredPanel.SetActive(false);
        questionText.enabled = false;

        foreach(var item in userAnswers)
        {
            Debug.Log("User answered " + item.Value.answerText + " for the following question: ");
            Debug.Log("Question Text: " + item.Key.QuestionText);
        }
        float percentCorrect = (float)numberCorrectlyAnswered / (float)totalNumQuestions;
        if (percentCorrect > .50)
        {
            roundEndPanel.SetActive(true);
        }
        else
        {
            roundFailedPanel.SetActive(true);
        }

        Debug.Log("Total time spent on this item: " + secondCount);
        Debug.Log("User answered " + percentCorrect * 100 + "% correctly");
        //DatabaseManager.sharedInstance.TimeAndAttempts("sqG05GXsh7TnGTiby9uMlDAkFz72",
        //                                               "astronomy",
        //                                               "solar system",
        //                                               "earth",
        //                                               "build",
        //                                               (double)secondCount,
        //                                               1);
        DatabaseManager.sharedInstance.StoreTime("sqG05GXsh7TnGTiby9uMlDAkFz72",
                                                 FindObjectOfType<SceneController>().classroom,
                                                 FindObjectOfType<SceneController>().module,
                                                 FindObjectOfType<ItemSceneManager>().currentItem.name,
                                                 "collect",
                                                 secondCount);
    }

    // Update is called once per frame
    void Update ()
    {
        secondCount += Time.deltaTime;
	}
}
