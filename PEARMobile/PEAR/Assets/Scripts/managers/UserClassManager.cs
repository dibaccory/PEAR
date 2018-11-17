using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class UserClassManager : MonoBehaviour {

    public InputField classCodeInput;
    public Button submitButton;

    public List<Classroom> classroomList = new List<Classroom>();
    public List<string> moduleList      = new List<string>();
    public List<string> loginItemList = new List<string>();


    public GameObject classItem;
    public GameObject moduleItem;
    public GameObject classScroll;
    public GameObject moduleScroll;


    private void Awake()
    {
        submitButton.interactable = false;
        classroomList.Clear();
        UpdateClasses();
    }

    void CreateClassRow(Classroom classroom)
    {
       GameObject newRow = Instantiate(classItem) as GameObject;
       newRow.GetComponent<RowConfig>().Initalize(classroom);
       newRow.GetComponent<Button>().onClick.AddListener(
       delegate()
       {
          OnClassroomClick( newRow.GetComponent<RowConfig>().text.text );
       });

       newRow.transform.SetParent(classScroll.transform, false);
    }

    void CreateModuleRow(string module)
    {
        
        GameObject newRow = Instantiate(moduleItem) as GameObject;
        newRow.GetComponent<RowConfig>().Initalize(module);
        newRow.GetComponent<Button>().onClick.AddListener(
        delegate()
        {
          OnModuleClick(module);
        });

        newRow.transform.SetParent(moduleScroll.transform, false);
    }

    void UpdateClasses()
    {
        FirebaseUser user = DatabaseManager.sharedInstance.GetUser();
        DatabaseManager.sharedInstance.GetClasses(user.UserId, (c) =>
        {
            classroomList = c;
            foreach (Classroom classroom in classroomList)
            {
                CreateClassRow(classroom);
            }
        });
    }

    void UpdateModules(string classCode)
    {
      DatabaseManager.sharedInstance.GetModules(classCode, (m) =>
     {
         moduleList = m;
         foreach (string module in moduleList)
         {
             CreateModuleRow(module);
         }
     });
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


        //Destroy all the row GameObjects currently in place

        UpdateClasses();
    }

    public void OnClassroomClick(string classCode)
    {
        FindObjectOfType<SceneController>().classroom = classCode;
        Debug.Log("Current Class Code is : " + classCode);
        UpdateModules(classCode);
    }

    public void OnModuleClick(string module)
    {
        FindObjectOfType<SceneController>().module = module;
        Debug.Log("Current Module is : " + module);
        FindObjectOfType<SceneController>().FadeAndLoadScene("CollectScene");
    }

}
