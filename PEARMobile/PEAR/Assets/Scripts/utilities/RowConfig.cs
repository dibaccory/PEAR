using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowConfig : MonoBehaviour {

    public Text classCode;
    public Text moduleName;
   
    public void Initalize(Classroom classroom)
    {
        Debug.Log("classroom init");
        this.classCode.text = classroom.classCode;
    }
    public void Initalize(Module module)
    {
        Debug.Log("module init");
        this.moduleName.text = module.moduleName;
    }
}
