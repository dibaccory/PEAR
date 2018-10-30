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
    Firebase.Auth.FirebaseAuth auth;

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
        Router.ClassWithUser(user.UserId, classroom.classCode).SetValueAsync(user.Email);

    }

    public void GetClasses(string uid, Action<List<Classroom>> completionBlock)
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

    public void GetModules(string classCode, Action<List<string>> completionBlock)
    {
        List<string> tempList = new List<string>();

        Router.GetModules(classCode).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot moduleSnapshot = task.Result;
            //Debug.Log(moduleSnapshot.GetRawJsonValue());

            foreach (DataSnapshot module in moduleSnapshot.Children)
            {
                var moduleKey = module.Key;
                tempList.Add(moduleKey);
            }
            completionBlock(tempList);
        });
    }

    public void getQnA(string classCode, string moduleName, string item, string buildOrCollect, Action<List<Question>> completionBlock)
    {
        List<Question> questionAndAnswerList = new List<Question>();

        Router.GetClassroomInfo(classCode, moduleName, item, buildOrCollect).GetValueAsync().ContinueWith((task) =>
        {
            int qNum = 1;
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
                questionAndAnswerList.Add(new Question(questionText, currentAnswerList, qNum++));
            }
            completionBlock(questionAndAnswerList);
        });
    }

    public void getMaterialNames(Action<Stack<string>> lambdaBuster)
    {
      //Debug.Log("we in");
      Stack<string> k = new Stack<string>();

      Router.ModuleMaterials("astronomy", "solar system").GetValueAsync().ContinueWith((task) =>
      {
        DataSnapshot materials = task.Result;
        foreach (DataSnapshot entry in materials.Children)
        {
          Debug.Log(entry.Key);
          k.Push(entry.Key);
        }
          lambdaBuster(k);
      });
    }
    public void SubmitAnswer(string uid,
                             string classCode,
                             string moduleName,
                             string item,
                             string buildOrCollect,
                             string questionNumber,
                             string submittedAnswer)
    {

        Router.StoreAttempts(uid, classCode, moduleName, item, buildOrCollect).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot snapshot = task.Result;

            string attemptNum = snapshot.Value.ToString();

            //if (snapshot.Value == null)
            //{
            //    //Debug.Log("null as fuvk");
            //    attemptNum = "1";
            //}
            //else
            //{
            //    int num = Convert.ToInt32(snapshot.Value.ToString());
            //    attemptNum = (num + 1).ToString();
            //}



            Router.StoreUserAnswers(uid,
                                    classCode,
                                    moduleName,
                                    item,
                                    buildOrCollect,
                                    attemptNum,
                                    questionNumber).SetValueAsync(submittedAnswer);

        });


    }
    //add attempt number
    public void StoreTime(string uid, 
                                string classCode, 
                                string moduleName, 
                                string item, 
                                string buildOrCollect,
                                double timeSpent)
    {

        Router.StoreAttempts(uid, classCode, moduleName, item, buildOrCollect).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot snapshot = task.Result;

            string attemptNum = snapshot.Value.ToString();
            Debug.Log("store time attempt number" + attemptNum);
            Debug.Log("setting time to" + timeSpent);


            //if (snapshot.Value == null)
            //{
            //    //Debug.Log("null as fuvk");
            //    attemptNum = "1";
            //}
            //else
            //{
            //    int num = Convert.ToInt32(snapshot.Value.ToString());
            //    attemptNum = (num + 1).ToString();
            //}

            Router.StoreTime(uid, classCode, moduleName, item, buildOrCollect, attemptNum).SetValueAsync(timeSpent);

        });

    }

    public void StoreAttempts(string uid,
                                string classCode,
                                string moduleName,
                                string item,
                                string buildOrCollect)
    {
        Router.StoreAttempts(uid, classCode, moduleName, item, buildOrCollect).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot snapshot = task.Result;
            int num;
            if (snapshot.Value == null)
            {
                //Debug.Log("null as fuvk");
                num = 1;
            }
            else
            {
                num = Convert.ToInt32(snapshot.Value.ToString());
                num++;
            }
            Router.StoreAttempts(uid, classCode, moduleName, item, buildOrCollect).SetValueAsync(num);
        });

    }

    public void ListItemsCollected(string uid,string classCode, string moduleName, Action<List<string>> completionBlock)
    {
        List<string> tempList = new List<string>();

        Router.ListItemsCollected(uid,classCode,moduleName).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot itemListSnapshot = task.Result;

            foreach (DataSnapshot item in itemListSnapshot.Children)
            {
                var itemKey = item.Key;
                tempList.Add(itemKey);
            }
            completionBlock(tempList);
        });
    }

    public FirebaseUser GetUser()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        return user;
    }

    public void Logout()
    {
        Debug.Log("Logout function invoked");
        // TODO: This might not be the best place for this function
        //       Needs to determine everything the user has gathered
        //       and/or built and upload it to the database
    }
}
