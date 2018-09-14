﻿using System.Collections;
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
        return baseRef.Child("classrooms").Child(classCode).Child("users").Child(uid);
    }

    public static DatabaseReference ClassWithUser2(string uid, string classCode)
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

}
