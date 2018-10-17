using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour {
    public GameObject obj;
    string planetName;
    int rotationSpeed;
    float resolutionSpeed;
	// Use this for initialization
	void Start () 
    {

	}

    void SetProperties(int rotation, float resolution, Vector3 scale)
    {
        rotationSpeed = rotation;
        resolutionSpeed = resolution;
        obj.transform.localScale = scale;
        obj.GetComponent<Renderer>().material = Resources.Load(planetName + "_material") as Material;
    }


    //Sets wireframe objects to planets automatically.
    //TODO: have a function do this when correctly placed
    private void Awake()
    {
        planetName = obj.transform.name;
        obj = Resources.Load(planetName) as GameObject;
        switch (planetName)
        {
            case "mercury":
                SetProperties(2, 8.0F, new Vector3(0.5F, 0.5F, 0.5F));
                break;

            case "venus":
                SetProperties(2, -5.3F, new Vector3(1.0F, 1.0F, 1.0F));
                break;

            case "earth":
                SetProperties(4, 5.5F, new Vector3(1.5F, 1.5F, 1.5F));
                break;

            case "mars":
                SetProperties(2, 3.0F, new Vector3(0.7F, 0.7F, 0.7F));
                break;

            case "jupiter":
                SetProperties(6, 4.1F, new Vector3(7.0F, 7.0F, 7.0F));
                break;

            case "saturn":
                SetProperties(2, 8.0F, new Vector3(6.0F, 6.0F, 6.0F));
                break;

            case "uranus":
                SetProperties(2, 8.0F, new Vector3(5.0F, 5.0F, 5.0F));
                break;

            case "neptune":
                SetProperties(2, 8.0F, new Vector3(4.0F, 4.0F, 4.0F));
                break;

            default:
                SetProperties(7, 0.0F, new Vector3(30.0F, 30.0F, 30.0F));
                break;


        }
    }

    // Update is called once per frame
    void Update () 
    {
        transform.RotateAround(GameObject.Find("Mid Air Stage").transform.position, Vector3.up, (float)resolutionSpeed * Time.deltaTime);
        transform.Rotate(rotationSpeed * Vector3.up * Time.deltaTime);
    }
}
