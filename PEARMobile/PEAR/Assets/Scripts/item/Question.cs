using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour {

    public string QuestionText { get; set; }

    public string CorrectAnswer { get; set; }

    public List<string> OtherAnswers { get; set; }
}
