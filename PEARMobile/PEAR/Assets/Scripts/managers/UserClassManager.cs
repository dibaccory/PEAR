using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;

public class UserClassManager : MonoBehaviour {

    public InputField classCodeInput;
    public Button submitButton;

    public List<Classroom> classroomList = new List<Classroom>();
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
            Debug.Log("result returned");

            classroomList = result;

            InitalizeUI();

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
        Debug.Log("initalize UI called");
        foreach(Classroom classroom in classroomList)
        {
            CreateRow(classroom);
        }
    }

    void CreateRow(Classroom classroom)
    {
        Debug.Log("create row classroom called with " + classroom.classCode);
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
        Debug.Log("On Add Class Click");
    }
}
