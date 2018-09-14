using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowConfig : MonoBehaviour {

    public Text classCode;
   
    public void Initalize(Classroom classroom)
    {
        this.classCode.text = classroom.classCode;
    }
}
