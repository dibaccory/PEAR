using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Router : MonoBehaviour
{

    private static DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

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

    public static DatabaseReference UserWithUID(string uid)
    {
        return baseRef.Child("users").Child(uid);
    }

    //public static DatabaseReference AddUserClassroom(string uid, Classroom classroom)
    //{
    //    return baseRef.SetTaskAsync(uid + "/" + classroom.classCode, classroom)
    //}

}
