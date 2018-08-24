using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class User
{
    public string email;
    public string name;
    //public int level;

    public User(string email, string name)
    {
        this.email = email;
        this.name = name;
        //this.level = level;
    }

    public User(IDictionary<string, object> dict) 
    {
        this.email = dict["email"].ToString();
        this.name = dict["name"].ToString();
        //this.level = Convert.ToInt32(dict["level"]);
    }
}