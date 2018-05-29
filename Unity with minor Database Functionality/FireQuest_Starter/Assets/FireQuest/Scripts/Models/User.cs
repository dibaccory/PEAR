using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

    public string email;
    public int score;
    public int level;

    public User(string email, int score, int level)
    {
        this.email = email;
        this.score = score;
        this.level = level;
    }

}
