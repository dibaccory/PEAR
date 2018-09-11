using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init_orb : MonoBehaviour {

	int count = 9; //Should get this from the database; i.e. How many things do we need to collect for this module
// [{tag: }]
	public GameObject orb;
	// Use this for initialization
	void Start () {
		for(int i=0; i < count; i++) {
			orb = Resources.Load("Orb") as GameObject;
			Vector3 position = Random.onUnitSphere*30;
			if(position.y < -10) { //Ensure user doesn't have to look to low
				position.y = Mathf.Abs(position.y);
			}
			Instantiate(orb, position, Quaternion.identity);
			//Orb needs to have a script to check if the user had interacted with it.
		}

	}

	// Update is called once per frame
	void Update () {

	}
}
