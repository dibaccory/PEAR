using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlmanacFormManager : MonoBehaviour {

    public Button InterfaceButton;
    public GridLayoutGroup Inventory;


    public void InterfaceButtonClick()
    {

        GameObject almanacPanel = Almanac.instance.almanacPanel;

        if(!almanacPanel.activeSelf)
        {
            almanacPanel.SetActive(true);
        }
        else
        {
            almanacPanel.SetActive(false);
        }
    }

    public void Awake()
    {
        
    }

    public void Start()
    {
        
    }
}
