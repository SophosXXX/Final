using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Canvas uiCanvas;
    public Button playButton1;
    public Button playButton2;
    public Button pauseButton;
    public Button exitButton;

    public VideoClip videoClip1;
    public VideoClip videoClip2;

    private bool isMouseOverQuad = false;
    private bool isCanvasVisible = false; // Track the visibility of the canvas

    void Start()
    {
        // Hide UI Canvas initially
        uiCanvas.gameObject.SetActive(false);

        // Set up button click events
        playButton1.onClick.AddListener(() => PlayVideo(videoClip1));
        playButton2.onClick.AddListener(() => PlayVideo(videoClip2));
        pauseButton.onClick.AddListener(TogglePause);
        exitButton.onClick.AddListener(StopVideo);
    }

    void Update()
    {
        // Check if the mouse is over the Quad
        if (isMouseOverQuad && Input.GetButtonDown("js2")) // Check for 'x' key press
        {
            // Toggle the canvas visibility
            isCanvasVisible = !isCanvasVisible;
            uiCanvas.gameObject.SetActive(isCanvasVisible);
        }
    }

    public void OnMouseEnterQuad()
    {
        isMouseOverQuad = true;
    }

    public void OnMouseExitQuad()
    {
        isMouseOverQuad = false;
    }

    public void PlayVideo(VideoClip clip)
    {
        // Load and play the specified video clip
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    public void TogglePause()
    {
        if (videoPlayer.isPlaying)
        {
            // Pause the video if it's playing
            videoPlayer.Pause();
        }
        else
        {
            // Resume playing the video if it's paused
            videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        // Stop video playback and clear the video clip
        videoPlayer.Stop();
        videoPlayer.clip = null;
    }
}
