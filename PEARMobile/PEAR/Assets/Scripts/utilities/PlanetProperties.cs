using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;

public class PlanetProperties : MonoBehaviour {
    //public GameObject obj;
    public Vector3 originalPosition;
    public Material map;

    string planetName;
    float rotationSpeed;
    float revolutionSpeed;
    Vector3 planetScale;
    public float planetDistance;
    bool correctlyPlaced;
    Vector3[] initPosition;
    float initScale;
    Item trueItem; //this item
    FirebaseUser user;
    private double timeSpent;
    string[] planets;

    static Vector3 sunScale;

    Transform origin;
    float initXRotation;

    Color highlightedColor, originalColor;

    SceneController controller;

    // Use this for initialization
    void SetProperties(float rotation, float revolution)
    {
        rotationSpeed = - rotation;
        revolutionSpeed = -revolution / 1.5F;

        //Check if user already placed this item
        if(controller.itemDictionary[planetName].isPlaced)
        {
            GetComponent<Renderer>().material = map;
            //SuccessfulPlacement();
        }
    } 


    //Sets wireframe objects to planets automatically.
    //TODO: have a function do this when correctly placed
    private void Awake()
    {
        initPosition = new Vector3[] 
        {
                new Vector3(0, 0, 0),
                new Vector3(1  * Mathf.Cos((Mathf.PI / 3)),    0, 1  * Mathf.Sin((Mathf.PI / 3)) ),
                new Vector3(2 * Mathf.Cos((Mathf.PI / 4)),    0, 2 * Mathf.Sin((Mathf.PI / 4)) ),
                new Vector3(3 * Mathf.Cos((Mathf.PI / 6)),    0, 3 * Mathf.Sin((Mathf.PI / 6)) ),
                new Vector3(4 * Mathf.Cos(0),                 0, 4 * Mathf.Sin(0) ),
                new Vector3(5 * Mathf.Cos((11*Mathf.PI / 6)), 0, 5 * Mathf.Sin((11*Mathf.PI / 6)) ),
                new Vector3(6 * Mathf.Cos((7 *Mathf.PI / 4)), 0, 6 * Mathf.Sin((7 *Mathf.PI / 4)) ),
                new Vector3(7 * Mathf.Cos((5 *Mathf.PI / 3)), 0, 7 * Mathf.Sin((5 *Mathf.PI / 3)) ),
                new Vector3(8 * Mathf.Cos((3 *Mathf.PI / 2)), 0, 8 * Mathf.Sin((3 *Mathf.PI / 2)) )
        };
        sunScale = GameObject.Find("sun").transform.localScale;
        initScale = transform.localScale.x; //scale is uniform so just store one
        planetName = transform.name;
        controller = FindObjectOfType<SceneController>();
        controller.activeItem = null;
        trueItem = controller.itemDictionary[planetName];
        originalColor = GetComponent<Renderer>().material.color;
        highlightedColor = new Color(0.5F, 0.5F, 0F);

        origin = GameObject.Find("origin").transform;
        initXRotation = origin.rotation.x;
        timeSpent = 0;

        user = DatabaseManager.sharedInstance.GetUser();

        switch (planetName)
        {
            case "mercury":
                SetProperties(0.3F, 47.87F);
                transform.localPosition = initPosition[1];
                planetScale = new Vector3(0.5F, 0.5F, 0.5F);
                planetDistance = 30.0F;
                break;

            case "venus":
                SetProperties(-0.5F, 35.02F);
                transform.localPosition = initPosition[2];
                planetScale = new Vector3(1.3F, 1.3F, 1.3F);
                planetDistance = 35.0F;
                break;

            case "earth":
                SetProperties(5.0F, 29.78F);
                transform.localPosition = initPosition[3];
                planetScale = new Vector3(1.5F, 1.5F, 1.5F); 
                planetDistance = 40.0F;
                break;

            case "mars":
                SetProperties(5.0F, 24.1F);
                transform.localPosition = initPosition[4];
                planetScale = new Vector3(0.7F, 0.7F, 0.7F); 
                planetDistance = 45.0F;
                break;

            case "jupiter":
                SetProperties(11.0F, 13.1F);
                transform.localPosition = initPosition[5];
                planetScale = new Vector3(7.0F, 7.0F, 7.0F); 
                planetDistance = 50.0F;
                break;

            case "saturn":
                SetProperties(10.5F, 9.69F);
                transform.localPosition = initPosition[6];
                planetScale = new Vector3(6.0F, 6.0F, 6.0F);
                planetDistance = 55.0F;
                break;

            case "uranus":
                SetProperties(-5.5F, 6.8F);
                transform.localPosition = initPosition[7];
                planetScale = new Vector3(5.0F, 5.0F, 5.0F);
                planetDistance = 60.0F;
                break;

            case "neptune":
                SetProperties(5.4F, 5.43F);
                transform.localPosition = initPosition[8];
                planetScale = new Vector3(4.0F, 4.0F, 4.0F);
                planetDistance = 65.0F;
                break;

            default: //sun
                SetProperties(7, 0.0F);
                transform.localPosition = initPosition[0];
                planetScale = new Vector3(25.0F, 25.0F, 25.0F);
                planetDistance = 0.0F;
                break;


        }
    }

