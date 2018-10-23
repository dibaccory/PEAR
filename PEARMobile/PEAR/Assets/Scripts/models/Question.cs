using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public string QuestionText { get; set; }
    public int QuestionNumber { get; set; }
    public List<Answer> answers;

    public Question(string questionText, List<Answer> answers, int questionNumber)
    {
        this.answers = answers;
        QuestionText = questionText;
        this.QuestionNumber = questionNumber;
    }

    public Question()
    {
        answers = new List<Answer>();
        QuestionText = null;
    }
}