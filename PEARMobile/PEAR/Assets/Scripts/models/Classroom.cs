using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Classroom
{
    public string classCode;
    //public string status;
    //public int level;

    public Classroom(string classCode)
    {
        this.classCode = classCode;
        //this.status = status;
        //this.string2 = string2;
        //this.level = level;
    }

    public Classroom(IDictionary<string, object> dict)
    {
        this.classCode = dict["classCode"].ToString();
        //this.status = dict["status"].ToString();
        //this.level = Convert.ToInt32(dict["level"]);
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["clasCode"] = classCode;
        //result["status"] = status;

        return result;
    }
}