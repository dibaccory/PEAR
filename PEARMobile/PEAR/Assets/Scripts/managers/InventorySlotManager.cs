using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotManager : MonoBehaviour {

    public Item item;

    public void Use()
    {
        if(item)
        {
            Debug.Log("You used: " + item.itemName);
            var controller = FindObjectOfType<SceneController>();
            if(controller.currentScene == "CollectScene" || controller.currentScene == "ItemDisplay")
            {
                // If we're in the collect scene, and the item is in the inventory,
                // then we've already collected the item. If the user clicks on the item
                // in the inventory then bring them back to the item display scene
                // with the current item loaded
                controller.activeItem = item;
                controller.FadeAndLoadScene("ItemDisplay");   
            }
            else if(controller.currentScene == "BuildScene")
            {
                // If we're currently in the build scene, when the user clicks
                // an item in the inventory, it initiates the build mode
                // for that particular item
                // TODO: Implement this
                controller.activeItem = item;
                FindObjectOfType<AlmanacFormManager>().InterfaceButtonClick(); //close almanac
                Debug.Log("You used: " + item.itemName + " in build mode");

                if(!string.IsNullOrEmpty(controller.selectedSceneItemInBuildMode))
                {
                    var objectInScene = GameObject.Find(controller.selectedSceneItemInBuildMode).GetComponent<PlanetProperties>();
                    objectInScene.validatePlacement();
                }
            }
            else
            {
                // Otherwise, do nothing
                ;
            }
        }
    }

    public void UpdateInfo()
    {
        Text displayText = transform.Find("Text").GetComponent<Text>();
        Image displayImage = transform.Find("ItemImage").GetComponent<Image>();

        if(item)
        {
            displayText.text = item.itemName;
            displayImage.sprite = item.sprite;
            displayImage.color = Color.white;
        }
        else
        {
            displayText.text = "";
            displayImage.sprite = null;
            displayImage.color = Color.clear;
        }
    }

    public void Start()
    {
        UpdateInfo();
    }
}