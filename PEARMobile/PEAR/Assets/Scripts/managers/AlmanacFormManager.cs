using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlmanacFormManager : MonoBehaviour {

    public Text inventoryLabel;
    public GameObject almanacPanel;

    public void Awake()
    {
        EnableGUIElements(false);
    }
    public void InterfaceButtonClick()
    {
        if(!almanacPanel.activeSelf)
        {
            EnableGUIElements(true);
        }
        else
        {
            EnableGUIElements(false);
        }
    }

    private void EnableGUIElements(bool setEnabled)
    {
        almanacPanel.SetActive(setEnabled);
        inventoryLabel.enabled = setEnabled;

    }
}
