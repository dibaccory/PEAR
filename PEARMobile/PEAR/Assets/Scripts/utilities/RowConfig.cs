using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowConfig : MonoBehaviour {

    public Text classCode;
   
    public void Initalize(User classroom)
    {
        Debug.Log("initalize in row config called with " + classroom.email);
        this.classCode.text = classroom.email;
    }
}
