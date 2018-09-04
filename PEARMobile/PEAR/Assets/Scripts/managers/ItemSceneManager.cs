using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSceneManager : MonoBehaviour {

    public Button goToQuestionsButton;
    public Button goToVideoButton;
    public GameObject videoQuad;
    public GameObject questionsQuad;

    private void Awake()
    {
        ToggleVideo(true);
    }

    public void OnQuestionButtonClick()
    {
        ToggleVideo(false);
    }

    public void OnVideoButtonClick()
    {
        ToggleVideo(true);
    }

    private void ToggleVideo(bool value)
    {
        videoQuad.SetActive(value);
        questionsQuad.SetActive(!value);
    }
}
