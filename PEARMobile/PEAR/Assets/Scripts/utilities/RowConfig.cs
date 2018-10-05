using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowConfig : MonoBehaviour {

    public Text text;
   
    public void Initalize(Classroom classroom)
    {
        Debug.Log("classroom init");
        this.text.text = classroom.classCode;
    }
    public void Initalize(string moduleKey)
    {
        Debug.Log("module init");
        this.text.text = moduleKey;
    }
}
