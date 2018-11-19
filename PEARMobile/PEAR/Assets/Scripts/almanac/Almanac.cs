using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Almanac : MonoBehaviour
{

    public List<Item> items = new List<Item>();

    public GameObject almanacPanel;

    public static Almanac instance;

    void UpdatePanelSlots()
    {
        int index = 0;
        foreach (Transform child in almanacPanel.transform)
        {
            InventorySlotManager slot = child.GetComponent<InventorySlotManager>();
            if (index < items.Count)
            {
                slot.item = items[index];
            }
            else
            {
                slot.item = null;
            }
            slot.UpdateInfo();
            index++;
        }
    }

    public void AddItem(Item itemToAdd)
    {
        if (items.Count < 9 && !items.Contains(itemToAdd))
        {
            items.Add(itemToAdd);
            Router.ItemCollected( DatabaseManager.sharedInstance.GetUser().UserId,
                                  SceneController.controller.classroom, 
                                  SceneController.controller.module,
                                  itemToAdd.tag).SetValueAsync(true);
        }
        else if(items.Contains(itemToAdd))
        {
            // TODO: Handle item already in inventory error
        }
        else if (items.Count >= 9)
        {
            // TODO: Handle inventory full error
        }
        UpdatePanelSlots();
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);

        }
        else
        {
            // TODO: Handle item not found error
        }
        UpdatePanelSlots();
    }

    private void Start()
    {
        instance = this;
        UpdatePanelSlots();
    }
}