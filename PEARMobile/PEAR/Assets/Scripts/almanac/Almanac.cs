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
        if (items.Count < 9)
        {
            items.Add(itemToAdd);
        }
        else
        {
            // TODO: Handle full inventory error
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