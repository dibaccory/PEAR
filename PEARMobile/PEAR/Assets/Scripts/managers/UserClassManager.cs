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
    public List<Module> moduleList      = new List<Module>();
    public List<string> loginItemList = new List<string>();


    public GameObject classItem;
    public GameObject moduleItem;
    public GameObject classScroll;
    public GameObject moduleScroll;
    public string uid;
    //public string classCode;

    private void Awake()
    {
        submitButton.interactable = false;

        uid = DatabaseManager.sharedInstance.GetUser().UserId;

        classroomList.Clear();
        UpdateClasses();

        //string classCode = "astronomy";
        //string moduleName = "solar system";
        //string item = "earth";
        //string buildOrCollect = "collect";
        //double timeSpent = 2.54325;
        //int numAttempts = 4;


        // DatabaseManager.sharedInstance.TimeAndAttempts(uid,classCode,moduleName,item,buildOrCollect,timeSpent,numAttempts);
        //
        //
        // DatabaseManager.sharedInstance.GetModules(classCode, (result) =>
        // {
        //     moduleList = result;
        //     InitalizeUI();
        // });
    }

    void CreateClassRow(Classroom classroom)
    {
       GameObject newRow = Instantiate(classItem) as GameObject;
       newRow.GetComponent<RowConfig>().Initalize(classroom);
       newRow.GetComponent<Button>().onClick.AddListener(
       delegate()
       {
          OnClassroomClick( newRow.GetComponent<RowConfig>().classCode.text );
       });

       newRow.transform.SetParent(classScroll.transform, false);
    }

    void CreateModuleRow(Module module)
    {
        Debug.Log("woop");
        GameObject newRow = Instantiate(moduleItem) as GameObject;
        newRow.GetComponent<RowConfig>().Initalize(module);
        newRow.GetComponent<Button>().onClick.AddListener(
        delegate()
        {
          OnModuleClick("CollectScene");
        });

        newRow.transform.SetParent(moduleScroll.transform, false);
    }

    void UpdateClasses()
    {
      DatabaseManager.sharedInstance.GetClasses(uid, (c) =>
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
         foreach (Module module in moduleList)
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
        UpdateModules(classCode);
    }

    public void OnModuleClick(string module)
    {
      SceneManager.LoadScene("CollectScene");
    }

}
