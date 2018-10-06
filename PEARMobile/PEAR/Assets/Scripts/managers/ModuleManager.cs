using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using Firebase.Database;

public class ModuleManager : MonoBehaviour {

    public List<string> moduleList = new List<string>();

    public GameObject rowPrefab;
    public GameObject scrollContainer;

    private void Awake()
    {
        moduleList.Clear();
        string classCode = "astronomy";


        Router.GetModules(classCode).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot moduleSnapshot = task.Result;

            foreach (DataSnapshot module in moduleSnapshot.Children)
            {
                var moduleKey = module.Key;
                moduleList.Add(moduleKey);
            }
            InitalizeUI();

        });

        //DatabaseManager.sharedInstance.GetModules(classCode, (result) =>
        //{
        //    moduleList = result;
        //    InitalizeUI();
        //});

    }

    void InitalizeUI()
    {
        foreach (string moduleKey in moduleList)
        {
            CreateRow(moduleKey);
        }
    }

    void CreateRow(string moduleKey)
    {
        GameObject newRow = Instantiate(rowPrefab) as GameObject;
        newRow.GetComponent<RowConfig>().Initalize(moduleKey);
        newRow.transform.SetParent(scrollContainer.transform, false);
    }

}
