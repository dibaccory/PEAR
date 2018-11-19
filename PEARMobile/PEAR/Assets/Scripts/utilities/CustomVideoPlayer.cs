using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class CustomVideoPlayer : MonoBehaviour {

    public Button playButton;
    private VideoPlayer videoPlayer;
    
    // Use this for initialization
    void Start () {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = FindObjectOfType<SceneController>().activeItem.video;

        // Setup Delegates
        videoPlayer.errorReceived += HandleVideoError;
        videoPlayer.started += HandleStartedEvent;
        videoPlayer.prepareCompleted += HandlePrepareCompleted;
        videoPlayer.seekCompleted += HandleSeekCompleted;
        videoPlayer.loopPointReached += HandleLoopPointReached;

        LogClipInfo();
    }
	
	// Update is called once per frame
	void Update () {
		
        if(videoPlayer.isPlaying)
        {
            ShowPlayButton(false);
        }
        else
        {
            ShowPlayButton(true);
        }
	}

    void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause(" + pause + ") called.");
        if (pause)
            Pause();
    }

    public void Play()
    {
        Debug.Log("Play Video");
        PauseAudio(false);
        videoPlayer.Play();
        ShowPlayButton(false);
    }

    public void Pause()
    {
        if (videoPlayer)
        {
            Debug.Log("Pause Video");
            PauseAudio(true);
            videoPlayer.Pause();
            ShowPlayButton(true);
        }
    }
    
    private void ShowPlayButton(bool enable)
    {
        if(playButton != null)
        {
            playButton.enabled = enable;
            playButton.GetComponent<Image>().enabled = enable;
        }
    }

    private void PauseAudio(bool pause)
    {
        for (ushort trackNumber = 0; trackNumber < videoPlayer.audioTrackCount; ++trackNumber)
        {
            if (videoPlayer.GetTargetAudioSource(trackNumber))
            {
                if (pause)
                    videoPlayer.GetTargetAudioSource(trackNumber).Pause();
                else
                    videoPlayer.GetTargetAudioSource(trackNumber).UnPause();
            }
        }
    }

    private void LogClipInfo()
    {
        if (videoPlayer.clip != null)
        {
            string stats =
                "\nName: " + videoPlayer.clip.name +
                "\nAudioTracks: " + videoPlayer.clip.audioTrackCount +
                "\nFrames: " + videoPlayer.clip.frameCount +
                "\nFPS: " + videoPlayer.clip.frameRate +
                "\nHeight: " + videoPlayer.clip.height +
                "\nWidth: " + videoPlayer.clip.width +
                "\nLength: " + videoPlayer.clip.length +
                "\nPath: " + videoPlayer.clip.originalPath;

            Debug.Log(stats);
        }
    }

    void HandleVideoError(VideoPlayer video, string errorMsg)
    {
        Debug.LogError("Error: " + video.clip.name + "\nError Message: " + errorMsg);
    }

    void HandleStartedEvent(VideoPlayer video)
    {
        Debug.Log("Started: " + video.clip.name);
    }

    void HandlePrepareCompleted(VideoPlayer video)
    {
        Debug.Log("Prepare Completed: " + video.clip.name);
    }

    void HandleSeekCompleted(VideoPlayer video)
    {
        Debug.Log("Seek Completed: " + video.clip.name);
    }

    void HandleLoopPointReached(VideoPlayer video)
    {
        Debug.Log("Loop Point Reached: " + video.clip.name);

        ShowPlayButton(true);
    }
}
