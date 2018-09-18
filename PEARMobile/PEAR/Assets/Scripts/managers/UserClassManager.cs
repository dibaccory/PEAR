using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using Firebase.Database;

public class UserClassManager : MonoBehaviour {

    public InputField classCodeInput;
    public Button submitButton;

    public List<Classroom> classroomList = new List<Classroom>();
    public List<Question> questionList = new List<Question>();

    Firebase.Auth.FirebaseAuth auth;

    public GameObject rowPrefab;
    public GameObject scrollContainer;

    private void Awake()
    {
        submitButton.interactable = false;

        string uid = GetUser().UserId;

        classroomList.Clear();

        DatabaseManager.sharedInstance.GetClasses(uid, (result) =>
        {
            classroomList = result;
            InitalizeUI();
        });

        //some test code on how to pull up and store some database info
        string classCode = "test class";
        string moduleName = "solar system";
        string item = "earth";
        string buildOrCollect = "build";

        DatabaseManager.sharedInstance.getQnA(classCode, moduleName, item, buildOrCollect, (result) =>
        {
            questionList = result;
        });
    }

    private FirebaseUser GetUser()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        return user;
    }

    void InitalizeUI()
    {
        foreach(Classroom classroom in classroomList)
        {
            CreateRow(classroom);
        }
    }

    void CreateRow(Classroom classroom)
    {
        GameObject newRow = Instantiate(rowPrefab) as GameObject;
        newRow.GetComponent<RowConfig>().Initalize(classroom);
        newRow.transform.SetParent(scrollContainer.transform, false);
    }

    public void ValidateClassCode()
    {
        string classCode = classCodeInput.text;

        if (!(string.IsNullOrEmpty(classCode)) )
        {
            submitButton.interactable = true;
        }
        else
        {
            submitButton.interactable = false;
        }
    }

    public void OnAddClass()
    {
        FirebaseUser user = GetUser();
        Classroom classroom = new Classroom(classCodeInput.text);
        DatabaseManager.sharedInstance.AddClass(classCodeInput.text, classroom, user);
        classroomList.Clear();

        DatabaseManager.sharedInstance.GetClasses(user.UserId, (result) =>
        {

            classroomList = result;
            InitalizeUI();

        });
    }
}
