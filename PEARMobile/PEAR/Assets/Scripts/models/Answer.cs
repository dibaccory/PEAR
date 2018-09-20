using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answer
{
    public string answerText;
    public bool isCorrect;

    public Answer(string answerText, bool isCorrect)
    {
        this.answerText = answerText;
        this.isCorrect = isCorrect;
    }
}
