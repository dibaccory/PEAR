using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Router : MonoBehaviour
{

    static DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

    public static DatabaseReference Users()
    {
        return baseRef.Child("users");
    }

    public static DatabaseReference UsersClasses(string uid)
    {
        return baseRef.Child("users").Child(uid).Child("classrooms");
    }

    public static DatabaseReference UserWithClass(string uid, string classCode)
    {
        return baseRef.Child("users").Child(uid).Child("classrooms").Child(classCode);
    }

    public static DatabaseReference ClassWithUser(string uid, string classCode)
    {
        return baseRef.Child("classrooms").Child(classCode).Child("users").Child(uid).Child("email");
    }

    public static DatabaseReference UserWithUID(string uid)
    {
        return baseRef.Child("users").Child(uid);
    }

    public static DatabaseReference GetClassroomInfo(string classCode, string moduleName, string item)
    {
        return baseRef.Child("classrooms").Child(classCode).Child("modules").Child(moduleName).Child(item);
    }

    public static DatabaseReference GetClassroomInfo(string classCode, string moduleName, string item, string buildOrCollect)
    {
        return baseRef.Child("classrooms").Child(classCode).Child("modules").Child(moduleName).Child(item).Child(buildOrCollect);
    }

    public static DatabaseReference GetClassroomInfo(string classCode, string moduleName, string item, string buildOrCollect, string questionNumber)
    {
        return baseRef.Child("classrooms").Child(classCode).Child("modules").Child(moduleName).Child(item).Child(buildOrCollect);
    }

    public static DatabaseReference GetModules (string classCode)
    {
      return baseRef.Child("classrooms").Child(classCode).Child("modules");
    }

    //Get all the Materials for current module
    public static DatabaseReference ModuleMaterials (string classCode, string moduleName)
    {
      //Do we have a getCurrentModule function?
        return baseRef.Child("classrooms").Child(classCode).Child("modules").Child(moduleName);
    }

    /*
    TODO Get current Module (to make this generic)
    pull in master

    */
    public static DatabaseReference StoreUserAnswers(string uid, string classCode, string moduleName, string item, string buildOrCollect, string questionNumber)
    {
        return baseRef.Child("answers").Child(uid).Child(classCode).Child("modules").Child(moduleName).Child(item).Child(buildOrCollect).Child(questionNumber).Child("answer given");

    }
    public static DatabaseReference StoreTime(string uid, string classCode, string moduleName, string item, string buildOrCollect)
    {
        return baseRef.Child("answers").Child(uid).Child(classCode).Child("modules").Child(moduleName).Child(item).Child(buildOrCollect).Child("time spent");

    }
    public static DatabaseReference StoreAttempts(string uid, string classCode, string moduleName, string item, string buildOrCollect)
    {
        return baseRef.Child("answers").Child(uid).Child(classCode).Child("modules").Child(moduleName).Child(item).Child(buildOrCollect).Child("attempts");

    }


}
