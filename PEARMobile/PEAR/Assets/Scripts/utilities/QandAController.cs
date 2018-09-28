﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QandAController : MonoBehaviour {

    public GameObjectPool answerButtonObjectPool;
    public Text questionText;
    public Text correctAnswerTextForIncorrectAnswer;
    public Transform answerButtonParent;
    public GameObject questionPanel;
    public GameObject roundEndPanel;
    public GameObject incorrectAnswerPanel;

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

    /* Used for testing purposes only
    private void SetupQuestions()
    {
        // Hard coded questions for testing
        List<Answer> questionAnswers = new List<Answer>
        {
            new Answer("This is a wrong answer", false),
            new Answer("This is another wrong answer", false),
            new Answer("This is a third wrong answer", false),
            new Answer("This is a correct answer", true)
        };

        questionPool.Add(new Question("This is a question you must answer", questionAnswers));
        questionPool.Add(new Question("This is another question for you", questionAnswers));
        questionPool.Add(new Question("And one more. Answer correctly", questionAnswers));
    }
    */

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        Question currentQuestion = questionPool[questionIndex];
        SetCorrectAnswerText(currentQuestion);
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
        if(isCorrect)
        {
            Debug.Log("Correct answer clicked");
        }
        else
        {
            IncorrectAnswerClicked();
        }

        if(questionPool.Count > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }

    public void ContinueButtonClick()
    {
        incorrectAnswerPanel.SetActive(false);
        questionText.enabled = true;
        questionPanel.SetActive(true);
    }

    private void SetCorrectAnswerText(Question currentQ)
    {
        List<Answer> answers = currentQ.answers;
        foreach(Answer ans in answers)
        {
            if(ans.isCorrect)
            {
                correctAnswerTextForIncorrectAnswer.text = ans.answerText;
                return;
            }
        }


    }

    private void IncorrectAnswerClicked()
    {
        Debug.Log("Incorrect answer clicked");
        questionPanel.SetActive(false);
        questionText.enabled = false;
        incorrectAnswerPanel.SetActive(true);


    }

    public void EndRound()
    {
        isRoundActive = false;
        questionPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        questionText.enabled = false;
        roundEndPanel.SetActive(true);

    }

    // Update is called once per frame
    void Update ()
    {
               		
	}
}
