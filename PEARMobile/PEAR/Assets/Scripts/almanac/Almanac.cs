using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Almanac : MonoBehaviour {

    public List<Item> items = new List<Item>();

    public GameObject almanacPanel;

    public static Almanac instance;

    void UpdatePanelSlots()
    {
        int index = 0;
        foreach(Transform child in almanacPanel.transform)
        {
            InventorySlotManager slot = child.GetComponent<InventorySlotManager>();
            if(index < items.Count)
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
        if(items.Count > 9)
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
        if(items.Contains(itemToRemove))
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

    //public const int numItemSlots = 9;

    //public Image[] itemImages = new Image[numItemSlots];
    //public Item[] items = new Item[numItemSlots];

    //public void AddItem(Item itemToAdd) 
    //{
    //    for (int i = 0; i < items.Length; i++) 
    //    {
    //        if(items[i] == null)
    //        {
    //            items[i] = itemToAdd;
    //            itemImages[i].sprite = itemToAdd.sprite;
    //            itemImages[i].enabled = true;
    //            return;
    //        }
    //    }
    //}

    //public void RemoveItem(Item itemToRemove)
    //{
    //    for (int i = 0; i < items.Length; i++)
    //    {
    //        if (items[i] == itemToRemove)
    //        {
    //            items[i] = null;
    //            itemImages[i].sprite = null;
    //            itemImages[i].enabled = false;
    //            return;
    //        }
    //    }
    //}
}
