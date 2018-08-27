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

        Debug.Log(Router.Users());
    }

    public void CreateNewUser(User user, string uid, Classroom classroom)
    {
        string userJSON = JsonUtility.ToJson(user);
        Router.UserWithUID(uid).SetRawJsonValueAsync(userJSON);

        string classJSON = JsonUtility.ToJson(classroom);
        Router.UserWithClass(uid, classroom.classCode).SetValueAsync("true");

        Router.ClassWithUser(uid, classroom.classCode, user.name).SetValueAsync("true");


    }

    public void GetUsers(Action<List<User>> completionBlock)
    {
        List<User> tempList = new List<User>();

        Router.Users().GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot users = task.Result;
            foreach(DataSnapshot usernode in users.Children) 
            {
                var userDict = (IDictionary<string,object>) usernode.Value;
                User newUser = new User(userDict);
                tempList.Add(newUser);
            }
            completionBlock(tempList);
        });
    }
}
