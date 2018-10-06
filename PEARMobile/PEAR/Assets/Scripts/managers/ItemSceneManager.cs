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
    public GameObject itemModel;
    public GameObject itemLabel;

    public Text questionText;

    public Item currentItem;
    Almanac almanac;

    void Awake()
    {
        ToggleVideo(true);
        var controller = FindObjectOfType<SceneController>();
        currentItem = controller.activeItem;
        itemModel.GetComponent<MeshRenderer>().material = currentItem.material;
        itemLabel.GetComponent<TextMesh>().text = currentItem.itemName;
    }

    public void OnAddItemButtonClick()
    {
        almanac = FindObjectOfType<Almanac>();
        almanac.AddItem(currentItem);
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

    public void returnToCollectButtonCLick()
    {
        var controller = FindObjectOfType<SceneController>();
        controller.FadeAndLoadScene("CollectScene");
    }

    void ToggleVideo(bool value)
    {
        videoQuad.SetActive(value);
        questionsQuad.SetActive(!value);
    }
}
