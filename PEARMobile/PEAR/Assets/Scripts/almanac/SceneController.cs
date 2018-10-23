using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// This script exists in the Persistent scene and manages the content
// based scene's loading.  It works on a principle that the
// Persistent scene will be loaded first, then it loads the scenes that
// contain the player and other visual elements when they are needed.
// At the same time it will unload the scenes that are not needed when
// the player leaves them.
public class SceneController : MonoBehaviour
{
    // Event delegate that is called just before a scene is unloaded.
    public event Action BeforeSceneUnload;

    // Event delegate that is called just after a scene is loaded.
    public event Action AfterSceneLoad;

    // The CanvasGroup that controls the Image used for fading to black.
    public CanvasGroup faderCanvasGroup;

    // How long it should take to fade to and from black.
    public float fadeDuration = 1f;

    // The name of the scene that should be loaded first.
    public string startingSceneName = "CollectScene";

    // Keeps track of the current scene loaded
    public string currentScene = "";

    // Reference to the ScriptableObject which stores the name of the StartingPosition in the next scene.
    public SaveData playerSaveData;

    // Flag used to determine if the Image is currently fading to or from black.
    private bool isFading;

    //(Build mode only) check if item in the almanac has been clicked
    public bool itemSelected = false;

    //self-explanatory
    public string selectedSceneItemInBuildMode;


    public IDictionary<string, Item> itemDictionary = new Dictionary<string, Item>();
    public Item sun;
    public Item mercury;
    public Item venus;
    public Item earth;
    public Item mars;
    public Item jupiter;
    public Item saturn;
    public Item neptune;
    public Item uranus;


    public Item activeItem;

    private IEnumerator Start()
    {
        // Setup all available items
        SetupItemDictionary();

        // Default item is earth for testing
        activeItem = earth;

        // Set the initial alpha to start off with a black screen.
        faderCanvasGroup.alpha = 1f;

        // Start the first scene loading and wait for it to finish.
        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));

        // Once the scene is finished loading, start fading in.
        StartCoroutine(Fade(0f));

    }

    private void SetupItemDictionary()
    {
        itemDictionary.Add("sun",       sun);
        itemDictionary.Add("mercury",   mercury);
        itemDictionary.Add("venus",     venus);
        itemDictionary.Add("earth",     earth);
        itemDictionary.Add("mars",      mars);
        itemDictionary.Add("jupiter",   jupiter);
        itemDictionary.Add("saturn",    saturn);
        itemDictionary.Add("neptune",   neptune);
        itemDictionary.Add("uranus",    uranus);
    }


    // This is the main external point of contact and influence from the rest of the project.
    // This will be called by a SceneReaction when the player wants to switch scenes.
    public void FadeAndLoadScene(string sceneName)
    {
        // If a fade isn't happening then start fading and switching scenes.
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }


    // This is the coroutine where the 'building blocks' of the script are put together.
    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        // Start fading to black and wait for it to finish before continuing.
        yield return StartCoroutine(Fade(1f));

        // If this event has any subscribers, call it.
        if (BeforeSceneUnload != null)
        {
            BeforeSceneUnload();
        }

        // Unload the current active scene.
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        // Start loading the given scene and wait for it to finish.
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        // If this event has any subscribers, call it.
        if (AfterSceneLoad != null)
        {
            AfterSceneLoad();
        }

        // Start fading back in and wait for it to finish before exiting the function.
        yield return StartCoroutine(Fade(0f));
    }


    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        // Allow the given scene to load over several frames and add it to the already 
        // loaded scenes (just the Persistent scene at this point).
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Find the scene that was most recently loaded (the one at the last index of the loaded scenes).
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        // Set the newly loaded scene as the active scene (this marks it as the one to be unloaded next).
        SceneManager.SetActiveScene(newlyLoadedScene);
        currentScene = sceneName;
    }


    private IEnumerator Fade(float finalAlpha)
    {
        // Set the fading flag to true so the FadeAndSwitchScenes coroutine won't be called again.
        isFading = true;

        // Make sure the CanvasGroup blocks raycasts into the scene so no more input can be accepted.
        faderCanvasGroup.blocksRaycasts = true;

        // Calculate how fast the CanvasGroup should fade based on it's current alpha, 
        // it's final alpha and how long it has to change between the two.
        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        // While the CanvasGroup hasn't reached the final alpha yet...
        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            // ... move the alpha towards it's target alpha.
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);

            // Wait for a frame then continue.
            yield return null;
        }

        // Set the flag to false since the fade has finished.
        isFading = false;

        // Stop the CanvasGroup from blocking raycasts so input is no longer ignored.
        faderCanvasGroup.blocksRaycasts = false;
    }
}
