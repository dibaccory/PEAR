using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QandAController : MonoBehaviour {

    public GameObjectPool answerButtonObjectPool;
    public Text questionText;
    public Text correctAnswerTextBox;
    public Transform answerButtonParent;
    public GameObject questionPanel;
    public GameObject roundEndPanel;
    public GameObject questionAnsweredPanel;

    private List<Question> questionPool = new List<Question>();
    private int questionIndex;
    private bool isRoundActive = false;

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    
	// Use this for initialization
	void Start ()
    {
        // Values hardcoded for testing right now
        // TODO: Do this dynamically 
        string classCode = "astronomy";
        string moduleName = "solar system";
        string item = "earth";
        string buildOrCollect = "build";
        DatabaseManager.sharedInstance.getQnA(classCode, moduleName, item, buildOrCollect, (result) =>
        {
            questionPool = result;
            questionIndex = 0;
            ShowQuestion();
            isRoundActive = true;
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

    public void AnswerButtonClick(bool isCorrect)
    {
        string message = "";
        string correctAnswer = GetCorrectAnswerText(questionPool[questionIndex]);
        Debug.Log(correctAnswer);
        if(isCorrect)
        {
            Debug.Log("Correct answer clicked");
            message = "You're right! The correct answer was "
                            + correctAnswer;
        }
        else
        {
            Debug.Log("Incorrect answer clicked");

            message = "Sorry, the correct answer was "
                            + correctAnswer;
        }
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
        roundEndPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update ()
    {
               		
	}
}