    private void SuccessfulPlacement()
    {
        Debug.Log("TIME SPENT ON " + controller.activeItem.name + " : " + timeSpent);
        //store time
        DatabaseManager.sharedInstance.StoreTime(DatabaseManager.sharedInstance.GetUser().UserId,
                                                 controller.classroom,
                                                 controller.module,
                                                 controller.activeItem.name,
                                                 "build",
                                                 timeSpent);
        //Store "is placed"
        Router.ItemBuilt(DatabaseManager.sharedInstance.GetUser().UserId, 
                         controller.classroom, 
                         controller.module, 
                         controller.activeItem.name).SetValueAsync(true);

        controller.activeItem = null; //Move out of this nested if-statement so user will have to re-click the almanac item every-time
        controller.selectedSceneItemInBuildMode = null;
        controller.itemDictionary[planetName].isPlaced = true;

        //transform.localPosition.Set(transform.localPosition.x, transform.localScale.y, planetDistance);
        //transform.localScale = planetScale;  --ISSUE: Gotta move other planet distances if they collide when the map is loaded
        GetComponent<Renderer>().material = map;

    }


    public void validatePlacement()
    {
        Debug.Log("controller var " + controller.activeItem.name);
        Debug.Log("controller module " + controller.module);
        Debug.Log("controller class " + controller.classroom);
        Debug.Log("username " + user.UserId);

        DatabaseManager.sharedInstance.StoreAttempts(DatabaseManager.sharedInstance.GetUser().UserId,
                                                     controller.classroom,
                                                     controller.module,
                                                     controller.activeItem.name,
                                                     "build");

        if (planetName == controller.activeItem.name)
        {
            GameObject.Find("Notification").GetComponent<Text>().text = "Successfully placed " + controller.activeItem.itemName + "!";
            SuccessfulPlacement();
        }
        else
        {
            //TODO: Incorrect Placement!
            GameObject.Find("Notification").GetComponent<Text>().text = "Sorry! Try again.";
            controller.activeItem = null; 
            controller.selectedSceneItemInBuildMode = null;
        }
        //attempts++;
    }

    private void OnMouseUp()
    {
        //if this item hasn't been correctly placed yet
        if (!controller.itemDictionary[planetName].isPlaced)
        {
            controller.selectedSceneItemInBuildMode = planetName;
            //GameObject.Find("Notification").GetComponent<Text>().text = "Currently selected: " + controller.itemDictionary[planetName].itemName;
        }
        //if almanac item hasn't been selected
        if (controller.activeItem == null)
        {
            //open almanac if it isn't open yet
            if (!FindObjectOfType<AlmanacFormManager>().almanacPanel.activeSelf)
            {
                FindObjectOfType<AlmanacFormManager>().InterfaceButtonClick();
            }

        }
        //almanac item seleted and scene item selected
        else
        {
            validatePlacement();
        }
       
    }

