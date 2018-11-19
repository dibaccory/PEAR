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
    public GameObject helpButton;

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
        helpButton.SetActive(setVisible);
    }

    public void EnableGUIElements(bool setEnabled)
    {
        almanacPanel.SetActive(setEnabled);
        inventoryLabel.enabled = setEnabled;

    }

    public void OnHelpButtonClick()
    {
        Handheld.PlayFullScreenMovie("tutoria_video_placeholder.mp4", Color.black, 
                                    FullScreenMovieControlMode.CancelOnInput, 
                                    FullScreenMovieScalingMode.AspectFill);
    }
}
