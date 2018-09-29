using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public string QuestionText { get; set; }
    public List<Answer> answers;

    public Question(string questionText, List<Answer> answers)
    {
        this.answers = answers;
        QuestionText = questionText;
    }

    public Question()
    {
        answers = new List<Answer>();
        QuestionText = null;
    }
}