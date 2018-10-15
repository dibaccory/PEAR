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
    public List<string> loginItemList = new List<string>();


    public GameObject rowPrefab;
    public GameObject scrollContainer;

    private void Awake()
    {
        submitButton.interactable = false;

        string uid = DatabaseManager.sharedInstance.GetUser().UserId;

        classroomList.Clear();

        DatabaseManager.sharedInstance.GetClasses(uid, (result) =>
        {
            classroomList = result;
            InitalizeUI();
        });
    }

    void InitalizeUI()
    {
        foreach (Classroom classroom in classroomList)
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
        FirebaseUser user = DatabaseManager.sharedInstance.GetUser();
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
