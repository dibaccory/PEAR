using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Classroom : MonoBehaviour {

    public string classCode;
    public string status;

    public Classroom(string classCode, string status)
    {
        this.classCode = classCode;
        this.status = status;
    }

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        result["classroom"] = classCode;
        result["status"] = status;

        return result;
    }
}