    // Update is called once per frame
    void Update () 
    {


        if (controller.activeItem == null && string.IsNullOrEmpty(controller.selectedSceneItemInBuildMode))
        {
            transform.Rotate(rotationSpeed * Vector3.up * Time.deltaTime);
            //if not correctly placed yet, set color back to original color
            if (!trueItem.isPlaced && GetComponent<Renderer>().material.color != originalColor)
            {
                GetComponent<Renderer>().material.color = originalColor;
            }

            transform.RotateAround(origin.transform.position, Vector3.up, (float)revolutionSpeed * Time.deltaTime);
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, 0, transform.localPosition.z), 8 * Time.deltaTime);

            //origin.rotation = Quaternion.RotateTowards(origin.rotation, new Quaternion(initXRotation, 0, 0, 1), 8 * Time.deltaTime);
            if(controller.itemDictionary[planetName].isPlaced)
            {


                if (planetName == "sun")
                {
                    transform.localScale = Vector3.MoveTowards(transform.localScale, planetScale, 8 * Time.deltaTime);
                    sunScale = transform.localScale;
                }
                else
                {
                    //Vector3 tempScale = this.transform.y
                    Vector3 scaleNorm = new Vector3(planetScale.x / sunScale.x, planetScale.y / sunScale.y, planetScale.z / sunScale.z);
                    transform.localScale = Vector3.MoveTowards(transform.localScale, scaleNorm, 8 * Time.deltaTime);
                }
                //Vector3 posNorm = new Vector3(planetScale.x / transform.parent.localScale.x, planetScale.y / transform.parent.localScale.y, planetScale.z / transform.parent.localScale.z);
                //transform.localScale = Vector3.MoveTowards(transform.localScale, scaleNorm, 8 * Time.deltaTime);
                //if(transform.GetChild(0))
                //transform.GetChild(0).transform.localScale = Vector3.MoveTowards(
                //transform.GetChild(0).transform.localScale,
                //new Vector3(initScale / transform.localScale.x, initScale / transform.localScale.y, initScale / transform.localScale.z),
                //5*Time.deltaTime);
            }
            else if(controller.itemDictionary["sun"].isPlaced && planetName != "sun")
            {
                Vector3 scaleNorm;
                if (controller.itemDictionary[planetName].isPlaced)
                {
                    scaleNorm = new Vector3(1.3F * planetScale.x / sunScale.x, 1.3F * planetScale.y / sunScale.y, 1.3F * planetScale.z / sunScale.z);
                }
                else
                {
                    scaleNorm = new Vector3(1.3F * initScale / sunScale.x, 1.3F * initScale / sunScale.y, 1.3F * initScale / sunScale.z);
                }
                transform.localScale = Vector3.MoveTowards(transform.localScale, scaleNorm, 8 * Time.deltaTime);
            }
        }
        else
        {
            //storing time for individual items
            if ( (controller.activeItem == trueItem || controller.selectedSceneItemInBuildMode == planetName ) && !controller.itemDictionary[planetName].isPlaced)
            {
                timeSpent += Time.deltaTime;
                Debug.Log("does this do a thing " + timeSpent);
            }
            //if not correctly placed yet, highlight item yellow
            if (!trueItem.isPlaced && GetComponent<Renderer>().material.color != highlightedColor)
            {
                GetComponent<Renderer>().material.color = highlightedColor;
            }

            //origin.rotation = Quaternion.RotateTowards(origin.rotation, new Quaternion(-90, 0, 0, 1), 8 * Time.deltaTime);

            //move towards original scale
            //transform.localScale = Vector3.MoveTowards(transform.localScale, initScale, Time.deltaTime);
        }
    }
}
