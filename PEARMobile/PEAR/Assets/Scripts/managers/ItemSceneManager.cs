using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Firebase.Database;

public class ItemSceneManager : MonoBehaviour 
{

    public Button goToQuestionsButton;
    public Button goToVideoButton;
    public GameObject videoQuad;
    public GameObject questionsQuad;
    public GameObject midAirPositioner;

    public Text questionText;
    public Button answerAButton;
    public Button answerBButton;
    public Button answerCButton;
    public Button answerDButton;

    public Item currentItem;
    Almanac almanac;

    void Awake()
    {
        ToggleVideo(true);
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

    void ToggleVideo(bool value)
    {
        videoQuad.SetActive(value);
        questionsQuad.SetActive(!value);
    }
}
