using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

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

        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pear-f60a2.firebaseio.com/");

        Debug.Log(Router.Users());
    }

    public void CreateNewUser(User user, string uid)
    {
        string userJSON = JsonUtility.ToJson(user);
        Router.UserWithUID(uid).SetRawJsonValueAsync(userJSON);
    }
}
