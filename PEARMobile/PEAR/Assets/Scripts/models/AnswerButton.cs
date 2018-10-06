using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{

    public Text answerText;

    private Answer answerData;
    private QandAController controller;

    // Use this for initialization
    void Start()
    {
        controller = FindObjectOfType<QandAController>();
    }

    public void Setup(Answer data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }


    public void HandleClick()
    {
        controller.AnswerButtonClick(answerData);
    }
}