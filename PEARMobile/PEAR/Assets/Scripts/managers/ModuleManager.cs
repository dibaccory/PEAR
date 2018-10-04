using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using Firebase.Database;

public class ModuleManager : MonoBehaviour {

    public List<Module> moduleList = new List<Module>();

    public GameObject rowPrefab;
    public GameObject scrollContainer;

    private void Awake()
    {
        moduleList.Clear();
        string classCode = "astronomy";

        Router.GetModules(classCode).GetValueAsync().ContinueWith((task) =>
        {
            DataSnapshot moduleSnapshot = task.Result;
            Debug.Log(moduleSnapshot.ChildrenCount);

            foreach (DataSnapshot module in moduleSnapshot.Children)
            {
                var moduleDict = (IDictionary<string, object>)module.Value;
                Module newModule = new Module(moduleDict);
                moduleList.Add(newModule);
            }

            InitalizeUI();
        });

    }

    void InitalizeUI()
    {
        foreach (Module module in moduleList)
        {
            CreateRow(module);
        }
    }

    void CreateRow(Module module)
    {
        GameObject newRow = Instantiate(rowPrefab) as GameObject;
        newRow.GetComponent<RowConfig>().Initalize(module);
        newRow.transform.SetParent(scrollContainer.transform, false);
    }

}
