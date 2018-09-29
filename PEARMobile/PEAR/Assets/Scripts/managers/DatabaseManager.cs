﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Threading.Tasks;
using Firebase.Auth;


public class DatabaseManager : MonoBehaviour
{

    public static DatabaseManager sharedInstance = null;

    /// <summary>
    /// Awake this instance and initialize sharedInstance for Singleton pattern
    /// </summary>
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pear-f60a2.firebaseio.com/");

        //Debug.Log(Router.Users());

    }

    public void CreateNewUser(User user, string uid, Classroom classroom)
    {
        string userJSON = JsonUtility.ToJson(user);
        Router.UserWithUID(uid).SetRawJsonValueAsync(userJSON);
        string classJSON = JsonUtility.ToJson(classroom);
        Router.UserWithClass(uid, classroom.classCode).SetRawJsonValueAsync(classJSON);
        Router.ClassWithUser(uid, classroom.classCode).SetValueAsync(user.email);


        //Router.UserWithClass(uid, classroom.classCode).SetValueAsync(classJSON);

        //Router.ClassWithUser(uid, classroom.classCode, user.name).SetValueAsync("true");

    }

    public void AddClass(string classCode, Classroom classroom, FirebaseUser user)
    {
        string userJSON = JsonUtility.ToJson(user);
        string classJSON = JsonUtility.ToJson(classroom);

        Router.UserWithClass(user.UserId, classroom.classCode).SetRawJsonValueAsync(classJSON);
        Router.ClassWithUser(user.UserId, classroom.classCode).Child("email").SetValueAsync(user.Email);

    }

    public void GetClasses(string uid, Action<List<Classroom>>completionBlock)
    {
        List<Classroom> tempList = new List<Classroom>();

        Router.UsersClasses(uid).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot classesSnapshot = task.Result;

            foreach (DataSnapshot classnode in classesSnapshot.Children)
            {
                var classDict = (IDictionary<string, object>)classnode.Value;
                Classroom newClassroom = new Classroom(classDict);
                tempList.Add(newClassroom);
            }
            completionBlock(tempList);
        });
    }

    public void getQnA(string classCode, string moduleName, string item, string buildOrCollect, Action<List<Question>> completionBlock)
    {
        List<Question> questionAndAnswerList = new List<Question>();

        Router.GetClassroomInfo(classCode, moduleName, item, buildOrCollect).GetValueAsync().ContinueWith((task) =>
        {
            foreach (var question in task.Result.Children)
            {
                string questionText = task.Result.Child(question.Key.ToString()).Child("question").Value.ToString();
                int loop = (int)task.Result.Child(question.Key.ToString()).Child("answers").ChildrenCount;
                List<Answer> currentAnswerList = new List<Answer>();

                for (int i = 0; i < loop; i++)
                {
                    string answer = "A" + (i + 1).ToString();
                    
                    string answerText = task.Result.Child(question.Key.ToString()).Child("answers").Child(answer).Value.ToString();
                    Answer currentAnswer = new Answer(answerText, true);
                    if (i == 0)
                    {
                        currentAnswer = new Answer(answerText, true);
                    }
                    else
                    {
                        currentAnswer = new Answer(answerText, false);
                    }
                    currentAnswerList.Add(currentAnswer);
                }
                questionAndAnswerList.Add(new Question(questionText, currentAnswerList));
            }
            completionBlock(questionAndAnswerList);
        });
    }


    public void SubmitAnswer(string uid, string classCode, string moduleName, string item, string buildOrCollect, string questionNumber, string submittedAnswer)
    {
        Router.StoreUserAnswers(uid, classCode, moduleName, item, buildOrCollect, questionNumber).SetValueAsync(submittedAnswer);
    }
    public void TimeAndAttempts(string uid, string classCode, string moduleName, string item, string buildOrCollect, float timeSpent, int numAttempts)
    {
        Router.StoreTimeAndAttempt(uid, classCode, moduleName, item, buildOrCollect).Child("time").SetValueAsync(timeSpent);
        Router.StoreTimeAndAttempt(uid, classCode, moduleName, item, buildOrCollect).Child("attempts").SetValueAsync(numAttempts);
    }

}