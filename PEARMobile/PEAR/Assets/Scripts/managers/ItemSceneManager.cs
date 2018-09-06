using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemSceneManager : MonoBehaviour {

    public Button goToQuestionsButton;
    public Button goToVideoButton;
    public GameObject videoQuad;
    public GameObject questionsQuad;
    public GameObject midAirPositioner;

    public Item currentItem;
    private Almanac almanac;

    private void Awake()
    {
        ToggleVideo(true);
        SceneManager.LoadScene("Almanac", LoadSceneMode.Additive);
    }
    
    public void OnAddItemButtonClick()
    {
        Debug.Log("Item: " + currentItem.name);
        almanac = FindObjectOfType<Almanac>();
        almanac.AddItem(currentItem);
        Debug.Log("Item " + currentItem.name + " added.");
    }
    public void OnQuestionButtonClick()
    {
        ToggleVideo(false);
    }

    public void OnVideoButtonClick()
    {
        ToggleVideo(true);
    }

    public void OnMidAirPositionPlaced()
    {
        midAirPositioner.SetActive(false);
    }

    private void ToggleVideo(bool value)
    {
        videoQuad.SetActive(value);
        questionsQuad.SetActive(!value);
    }
}
