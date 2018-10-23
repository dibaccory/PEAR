using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class init_orb : MonoBehaviour {


	int count = 9; //Should get this from the database; i.e. How many things do we need to collect for this module
	public GameObject orb;
	public Stack<string> keys = new Stack<string>();
	// Use this for initialization
	void Start ()
	{
		DatabaseManager.sharedInstance.getMaterialNames((result) =>
		{
			keys = result;
			populateOrbs();
		});

	}

	void populateOrbs ()
	{
		List<Vector3> temp = new List<Vector3>();
		for(int i=0; i < count; i++)
		{
			Vector3 pos = Random.onUnitSphere*30;
            //if(temp.Contains(pos) /* || check if within a certain distance of any of the currently added Vector3*/){}
            temp.Add(pos);
			if(pos.y < -10)
			{ //Ensure user doesn't have to look to low
				pos.y = Mathf.Abs(pos.y);
			}

            orb = (GameObject)Instantiate(Resources.Load("Orb"), pos, Quaternion.identity);
            orb.name = keys.Pop();
            //Orb needs to have a script to check if the user had interacted with it.
        }
	}

	// Update is called once per frame
	void Update () {

	}
}
