using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour {

    private string mQuestion;
    string correctAnswer;
    private List<string> otherAnswers;

    public string MQuestion
    {
        get
        {
            return mQuestion;
        }

        set
        {
            mQuestion = value;
        }
    }

    public List<string> MAnswers
    {
        get
        {
            return mAnswers;
        }

        set
        {
            mAnswers = value;
        }
    }
}
