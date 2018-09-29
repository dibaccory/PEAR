using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
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
    }

    public void AddClass(string classCode, Classroom classroom, FirebaseUser user)
    {
        string userJSON = JsonUtility.ToJson(user);
        string classJSON = JsonUtility.ToJson(classroom);

        Router.UserWithClass(user.UserId, classroom.classCode).SetRawJsonValueAsync(classJSON);
        Router.ClassWithUser2(user.UserId, classroom.classCode).SetValueAsync(user.Email);
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
            DataSnapshot snapshot = task.Result;

            foreach (DataSnapshot question in snapshot.Children)
            {
                Question currentQuestion = new Question();

                currentQuestion.QuestionText = snapshot.Child(question.Key.ToString()).Child("question").Value.ToString();
                //Debug.Log(currentQuestion.QuestionText);
                long loop = snapshot.Child(question.Key.ToString()).Child("answers").ChildrenCount;

                for (int i = 1; i <= loop; i++)
                {
                    string answer = "A" + i.ToString();
                    if (i == 1)
                    {
                        currentQuestion.CorrectAnswer = snapshot.Child(question.Key.ToString()).Child("answers").Child(answer).Value.ToString();
                        //Debug.Log(currentQuestion.CorrectAnswer);

                    }
                    else
                    {
                        currentQuestion.otherAnswers.Add(snapshot.Child(question.Key.ToString()).Child("answers").Child(answer).Value.ToString());
                        //Debug.Log(snapshot.Child(question.Key.ToString()).Child("answers").Child(answer).Value.ToString());

                    }
                }
                questionAndAnswerList.Add(currentQuestion);
            }
            completionBlock(questionAndAnswerList);
        });
    }

    public void GetUserAnswers(string uid, string classCode, string moduleName, string item, string buildOrCollect, string questionNumber /*User user, string uid, Classroom classroom*/)
    {
        //string userDataJSON = JsonUtility.ToJson(user);
        //Router.GetUserAnswers(uid, classCode, moduleName, item, buildOrCollect, questionNumber).SetRawJsonValueAsync(userDataJSON);

    }
    //get keys of the items in the class to populate the collect mode items
    public void ItemList(string classCode, string moduleName, Action<List<Item>> completionBlock)
    {
        //List<Item> tempList = new List<Item>();

        //Router.GetClassroomInfo(classCode,moduleName).GetValueAsync().ContinueWith((task) =>
        //{
        //    DataSnapshot classSnapshot = task.Result;

        //    foreach (DataSnapshot classnode in classSnapshot.Children)
        //    {
        //        var key = classnode.Key;
        //        Item newItem = new Item(classDict);
        //        tempList.Add(newItem);
        //    }
        //    completionBlock(tempList);
        //});
    }

    public void SubmitAnswer(string uid, string classCode, string moduleName, string item, string buildOrCollect, string questionNumber, string submittedAnswer)
    {
        Router.StoreUserAnswers(uid,classCode,moduleName, item, buildOrCollect,questionNumber).SetValueAsync(submittedAnswer);
    }
    public void TimeAndAttempts(string uid, string classCode, string moduleName, string item, string buildOrCollect, float timeSpent, int numAttempts)
    {
        Router.StoreTimeAndAttempt(uid, classCode, moduleName, item, buildOrCollect).Child("time").SetValueAsync(timeSpent);
        Router.StoreTimeAndAttempt(uid, classCode, moduleName, item, buildOrCollect).Child("attempts").SetValueAsync(numAttempts);
    }

}
