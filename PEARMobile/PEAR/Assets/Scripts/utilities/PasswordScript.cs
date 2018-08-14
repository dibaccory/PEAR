using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordScript : MonoBehaviour {
    public InputField password;
	// Use this for initialization
	void Start () {
        password.contentType = InputField.ContentType.Password;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
