using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlmanacFormManager : MonoBehaviour {

    public Text inventoryLabel;
    public GameObject almanacPanel;
    public GameObject almanacButton;
    public GameObject buildSceneButton;
    public GameObject collectSceneButton;
    public GameObject logoutButton;

    public void Awake()
    {
        SetButtonVisibility(false);
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

    public void SetButtonVisibility(bool setVisible)
    {
        buildSceneButton.SetActive(setVisible);
        collectSceneButton.SetActive(setVisible);
        logoutButton.SetActive(setVisible);
        almanacButton.SetActive(setVisible);
    }

    public void EnableGUIElements(bool setEnabled)
    {
        almanacPanel.SetActive(setEnabled);
        inventoryLabel.enabled = setEnabled;

    }
}
