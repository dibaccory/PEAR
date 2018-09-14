using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Classroom
{
    public string classCode;

    public Classroom(string classCode)
    {
        this.classCode = classCode;
    }

    public Classroom(IDictionary<string, object> dict)
    {
        this.classCode = dict["classCode"].ToString();
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["classCode"] = classCode;
        return result;
    }
}