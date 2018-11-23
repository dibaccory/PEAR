using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class PlanetProperties : MonoBehaviour {
    //public GameObject obj;
    public Vector3 originalPosition;
    public Material map;

    string planetName;
    int rotationSpeed;
    float resolutionSpeed;
    Vector3 planetScale;
    public float planetDistance;
    bool correctlyPlaced;
    Vector3[] initPosition;
    float initScale;
    Item trueItem; //this item
    FirebaseUser user;
    int attempts = 0;
    float timeSpent;
    string[] planets;

    static Vector3 sunScale;

    Transform origin;
    float initXRotation;

    Color highlightedColor, originalColor;

    SceneController controller;

    // Use this for initialization
    void SetProperties(int rotation, float resolution)
    {
        rotationSpeed = rotation;
        resolutionSpeed = resolution;

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
        planets = new string[] { "mercury", "venus", "earth", "mars", "jupiter", "saturn", "uranus", "neptune" };
        
        originalColor = GetComponent<Renderer>().material.color;
        highlightedColor = new Color(0.5F, 0.5F, 0F);

        origin = GameObject.Find("origin").transform;
        initXRotation = origin.rotation.x;
        timeSpent = 0;

        user = DatabaseManager.sharedInstance.GetUser();

        switch (planetName)
        {
            case "mercury":
                SetProperties(2, 8.0F);
                transform.localPosition = initPosition[1];
                planetScale = new Vector3(0.5F, 0.5F, 0.5F);
                planetDistance = 30.0F;
                break;

            case "venus":
                SetProperties(2, -5.3F);
                transform.localPosition = initPosition[2];
                planetScale = new Vector3(1.0F, 1.0F, 1.0F);
                planetDistance = 35.0F;
                break;

            case "earth":
                SetProperties(4, 5.5F);
                transform.localPosition = initPosition[3];
                planetScale = new Vector3(1.5F, 1.5F, 1.5F); 
                planetDistance = 40.0F;
                break;

            case "mars":
                SetProperties(2, 3.0F);
                transform.localPosition = initPosition[4];
                planetScale = new Vector3(0.7F, 0.7F, 0.7F); 
                planetDistance = 45.0F;
                break;

            case "jupiter":
                SetProperties(6, 4.1F);
                transform.localPosition = initPosition[5];
                planetScale = new Vector3(7.0F, 7.0F, 7.0F); 
                planetDistance = 50.0F;
                break;

            case "saturn":
                SetProperties(2, 8.0F);
                transform.localPosition = initPosition[6];
                planetScale = new Vector3(6.0F, 6.0F, 6.0F);
                planetDistance = 55.0F;
                break;

            case "uranus":
                SetProperties(2, 8.0F);
                transform.localPosition = initPosition[7];
                planetScale = new Vector3(5.0F, 5.0F, 5.0F);
                planetDistance = 60.0F;
                break;

            case "neptune":
                SetProperties(2, 8.0F);
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
        //store time
        DatabaseManager.sharedInstance.StoreTime(user.UserId,
                                                 controller.classroom,
                                                 controller.module,
                                                 controller.activeItem.name,
                                                 "build",
                                                 timeSpent);
        //Store "is placed"
        Router.ItemBuilt(user.UserId, 
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

        DatabaseManager.sharedInstance.StoreAttempts(user.UserId,
                                                     controller.classroom,
                                                     controller.module,
                                                     controller.activeItem.name,
                                                     "build");

        if (planetName == controller.activeItem.name)
        {
            SuccessfulPlacement();
        }
        else
        {
            //TODO: Incorrect Placement!
            controller.activeItem = null; 
            controller.selectedSceneItemInBuildMode = null;
        }
        //attempts++;
    }

    private void OnMouseUp()
    {
         //if this item hasn't been correctly placed yet
        if (!trueItem.isPlaced)
        {
            controller.selectedSceneItemInBuildMode = planetName;
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
        //storing time for individual items
        if(controller.activeItem == trueItem && !trueItem.isPlaced)
        {
            timeSpent += Time.deltaTime;
        }

        //IDEA: When item is selected, planets organize into a line
        transform.Rotate(rotationSpeed * Vector3.up * Time.deltaTime);
        if (controller.activeItem == null && string.IsNullOrEmpty(controller.selectedSceneItemInBuildMode))
        {
            //if not correctly placed yet, set color back to original color
            if (!trueItem.isPlaced && GetComponent<Renderer>().material.color != originalColor)
            {
                GetComponent<Renderer>().material.color = originalColor;
            }

            transform.RotateAround(origin.transform.position, Vector3.up, (float)resolutionSpeed * Time.deltaTime);
            //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, 0, transform.localPosition.z), 8 * Time.deltaTime);

            //origin.rotation = Quaternion.RotateTowards(origin.rotation, new Quaternion(initXRotation, 0, 0, 1), 8 * Time.deltaTime);
            if(controller.itemDictionary[planetName].isPlaced && !transform.localScale.Equals(planetScale))
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
        }
        else
        {
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
