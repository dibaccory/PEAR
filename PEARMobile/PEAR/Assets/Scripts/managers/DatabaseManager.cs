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


        //Router.UserWithClass(uid, classroom.classCode).SetValueAsync(classJSON);

        //Router.ClassWithUser(uid, classroom.classCode, user.name).SetValueAsync("true");

    }

    public void AddClass(string classCode, Classroom classroom, FirebaseUser user)
    {
        //User userInfo = new User(user.UserId.email, user.name);
        string userJSON = JsonUtility.ToJson(user);
        Debug.Log("add class user info" + userJSON);
        string classJSON = JsonUtility.ToJson(classroom);
        Debug.Log("add class class info" + classJSON);

        Router.UserWithClass(user.UserId, classroom.classCode).SetRawJsonValueAsync(classJSON);
        Router.ClassWithUser2(user.UserId, classroom.classCode).SetValueAsync(user.Email);

    }

    public void GetClasses(string uid, Action<List<Classroom>>completionBlock)
    {
        List<Classroom> tempList = new List<Classroom>();

        Router.UsersClasses(uid).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot classesSnapshot = task.Result;
            string print = classesSnapshot.GetRawJsonValue();
            Debug.Log("this is the datasnapshot " + classesSnapshot);
            //Debug.Log(print[0]);


            foreach (DataSnapshot classnode in classesSnapshot.Children)
            {
                Debug.Log("for each ran!");
                var classDict = (IDictionary<string, object>)classnode.Value;
                Debug.Log(classDict);
                Classroom newClassroom = new Classroom(classDict);
                tempList.Add(newClassroom);
            }
            Debug.Log("temp list");
            completionBlock(tempList);
        });
    }
}
