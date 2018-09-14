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

        Router.GetClassroomInfo(classCode, moduleName, item).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot snapshot = task.Result;

            foreach (DataSnapshot classnode in snapshot.Children)
            {
                Debug.Log(classnode.Key.ToString());

                Router.GetClassroomInfo(classCode, moduleName, item, classnode.Key.ToString()).GetValueAsync().ContinueWith((task2) =>
                {
                    DataSnapshot snapshot2 = task2.Result;

                    foreach (DataSnapshot questionNode in snapshot2.Children)
                    {
                        Router.GetClassroomInfo(classCode, moduleName, item, classnode.Key.ToString(), questionNode.Key.ToString()).GetValueAsync().ContinueWith((task3) =>
                        {
                            DataSnapshot snapshot3 = task3.Result;
                            Debug.Log(snapshot3.Child(questionNode.Key.ToString()).Child("question").GetRawJsonValue());
                            Debug.Log(snapshot3.Child(questionNode.Key.ToString()).Child("answers").ChildrenCount);

                            Debug.Log(snapshot3.Child(questionNode.Key.ToString()).Child("answers").GetRawJsonValue());


                        });
                    }

                });
                //var classDict = (IDictionary<string, object>)classnode.Value;
                //Debug.Log(classDict);
                //Classroom newClassroom = new Classroom(classDict);
                //tempList.Add(newClassroom);
            }
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
