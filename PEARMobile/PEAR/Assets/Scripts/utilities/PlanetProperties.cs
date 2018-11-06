using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetProperties : MonoBehaviour {
    //public GameObject obj;
    public Vector3 originalPosition;
    public Material map;

    string planetName;
    int rotationSpeed;
    float resolutionSpeed;
    Vector3 planetScale;
    float planetDistance;
    bool correctlyPlaced;


    SceneController controller;

    // Use this for initialization
    void SetProperties(int rotation, float resolution)
    {
        rotationSpeed = rotation;
        resolutionSpeed = resolution;

        //Check if user already placed this item
        if(controller.itemDictionary[planetName].isPlaced)
        {
            SuccessfulPlacement();
        }
    }


    //Sets wireframe objects to planets automatically.
    //TODO: have a function do this when correctly placed
    private void Awake()
    {
        planetName = transform.name;
        controller = FindObjectOfType<SceneController>();

        switch (planetName)
        {
            case "mercury":
                SetProperties(2, 8.0F);
                planetScale = new Vector3(0.5F, 0.5F, 0.5F);
                planetDistance = 55.0F;
                break;

            case "venus":
                SetProperties(2, -5.3F);
                planetScale = new Vector3(1.0F, 1.0F, 1.0F);
                planetDistance = 60.0F;
                break;

            case "earth":
                SetProperties(4, 5.5F);
                planetScale = new Vector3(1.5F, 1.5F, 1.5F); 
                planetDistance = 65.0F;
                break;

            case "mars":
                SetProperties(2, 3.0F);
                planetScale = new Vector3(0.7F, 0.7F, 0.7F); 
                planetDistance = 70.0F;
                break;

            case "jupiter":
                SetProperties(6, 4.1F);
                planetScale = new Vector3(7.0F, 7.0F, 7.0F); 
                planetDistance = 62.0F;
                break;

            case "saturn":
                SetProperties(2, 8.0F);
                planetScale = new Vector3(6.0F, 6.0F, 6.0F);
                planetDistance = 50.0F;
                break;

            case "uranus":
                SetProperties(2, 8.0F);
                planetScale = new Vector3(5.0F, 5.0F, 5.0F);
                planetDistance = 44.0F;
                break;

            case "neptune":
                SetProperties(2, 8.0F);
                planetScale = new Vector3(4.0F, 4.0F, 4.0F);
                planetDistance = 40.0F;
                break;

            default:
                SetProperties(7, 0.0F);
                planetScale = new Vector3(40.0F, 40.0F, 40.0F);
                planetDistance = 0.0F;
                break;


        }
    }

    private void SuccessfulPlacement()
    {
        controller.activeItem = null; //Move out of this nested if-statement so user will have to re-click the almanac item every-time
        controller.selectedSceneItemInBuildMode = null;
        controller.itemSelected = false;
        controller.itemDictionary[planetName].isPlaced = true;

        transform.localPosition.Set(transform.localPosition.x, transform.localScale.y, planetDistance);
        //transform.localScale = planetScale;  --ISSUE: Gotta move other planet distances if they collide when the map is loaded
        GetComponent<Renderer>().material = map;
    }


    private void OnMouseUp()
    {
        Debug.Log("u did it");
        //if user selected orb first, open up almanac to choose item
        if (controller.selectedSceneItemInBuildMode != null)
        {
            //open almanac
            if (!FindObjectOfType<AlmanacFormManager>().almanacPanel.activeSelf)
            {
                FindObjectOfType<AlmanacFormManager>().InterfaceButtonClick();
            }

            //if orb selected the same item as the active item...
            if (controller.itemDictionary[planetName] == controller.activeItem)
            {
                SuccessfulPlacement();
            }
        }

        //if user selected an item in the almanac first...
        else if (controller.itemSelected)
        {
            if (controller.itemDictionary[planetName] == controller.activeItem)
            {
                SuccessfulPlacement();
            }
        }

        else if(!controller.itemDictionary[planetName].isPlaced)
        {
            controller.selectedSceneItemInBuildMode = planetName;

        }
    }

    // Update is called once per frame
    void Update () 
    {

        //IDEA: When item is selected, planets organize into a line


        if (!(controller.itemSelected && controller.selectedSceneItemInBuildMode.Equals("")))
        {
            transform.RotateAround(GameObject.Find("Mid Air Stage").transform.position, Vector3.up, (float)resolutionSpeed * Time.deltaTime);
            transform.Rotate(rotationSpeed * Vector3.up * Time.deltaTime);
        }
        else
        {
            //move towards original position
        }
    }
}
