using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public List<string> otherAnswers = new List<string>();
    public string QuestionText { get; set; }

    public string CorrectAnswer { get; set; }

    //public List<string> OtherAnswers(Question answer) { OtherAnswers.Add(answer); }
}