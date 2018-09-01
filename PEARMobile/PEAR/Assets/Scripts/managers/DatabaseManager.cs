using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

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

        Router.UserWithClass(uid, classroom.classCode).SetValueAsync("true");

        Router.ClassWithUser(uid, classroom.classCode, user.name).SetValueAsync("true");


    }

    public void GetClasses(Action<List<User>> completionBlock)
    {
        List<User> tempList = new List<User>();

        Router.Users().GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot classesSnapshot = task.Result;

            foreach (DataSnapshot classnode in classesSnapshot.Children)
            {
                Debug.Log("for each ran!");
                var classDict = (IDictionary<string, object>)classnode.Value;
                User newClassroom = new User(classDict);
                tempList.Add(newClassroom);
            }
            Debug.Log("temp list");
            completionBlock(tempList);
        });
    }
}
