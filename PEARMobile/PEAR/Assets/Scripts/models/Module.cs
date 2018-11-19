using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Module
{
    public string moduleName;
    IDictionary<string, object> etc;

    public Module(string moduleName)
    {
        this.moduleName = moduleName;
    }

    public Module(IDictionary<string, object> dict)
    {
        this.moduleName = dict["moduleName"].ToString();
        this.etc = dict;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["moduleName"] = moduleName;
        return result;
    }
}
