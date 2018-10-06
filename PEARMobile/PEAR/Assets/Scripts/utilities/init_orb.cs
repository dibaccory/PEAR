using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init_orb : MonoBehaviour {

	int count = 9; //Should get this from the database; i.e. How many things do we need to collect for this module
// [{tag: }]
	public GameObject orb;
	// Use this for initialization
	void Start () {

		List<Vector3> temp = new List<Vector3>();

		for(int i=0; i < count; i++) {
			orb = Resources.Load("Orb") as GameObject;
			Vector3 pos = Random.onUnitSphere*30;
			temp.Add(pos);

			if(temp.Contains(pos) /* || check if within a certain distance of any of the currently added Vector3*/)

			if(pos.y < -10) { //Ensure user doesn't have to look to low
				pos.y = Mathf.Abs(pos.y);
			}
			Instantiate(orb, pos, Quaternion.identity);
			//Orb needs to have a script to check if the user had interacted with it.
		}

	}

	// Update is called once per frame
	void Update () {

	}
}
