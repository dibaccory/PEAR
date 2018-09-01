using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class UserClassManager : MonoBehaviour {

    public List<User> classroomList = new List<User>();
    Firebase.Auth.FirebaseAuth auth;

    public GameObject rowPrefab;
    public GameObject scrollContainer;

    private void Awake()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        string uid = user.UserId;

        classroomList.Clear();

        DatabaseManager.sharedInstance.GetClasses( result =>
        {
            Debug.Log("result returned");
            classroomList = result;

            InitalizeUI();

        });
    }

    void InitalizeUI()
    {
        Debug.Log("initalize UI called");
        foreach(User classroom in classroomList)
        {
            CreateRow(classroom);
        }
    }

    void CreateRow(User classroom)
    {
        Debug.Log("create row classroom called with " + classroom.email);
        GameObject newRow = Instantiate(rowPrefab) as GameObject;
        newRow.GetComponent<RowConfig>().Initalize(classroom);
        newRow.transform.SetParent(scrollContainer.transform, false);
    }
}
